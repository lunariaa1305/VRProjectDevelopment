using System.Collections.Generic;
using UnityEngine;

namespace LJ.Stats
{
    public class EffectContext
    {
        public StatEffect effect;
        public Component sender;
        public float timeApplied;
        public int priority;
        public List<ContextProperty> properties;

        public ContextProperty[] context;
        public EffectContext(StatEffect effect, Component sender, float timeApplied, int priority = 0, List<ContextProperty> properties = null)
        {
            this.effect = effect;
            this.sender = sender;
            this.timeApplied = timeApplied;
            this.priority = priority;
            this.properties = properties;
        }
        public ContextProperty GetProperty<T>(PropertyKey key)
        {
            return properties.Find(x => x.value.GetType() == typeof(T) && x.key == key);
        }
        
    }

}
