using System.Collections.Generic;
using UnityEngine;

namespace LJ.Stats.Custom
{
    public static class StatEffectDataExtensions
    {
        public static StatEffectData WithModifier(this StatEffectData data, StatModifier modifier)
        {
            StatEffectData modified = new StatEffectData(new List<StatEffect>(data.providesImmunityTo), data.targetStat, new List<StatModifier>(data.modifiers));
            modified.modifiers.Add(modifier);
            return modified;
        }
    }

}
