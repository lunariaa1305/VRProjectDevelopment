using UnityEngine;

public class controllerBehavior : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {

        Debug.Log("Hit: " + collision.name);

        // if the player hits an object marked as able to be hit in a takedown
        if (collision.tag == "hitTarget")
        {
            collision.GetComponentInParent<FinishingHitScript>().takedown(collision.gameObject);
        }        
    }
}
