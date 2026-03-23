using System.Collections.Generic;
using UnityEngine;
namespace LJ.Stats
{
    [RequireComponent(typeof(StatChangeListener))]
    public class StatEvaluator : MonoBehaviour, IRespond
    {
        [SerializeField] List<CurveResponse> curveResponses;
        void EvaluateStat(float statValue)
        {
            foreach (CurveResponse response in curveResponses)
            {
                response.EvaluateStatCurve(statValue);
            }
        }

        public void StatRespond(float statValue)
        {
            EvaluateStat(statValue);
        }

        public void EffectRespond(EffectContext token)
        {
            throw new System.NotImplementedException();
        }
    }
}

