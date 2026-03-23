
using UnityEngine;

namespace LJ.Stats
{
    [System.Serializable]
    public class LocalStatInstance
    {
        public Stat stat;
        public float currentValue;

        public delegate void StatChanged(float currentValue);
        public event StatChanged onStatChanged;

        public LocalStatInstance(Stat stat, float initialValue)
        {
            this.stat = stat;
            this.currentValue = initialValue;
        }
        public void ApplyEffectLogic(StatEffectData effect)
        {
            foreach (StatModifier modifier in effect.modifiers)
            {
                modifier.Evaluate(currentValue, out object result);
                ChangeValue((float)result);
            }
        }
        public void TriggerEvent()
        {
            float normalizedValue = StatMathUtility.GetNormalizedStat(currentValue, stat.minValue, stat.maxValue);
            onStatChanged?.Invoke(normalizedValue);
        }
        public void ChangeValue(float newValue)
        {
            currentValue = Mathf.Clamp(newValue, stat.minValue, stat.maxValue);
            TriggerEvent();
        }
        public void ResetStat()
        {
            currentValue = stat.defaultValue;
        }
    }

}
