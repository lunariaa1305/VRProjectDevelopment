using LJ.Stats.Custom;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LJ.Stats
{
    public class EffectChangeListener : MonoBehaviour
    {
        [SerializeField] Transform overrideTransform;
        LocalStatHandler statHandler;
        [SerializeField] List<StatEffect> acceptedEffects;
        HashSet<StatEffect> acceptedEffectsLookup;
        [SerializeField] List<StatEffectType> acceptedTypes;
        HashSet<StatEffectType> acceptedTypesLookup;
        [SerializeField] UnityEvent<EffectContext> effectInstantResponse;
        [Header("Continuous")]
        [Tooltip("Continuous effects will trigger this on applied")][SerializeField] UnityEvent<EffectContext> effectStartedResponse;
        [Tooltip("Continuous effects will trigger when repeated")][SerializeField] UnityEvent<EffectContext> effectRepeatedResponse;
        [Tooltip("Continuous effects will trigger this when removed")][SerializeField] UnityEvent<EffectContext> effectEndedResponse;
        AppliedStatEffect appliedEffect;
        private void Awake()
        {
            acceptedEffectsLookup = new HashSet<StatEffect>();
            acceptedTypesLookup = new HashSet<StatEffectType>();
            foreach (StatEffect effect in acceptedEffects)
            {
                acceptedEffectsLookup.Add(effect);
            }
            foreach (StatEffectType effectType in acceptedTypes)
            {
                acceptedTypesLookup.Add(effectType);
            }
        }
        private void Start()
        {
            if (overrideTransform != null)
            {
                statHandler = overrideTransform.StatHandler();
            }
            else
            {
                statHandler = GetComponentInParent<LocalStatHandler>();
            }
            statHandler.onEffectAdded += StartEffect;
            statHandler.onInstantaneousEffectApplied += ApplyInstantaneousEffect;
        }

        void StartEffect(EffectContext token)
        {
            if (CheckEffect(token.effect) || CheckType(token.effect.effectType))
            {
                effectStartedResponse?.Invoke(token);
                appliedEffect = statHandler.GetEffect(token.effect);
                appliedEffect.onEffectApplied += ApplyRepeatEffect;
                appliedEffect.onEffectEnded += EndEffect;
            }
        }
        void ApplyRepeatEffect(EffectContext token)
        {
            effectRepeatedResponse?.Invoke(token);
        }
        void ApplyInstantaneousEffect(EffectContext token)
        {
            if (CheckEffect(token.effect) || CheckType(token.effect.effectType))
            {
                effectInstantResponse?.Invoke(token);
            }
        }
        void EndEffect(EffectContext token)
        {
            effectEndedResponse?.Invoke(token);
            if (appliedEffect != null)
            {
                appliedEffect.onEffectApplied -= ApplyRepeatEffect;
                appliedEffect.onEffectEnded -= EndEffect;
            }
        }
        bool CheckEffect(StatEffect effect)
        {
            if (acceptedEffectsLookup.Contains(effect)) return true;
            return false;
        }
        bool CheckType(StatEffectType type)
        {
            if (acceptedTypesLookup.Contains(type)) return true;
            return false;
        }
        private void OnDisable()
        {
            statHandler.onEffectAdded -= StartEffect;
            statHandler.onInstantaneousEffectApplied -= ApplyInstantaneousEffect;
            if (appliedEffect != null)
            {
                appliedEffect.onEffectApplied -= ApplyRepeatEffect;
                appliedEffect.onEffectEnded -= EndEffect;
            }
        }
    }
}

