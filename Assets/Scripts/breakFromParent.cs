using UnityEngine;

public class breakFromParent : MonoBehaviour
{
    Rigidbody myRigidBody;
    float storedVelocity = 0f;

    private void Update()
    {
        Invoke("recordVelocity", 0.25f);
    }

    float recordVelocity()
    {
        return myRigidBody.linearVelocity.x + myRigidBody.linearVelocity.y + myRigidBody.linearVelocity.z;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (myRigidBody.linearVelocity.x + myRigidBody.linearVelocity.y + myRigidBody.linearVelocity.z < storedVelocity / 2)
        {
            transform.parent = null;
        }
    }
}
