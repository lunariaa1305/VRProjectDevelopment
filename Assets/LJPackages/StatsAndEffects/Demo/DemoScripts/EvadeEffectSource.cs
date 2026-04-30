using UnityEngine;
using UnityEngine.AI;
namespace LJ.Stats.Demos
{
    public class EvadeEffectSource : MonoBehaviour, IRespond
    {
        NavMeshAgent agent;
        [SerializeField] float distance;
        void OnEnable()
        {
            agent = GetComponentInParent<NavMeshAgent>();
        }
        public void EffectRespond(EffectContext token) // this implements IRespond, which is used to respond to stat effects with knowledge of applier, source, effect type, etc.
        {
            Transform source = token.sender.transform; // get the source transform
            Vector3 dir = (transform.position - source.position).normalized; //get direction away from source
            dir.y = 0f;
            Vector3 destination = SampleAgentPositionInDirection(dir);
            if (destination != Vector3.zero) agent.SetDestination(destination);
            
        }
        public Vector3 SampleAgentPositionInDirection(Vector3 direction)
        {
            for (int i = 0; i < distance; i++)
            {
                if(NavMesh.SamplePosition((i+1) * direction + transform.position, out NavMeshHit hit, distance, NavMesh.AllAreas))
                {
                    return hit.position; // find a point on the navmesh in direction
                }
            }
            Debug.LogError("Unable to sample position");
            return Vector3.zero;
        }
        public void StatRespond(float statValue)
        {
            throw new System.NotImplementedException(); //no need for this one
        }

    }

}
