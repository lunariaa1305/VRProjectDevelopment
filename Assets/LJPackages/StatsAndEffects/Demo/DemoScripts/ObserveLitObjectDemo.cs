using LJ.Stats.Custom;
using UnityEngine;

namespace LJ.Stats.Demos
{
    public class ObserveLitObjectDemo : MonoBehaviour
    {
        [SerializeField] Transform start;
        [SerializeField] Transform target;
        [SerializeField] Stat litStat;
        [SerializeField][Range(0f,1f)] float litPercentTrigger;
        LocalStatHandler handler;
        private void Start()
        {
            handler = target.StatHandler();
            
        }
        private void Update()
        {
            if (handler.GetStatPercentage(litStat) >= litPercentTrigger) { Debug.DrawLine(start.position, target.position, Color.green); }
            else Debug.DrawLine(start.position, target.position, Color.red);
            
        }
    }

}
