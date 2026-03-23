using UnityEngine;

namespace LJ.Stats
{
    [CreateAssetMenu(fileName = "Stat", menuName = "LJ/Stats/Stat")]
    public class Stat : ScriptableObject
    {
        public string displayName;
        public float maxValue;
        public float minValue;
        public float defaultValue;

    }

}
