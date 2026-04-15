using Oculus.Interaction;
using UnityEngine;

public class FinishingHitScript : MonoBehaviour
{

    public GameObject[] targets;
    public GameObject LController, RContrller;

    int currentTarget = 1;

    public void takedown(GameObject target)
    {
        if (GetComponent<Collider>().name == "Hit" + currentTarget.ToString())
        {
            targets[currentTarget].SetActive(false);
            if (currentTarget++ <= targets.Length)
            {
                currentTarget++;
                targets[currentTarget].SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
