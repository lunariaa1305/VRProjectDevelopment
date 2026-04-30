using UnityEngine;

namespace LJ.Stats
{
    [CreateAssetMenu(fileName = "StatEffect", menuName = "LJ/Stats/StatEffect")]
    public class StatEffect : ScriptableObject
    {
        public StatEffectData[] dataSet;
        [Tooltip("Responses can be triggered by effect type, to enable grouping similar effects together")]public StatEffectType effectType;
        [Header("Continuous Only")]
        [Tooltip("Effect will be modified at this interval if continuous")] public float interval;
        [Tooltip("Does the effect stick around, or is it only applied on impact.")] public bool isContinuous;

        public StatEffect(StatEffectData[] dataSet, StatEffectType effectType, float interval, bool isContinuous)
        {
            this.dataSet = dataSet;
            this.effectType = effectType;
            this.interval = interval;
            this.isContinuous = isContinuous;
        }
        private void OnValidate()
        {
            foreach(StatEffectData data in dataSet)
            {
                data.NameModifiers();
            }
        }
    }

}
