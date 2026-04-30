using UnityEngine;
using System.Collections.Generic;
using System;
namespace LJ.Stats
{
	public class AppliedStatEffect 
    {
        public StatEffect effect;
        private List<EffectContext> contexts;
        private EffectContext mostRecentContext;
        private Dictionary<LocalStatInstance, StatEffectData> dataByStat;
        private HashSet<StatEffect> immunities;
        private bool effectActive = true;
        private float lastTime;
        public EffectEvent onEffectApplied;
        public EffectEvent onEffectEnded;
        LocalStatHandler handler;
        public AppliedStatEffect(LocalStatHandler handler, StatEffect appliedEffect, EffectContext token)
        {
            this.handler = handler;
            contexts = new List<EffectContext>();
            immunities = new HashSet<StatEffect>();
            contexts.Add(token);
            mostRecentContext = token;
            effect = appliedEffect;
            dataByStat = new Dictionary<LocalStatInstance, StatEffectData>();
            lastTime = Time.time;
            foreach (StatEffectData effectData in effect.dataSet)
            {
                foreach(StatEffect immunity in effectData.providesImmunityTo)
                {
                    immunities.Add(immunity);
                }
            }
            
        }
        public void AddContext(EffectContext context)
        {
            if (!CheckContext(context)) 
                contexts.Add(context);
            mostRecentContext = context;
        }
        public EffectContext GetContextBySender(Component sender)
        {
            return contexts.Find(x => x.sender == sender);
        }
        public void RemoveContext(EffectContext context)
        {
            if (CheckContext(context)) 
                contexts.Remove(context);
            if (contexts.Count == 0)
            {
                RemoveEffect();
            }
        }
        public bool CheckContext(EffectContext context)
        {
            return contexts.Contains(context);
        }
        public bool Compare(StatEffect effect, EffectContext context)
        {
            if (this.effect == effect && contexts.Contains(context)) 
                return true;
            else return false;
        }
        public bool CheckImmune(StatEffect effect)
        {
            if (effectActive && immunities.Contains(effect)) 
                return true;
            else return false;
        } //compare data inside class without direct class comparison
        public void Evaluate() 
        {
            if (!effectActive)
                return;
            
            if (lastTime + effect.interval <= Time.time)
            {
                lastTime = Time.time;

                foreach (LocalStatInstance instance in dataByStat.Keys) // applies effect logic to each
                {
                    StatEffectData data = dataByStat[instance];
                    foreach(StatModifier modifier in data.modifiers)
                    {
                        modifier.Evaluate(instance.currentValue, out object result);
                        instance.ChangeValue((float)result);
                    }

                }
                onEffectApplied?.Invoke(mostRecentContext);
            }
        }
        public void ConfirmInitialization()
        {
            Activate();
            Evaluate();
        }
        public void Activate()
        {
            effectActive = true;
        }
        public void AddInstance(LocalStatInstance instance, StatEffectData data)
        {
            if (instance == null)
            {
                Debug.LogWarning("LJ Stats: Tried to add null stat instance to dataByStat dictionary.");
                return;
            }
            dataByStat.Add(instance, data);
        }
        public float GetTimeElapsed()
        {
            return Time.time - mostRecentContext.timeApplied;
        }
        public void RemoveEffect()
        {
            onEffectEnded?.Invoke(mostRecentContext);
            effectActive = false;
            dataByStat.Clear();
            contexts.Clear();
            mostRecentContext = null;
            Debug.Log("No effect appliers left, removing...");
            handler.CleanupRemovedAppliedEffect(this);
        }
    }
}