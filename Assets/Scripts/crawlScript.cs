using Unity.VisualScripting;
using UnityEngine;

public class crawlScript : MonoBehaviour
{

    Vector3 startRControllerPosition, currentRControllerPosition, startLControllerPosition, currentLControllerPosition, startTransformPosition;
    float lButtonPress, rButtonPress, XTransform, ZTransform;
    public float speedModifier, movementSmoothing;
    bool moveWithRController, moveWithLController = false;

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
        ZTransform = 0;

        if (moveWithRController)
        {
            XTransform = (startRControllerPosition.x - currentRControllerPosition.x) * speedModifier;
            ZTransform = (startRControllerPosition.z - currentRControllerPosition.z) * speedModifier;
        }
        else if (moveWithLController)
        {
            XTransform = (startLControllerPosition.x - currentLControllerPosition.x) * speedModifier;
            ZTransform = (startLControllerPosition.z - currentLControllerPosition.z) * speedModifier;
        }
        if (moveWithLController || moveWithRController)
        {
            transform.position = new Vector3(startTransformPosition.x + XTransform, startTransformPosition.y, startTransformPosition.z + ZTransform);
        }
    }
}
