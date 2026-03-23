using UnityEngine;
using LJ.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace LJ.Stats
{
    public delegate void EffectEvent(EffectContext context);
    public class LocalStatHandler : MonoBehaviour, IInitializable
    {

        [SerializeField] private StatSet data;
        [SerializeField] private List<StatEffect> startEffects;
        [SerializeField] List<LocalStatInstance> stats; //holds all stat instances for easy iteration
        [SerializeField] private float updatesPerSecond;
        private Dictionary<Stat, LocalStatInstance> statDictionary; //used for lookup instance by stat
        private Dictionary<StatEffect, AppliedStatEffect> activeEffects = new Dictionary<StatEffect, AppliedStatEffect>(); //lookup for active effects 
        private Coroutine loop;

        public event EffectEvent onEffectAdded;//broadcasts event whenever an effect is added, informing context and effect
        public event EffectEvent onInstantaneousEffectApplied; // broadcasts when an effect is applied instantaneously (one time)
        public event Initialize onInitializationComplete; //from IInitializable interface, this allows other classes to be sure that this class is ready to be interacted with

        public void BroadcastInitializationComplete()
        {
            onInitializationComplete?.Invoke();
        }
//=====================================================================================
        public float GetStatPercentage(Stat stat)
        {
            if (!InstanceExists(stat))
            {
                Debug.Log("Instance not found for stat: " + stat.ToString());
                return float.NaN;

            }
            statDictionary.TryGetValue(stat, out LocalStatInstance statValue);
            return StatMathUtility.GetNormalizedStat(statValue.currentValue, stat.minValue, stat.maxValue);
        }
//=====================================================================================
        public LocalStatInstance GetInstance(Stat stat) //find local stat instance by stat
        {
            if (statDictionary.TryGetValue(stat, out LocalStatInstance instance)) return instance;
            else
            {
                Debug.Log("Instance Not Available");
                return null;
            }
            //get local stat instance repreasenting stat
        }
        public AppliedStatEffect GetEffect(StatEffect effect) => activeEffects.TryGetValue(effect, out var appliedEffect) ? appliedEffect : null;
//=====================================================================================
        public void RemoveEffect(StatEffect effect)
        {
            TryRemoveEffect(effect);
        }
        public bool TryApplyEffect(StatEffect effect, EffectContext context = null)
        {
            if(context == null)
                context = new EffectContext(effect, this, Time.time); //if no token is provided, create a new one with default values
            //====================================================
            if (!IsValidEffectApplication(effect, context))
                return false;
            //====================================================== ^ failure conditions
            if (!effect.isContinuous)
            {
                TriggerEffectData(effect.dataSet);
                onInstantaneousEffectApplied?.Invoke(context); // If there is no continuous data, the effect can be applied once and ended.
                return true;
            }
            AppliedStatEffect appliedEffect = GetEffect(effect);
            if (appliedEffect != null)
            {
                appliedEffect.AddContext(context);
            }
            else
            {
                appliedEffect = CreateAppliedEffectInstance(effect, context);
                appliedEffect.ConfirmInitialization();
                onEffectAdded?.Invoke(context); //broadcast
            }
            return true;
        }
        public void ApplyEffectSimple(StatEffect effect)
        {
            TryApplyEffect(effect, new EffectContext(effect, this, Time.time));
        }
        public bool TryApplySingleDataEffect(StatEffectData data, EffectContext token)
        {
            RemoveImmuneEffectsByData(data);
            onInstantaneousEffectApplied?.Invoke(token);

            LocalStatInstance instance = GetInstance(data.targetStat);
            if (instance == null)
                return false;
            instance.ApplyEffectLogic(data);
            return true;
        }
        public bool TryRemoveEffectBySender(StatEffect effect, Component sender)
        {
            AppliedStatEffect appliedEffect = GetEffect(effect);
            if (appliedEffect == null || sender == null)
            {
                Debug.LogWarning("LJStats: Failed to remove effect by sender:" + effect.name + " sender: " + sender.name);
                return false;
            }
            EffectContext ctx = appliedEffect.GetContextBySender(sender);
            if (ctx != null)
            {
                appliedEffect.RemoveContext(ctx);
                return true;
            }
            Debug.LogWarning("LJStats: Sender is not associated with effect: " + effect.name + " sender: " + sender.name);
            return false;
            
        }
        public bool TryRemoveEffectContext(StatEffect effect, EffectContext context)
        {
            //===============================================================================
            AppliedStatEffect appliedEffect = GetEffect(effect);
            if (effect == null || context == null || appliedEffect == null || !appliedEffect.CheckContext(context))
            {
                Debug.LogWarning("Failed to remove effect:" + effect.name + " sender: " + context.sender.name);
                return false;
            }
            //====================================================== ^ Failure conditions
            appliedEffect.RemoveContext(context);
            return true;
        }
        public bool TryRemoveEffect(StatEffect effect)
        {
            AppliedStatEffect appliedEffect = GetEffect(effect);
            if (effect == null || appliedEffect == null)
            {
                return false;
            }
            appliedEffect.RemoveEffect(); //cleanup
            return true;
        }
        public void CleanupRemovedAppliedEffect(AppliedStatEffect effect)
        {
            activeEffects.Remove(effect.effect);
        }
//=====================================================================================
        private bool InstanceExists(Stat stat)
        {
            return statDictionary.ContainsKey(stat);
        }
        private bool Immune(StatEffect effect) => activeEffects.Values.Any(e => e.CheckImmune(effect));

        private bool IsValidEffectApplication(StatEffect effect, EffectContext token)
        {

            if (effect == null || token == null)
            {
                Debug.LogWarning("Tried to add null effect!");
                return false;
            }
            if (Immune(effect))
                return false;
            return true;

        }
//=====================================================================================
        private void Awake()
        {
            statDictionary = new Dictionary<Stat, LocalStatInstance>();
            stats = new List<LocalStatInstance>();
            foreach (Stat stat in data.startStats) AddStat(stat);
        }
        private void Start()
        {
            foreach (StatEffect effect in startEffects) TryApplyEffect(effect); //adds effects that are in list from start, allowing user to add effects to one-off characters
            if (data.startEffects != null && data.startEffects.Count > 0)
            {
                foreach (StatEffect effect in data.startEffects) TryApplyEffect(effect);  //adds effects from stat set starters
            }

            loop = StartCoroutine(UpdateLoop());
            BroadcastInitializationComplete(); //this ensures other classes do not attempt to reference objects that aren't ready yet
        }
//=====================================================================================
        private IEnumerator UpdateLoop()
        {
            while (this.isActiveAndEnabled)
            {
                foreach (AppliedStatEffect effect in activeEffects.Values)
                {
                    effect.Evaluate();
                }
                yield return new WaitForSeconds(1f / updatesPerSecond);
            }
        }
        private AppliedStatEffect CreateAppliedEffectInstance(StatEffect effect, EffectContext token)
        {
            AppliedStatEffect appliedEffect = new AppliedStatEffect(this, effect, token);
            activeEffects.Add(effect, appliedEffect);
            foreach (StatEffectData data in effect.dataSet)
            {
                appliedEffect.AddInstance(GetInstance(data.targetStat), data);
                RemoveImmuneEffectsByData(data);
            }
            return appliedEffect;
        }
        private void AddStat(Stat stat) // initializes stats
        {
            LocalStatInstance instance = new LocalStatInstance(stat, stat.defaultValue);
            statDictionary.Add(stat, instance);
            stats.Add(instance);
        }
        private void TriggerEffectData(StatEffectData[] dataSet)
        {
            foreach (StatEffectData data in dataSet)
            {
                RemoveImmuneEffectsByData(data);
                if (data.targetStat == null) continue;
                LocalStatInstance instance = GetInstance(data.targetStat);
                if (instance == null) continue;
                instance.ApplyEffectLogic(data);
            }
        }
        private void RemoveImmuneEffectsByData(StatEffectData effectData)
        {
            foreach (StatEffect immuneTo in effectData.providesImmunityTo)
            {
                TryRemoveEffect(immuneTo);
            }
        }

    }
}
