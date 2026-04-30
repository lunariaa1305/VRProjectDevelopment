using LJ.Stats.Custom;
using LJ.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace LJ.Stats
{
    public class StatChangeListener : MonoBehaviour, IRespond
    {
        [SerializeField] Transform overrideTransform;
        LocalStatHandler statHandler;
        [SerializeField] Stat targetStat;
        public UnityEvent<float> response;

        LocalStatInstance statInstance;
        private void Awake()
        {
            if (overrideTransform != null)
            {
                statHandler = overrideTransform.StatHandler();
            }
            else
            {
                statHandler = GetComponentInParent<LocalStatHandler>();
            }

            IInitializable init = statHandler as IInitializable;
            init.onInitializationComplete += CompleteInitialization;
            
        }
        void CompleteInitialization() // once handler initialized, sub to events and do first update
        {
            Debug.Log("Initialization complete, subscribing to events...");
            statInstance = statHandler.GetInstance(targetStat);
            statInstance.onStatChanged += StatRespond;

            statInstance.TriggerEvent();
        }
        private void OnDisable()
        {
            statInstance = statHandler?.GetInstance(targetStat);
            statInstance.onStatChanged -= StatRespond;
        }

        public void StatRespond(float statValue)
        {
            response?.Invoke(statValue);
        }

        public void EffectRespond(EffectContext token)
        {
            throw new System.NotImplementedException();
        }
    }

}
