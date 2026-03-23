using UnityEngine;

namespace LJ.Stats.Demos
{
    public class Explode : MonoBehaviour
    {
        [SerializeField] float explosionForce;
        [SerializeField] float explosionRadius;
        public void TriggerExplosion()
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, 3.0F);
            }
        }
    }

}
