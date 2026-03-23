using UnityEngine;

namespace LJ.Stats
{
    public static class StatMathUtility
    {
        public static float GetNormalizedStat(float current, float min, float max)
        {
            return (current - min) / (max - min);
        }
    }

}
