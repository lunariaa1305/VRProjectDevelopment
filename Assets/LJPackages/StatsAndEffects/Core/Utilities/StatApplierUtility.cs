using LJ.Stats.Custom;
using UnityEngine;

namespace LJ.Stats
{
    public static class StatApplierUtility
    {
        public static void ApplyEffectInRadius(Vector3 position, float radius, StatEffect effect, LayerMask exclusionMask, EffectContext token)
        {
            Collider[] checks = Physics.OverlapSphere(position, radius, ~exclusionMask);
            if (checks.Length == 0) return;

            LocalStatHandler handler = null;
            foreach (Collider check in checks)
            {
                handler = check.transform.StatHandler();
                if (handler != null)
                {
                    handler.TryApplyEffect(effect, token);
                }
            }
        }
        public static void ApplyEffectByRay(Ray ray, float length, StatEffect effect, LayerMask exclusionMask, EffectContext token)
        {

            Physics.Raycast(ray, out RaycastHit hit, length, ~exclusionMask);
            if (hit.transform != null)
            {
                LocalStatHandler handler = hit.transform.StatHandler();
                if (handler != null) handler.TryApplyEffect(effect, token);
            }
        }
    }
}
