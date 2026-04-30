using UnityEngine;
using UnityEngine.Events;
namespace LJ.Stats.Demos
{
    public class TimedUnityEvent : MonoBehaviour
    {
        public UnityEvent startOccurence;
        public UnityEvent endOccurence;
        [SerializeField] float time;
        public void DoEvent()
        {
            startOccurence?.Invoke();
            Invoke("End", time);
        }
        void End()
        {
            endOccurence?.Invoke();
        }
    }

}
