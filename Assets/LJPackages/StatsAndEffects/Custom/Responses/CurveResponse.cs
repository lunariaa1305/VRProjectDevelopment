using UnityEngine;
using UnityEngine.Events;

namespace LJ.Stats
{
    [System.Serializable]
    public class CurveResponse
    {
        public string name;
        [Tooltip(
        "This curve represents the stat, x0 is minValue, x1 is maxValue" +
        "The curve will essentially transform the stats value with a multiplier")]
        [SerializeField] AnimationCurve statCurve;
        [Tooltip("If not a multiplier, then the response will just use the point along the curve")]
        [SerializeField] bool multiplier;
        [SerializeField] UnityEvent<float> response;
        public void EvaluateStatCurve(float statValue) // transforms stat based on curve
        {
            float statCurved = statCurve.Evaluate(statValue);
            if (multiplier) response?.Invoke(statCurved * statValue);
            else response?.Invoke(statCurved);
        }
    }
}

