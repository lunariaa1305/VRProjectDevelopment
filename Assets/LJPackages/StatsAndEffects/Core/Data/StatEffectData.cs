using LJ.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LJ.Stats
{
    [System.Serializable]
    public class StatEffectData
    {
        public string name;
        public List<StatEffect> providesImmunityTo; //while this effect is applied, these effects will not be able to be added
        public List<StatModifier> modifiers;
        public Stat targetStat; //which stat to affect

        public StatEffectData(List<StatEffect> providesImmunityTo, Stat targetStat, List<StatModifier> modifiers)
        {
            this.providesImmunityTo = providesImmunityTo;
            this.targetStat = targetStat;
            this.modifiers = modifiers;
            foreach (StatModifier modifier in modifiers)
            {
                modifier.SetName();
            }
        }
        public void NameModifiers()
        {
            foreach (StatModifier modifier in modifiers)
            {
                modifier.SetName();
            }
        }
    }

}
