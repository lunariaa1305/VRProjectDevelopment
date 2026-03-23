using UnityEngine;

namespace LJ.Stats.Custom
{
    public class ApplyStatEffectOnTriggerEnter : MonoBehaviour
    {
        [SerializeField] StatEffect effect;
        [SerializeField] bool ignoreParentCollisions;
        Collider trigger;
        EffectContext context;
        private void Start()
        {
            context = new EffectContext(effect, this, Time.time);
            trigger = transform.GetComponent<Collider>();
            if (ignoreParentCollisions)
            {
                Collider[] colliders = transform.root.GetComponentsInChildren<Collider>();
                foreach (Collider collider in colliders)
                {
                    if (collider == trigger) continue;
                    Physics.IgnoreCollision(trigger, collider);
                }
            }
        }
        void OnTriggerEnter(Collider other)
        {
            if (!isActiveAndEnabled) return;
            LocalStatHandler handler = other.transform.GetComponentInParent<LocalStatHandler>();
            if (handler == null) return;

            context.timeApplied = Time.time;
            handler.TryApplyEffect(effect, context);
        }

    }

}
