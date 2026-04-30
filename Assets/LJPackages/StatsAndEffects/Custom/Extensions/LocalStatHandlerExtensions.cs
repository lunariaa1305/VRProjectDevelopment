using UnityEngine;

namespace LJ.Stats.Custom
{
    public static class LocalStatHandlerExtensions
    {
        public static LocalStatHandler StatHandler(this Transform transform)
        {
            LocalStatHandler handler = transform.GetComponent<LocalStatHandler>();
            if (handler != null)
            {
                return handler;
            }
            handler =transform.GetComponentInParent<LocalStatHandler>();
            return handler;
        }
        
    }

}
