using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class crawlScript : MonoBehaviour
{

    Vector3 startRControllerPosition, currentRControllerPosition, startLControllerPosition, currentLControllerPosition, startTransformPosition;
    public Vector3 movementVector;
    float lButtonPress, rButtonPress, XTransform, YTransform, ZTransform;
    public float speedModifier, movementSmoothing;
    bool moveWithRController, moveWithLController, interrupt = false;

    public LayerMask maskForWalls;

    private void Start()
    {
        maskForWalls = LayerMask.GetMask("Collidable");
    }


    // Update is called once per frame
    void Update()
    {
        rButtonPress = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        lButtonPress = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        if (rButtonPress > 0.1f && !interrupt) // Maybe check to see if anything should interrupt the dragging? So we are able to apply physics and collisions
        {
            // If not already moving with the controller, set a start position
            if (!moveWithRController)
            {
                startRControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
                startTransformPosition = transform.position;
            }

            moveWithRController = true;
        }
        else
        {
            moveWithRController = false;
        }

        if (lButtonPress > 0.1f && !interrupt)
        {
            // If not already moving with the controller, set a start position
            if (!moveWithLController)
            {
                startLControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
                startTransformPosition = transform.position;
            }

            moveWithLController = true;
        }
        else
        {
            moveWithLController = false;
        }

        

        currentRControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
        currentLControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        XTransform = 0;
        YTransform = 0;
        ZTransform = 0;


        if (moveWithRController)
        {
            XTransform = (startRControllerPosition.x - currentRControllerPosition.x) * speedModifier;
            //YTransform = (startRControllerPosition.y - currentRControllerPosition.y) * speedModifier;
            ZTransform = (startRControllerPosition.z - currentRControllerPosition.z) * speedModifier;
        }
        else if (moveWithLController)
        {
            XTransform = (startLControllerPosition.x - currentLControllerPosition.x) * speedModifier;
            //YTransform = (startLControllerPosition.y - currentLControllerPosition.y) * speedModifier;
            ZTransform = (startLControllerPosition.z - currentLControllerPosition.z) * speedModifier;
        }


        if ((moveWithLController || moveWithRController)) // Potential fix for wall clipping
        {
            movementVector = new Vector3(startTransformPosition.x + XTransform, startTransformPosition.y + YTransform, startTransformPosition.z + ZTransform);
            Debug.DrawRay(transform.position, new Vector3(), Color.pink, 20f);
            transform.position = movementVector; // Potential improvement to Y movement
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        while (collision.gameObject.layer == 3)
        {
            interrupt = true;
            //transform.position = new Vector3(-movementVector.x, movementVector.y, -movementVector.z);
        }

        if (collision.gameObject.layer != 3)
        {
            interrupt = false;
        }
    }
}


/*
 * 
 *  https://developers.meta.com/horizon/documentation/unity/unity-isdk-ray-interaction/
 * 
 */