using UnityEngine;

public class Smacked : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Working hit with: " + collision.gameObject.name);

        // if this gameObject is hit by a controller, run takedown in parent.
        if (collision.gameObject.name == "ControllerGrabLocation")
        {
            GetComponentInParent<FinishingHitScript>().takedown(this.gameObject);
        }

    }
}