using UnityEngine;

public class crawlScript : MonoBehaviour
{

    Vector3 currentControllerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            currentControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
            Debug.Log(currentControllerPosition);
        }
    }


}
