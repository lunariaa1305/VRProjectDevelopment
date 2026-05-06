using UnityEngine;

public class menuButtonSmacked : MonoBehaviour
{

    public sceneManagerScript sceneManagerScript;

    private void OnTriggerEnter(Collider collision)
    {

        // if this gameObject is hit by a controller, run takedown in parent.
        if (collision.gameObject.name == "ControllerGrabLocation")
        {
            Debug.Log("Start Game");
            sceneManagerScript.startGame();
        }
    }
}