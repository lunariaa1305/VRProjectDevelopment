using Unity.VisualScripting;
using UnityEngine;

public class crawlScript : MonoBehaviour
{

    Vector3 startRControllerPosition, currentRControllerPosition, startLControllerPosition, currentLControllerPosition, startTransformPosition;
    float lButtonPress, rButtonPress, XTransform, YTransform, ZTransform;
    public float speedModifier, movementSmoothing;
    bool moveWithRController, moveWithLController = false;

    private LayerMask maskForWalls;
    private RaycastHit hit;

    private void Start()
    {
        maskForWalls = LayerMask.GetMask("Wall");
    }


    // Update is called once per frame
    void Update()
    {
        rButtonPress = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        lButtonPress = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        if (rButtonPress > 0.1f)
        {
            // If not already moving with the controller, set a start position
            if (!moveWithRController)
            {
                startRControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
                startTransformPosition = transform.position;
            }

            moveWithRController = true;
        } else
        {
            moveWithRController = false;
        }

        if (lButtonPress > 0.1f)
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
            YTransform = (startRControllerPosition.y - currentRControllerPosition.y) * speedModifier;
            ZTransform = (startRControllerPosition.z - currentRControllerPosition.z) * speedModifier;
        }
        else if (moveWithLController)
        {
            XTransform = (startLControllerPosition.x - currentLControllerPosition.x) * speedModifier;
            YTransform = (startLControllerPosition.y - currentLControllerPosition.y) * speedModifier;
            ZTransform = (startLControllerPosition.z - currentLControllerPosition.z) * speedModifier;
        }
        if ((moveWithLController || moveWithRController) && !Physics.Raycast(transform.position, new Vector3(startTransformPosition.x + XTransform, startTransformPosition.y + YTransform, startTransformPosition.z + ZTransform), 5f, maskForWalls)) // Potential fix for wall clipping
        {
            transform.position = new Vector3(startTransformPosition.x + XTransform, startTransformPosition.y + YTransform, startTransformPosition.z + ZTransform); // Potential improvement to Y movement
        }
    }
}