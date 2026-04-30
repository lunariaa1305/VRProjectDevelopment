using UnityEngine;
using System.Collections.Generic;
namespace LJ.Stats
{
    [CreateAssetMenu(fileName = "StatSet", menuName = "LJ/Stats/StatSet")]
    public class StatSet : ScriptableObject
    {
        public List<Stat> startStats;
        public List<StatEffect> startEffects;
    }

}
