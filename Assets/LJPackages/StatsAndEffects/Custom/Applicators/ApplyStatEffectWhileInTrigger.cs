using System.Collections.Generic;
using UnityEngine;

namespace LJ.Stats.Custom
{
    public class ApplyStatEffectWhileInTrigger : MonoBehaviour
    {
        //this is a demo script showing how a system can apply an effect for a temporary time using triggers
        [SerializeField] StatEffect effect;
        List<LocalStatHandler> affectedHandlers = new List<LocalStatHandler>();
        EffectContext context;
        private void Start()
        {
            context = new EffectContext(effect, this, Time.time);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!isActiveAndEnabled) return;
            LocalStatHandler handler = other.transform.StatHandler();
            if (handler == null) return;
            handler.TryApplyEffect(effect, context);
            affectedHandlers.Add(handler);
        }
        private void OnTriggerExit(Collider other)
        {
            LocalStatHandler handler = other.transform.StatHandler();   
            if (handler == null) return;
            handler.TryRemoveEffectBySender(effect, this);
            affectedHandlers.Remove(handler);
        }
        private void OnDisable()
        {
            foreach(LocalStatHandler handler in affectedHandlers)
            {
                handler.TryRemoveEffectBySender(effect, this);
            }
            affectedHandlers.Clear();
        }
    }

}
