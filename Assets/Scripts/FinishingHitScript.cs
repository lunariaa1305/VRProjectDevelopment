using Oculus.Interaction;
using UnityEngine;

public class FinishingHitScript : MonoBehaviour
{

    public GameObject[] targets;
    // public GameObject LController, RContrller;

    int currentTarget = 1;

    public void takedown(GameObject target)
    {
        if (target.name == "Hit" + currentTarget.ToString())
        {
            Debug.Log("Gameobject Passed: " + target.gameObject.name);

            if (currentTarget + 1 <= targets.Length)
            {
                targets[currentTarget - 1].SetActive(false);
                Debug.Log("Set target " + currentTarget + " to inactive");
                currentTarget++;
                Debug.Log("Target is now: " + currentTarget);

                targets[currentTarget - 1].SetActive(true);
                Debug.Log("Set target " + currentTarget + " to active");
            }
            else
            {
                Debug.Log("End of target chain, destroying...");
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name);   
    }
}
