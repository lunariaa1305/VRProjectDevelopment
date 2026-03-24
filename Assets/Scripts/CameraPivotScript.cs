using Meta.WitAi.Utilities;
using Unity.VisualScripting;
using UnityEngine;

public class CameraPivotScript : MonoBehaviour
{
    
    public float rotation = 30f;
    public float rotationSpeed = 0.5f;
    public float pause = 3f;


    // Update is called once per frame
    void Update()
    {
        if (rotation + rotationSpeed * Time.deltaTime <= 150f && rotation + rotationSpeed * Time.deltaTime >= 30f)
        {
            rotation += rotationSpeed * Time.deltaTime;
        } else {
            // reverse the rotation direction
            rotationSpeed *= -1;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
