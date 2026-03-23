using UnityEngine;

namespace LJ.Stats
{
    public class ContextProperty
    {
        public PropertyKey key;
        public object value;
        public ContextProperty(PropertyKey key, object value)
        {
            this.key = key;
            this.value = value; 
        }
    }
}
