using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    CapsuleCollider lightArea;
    Light lightComponent;

    Color currentCameraColor = Color.white;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightArea = GetComponent<CapsuleCollider>();
        lightComponent = GetComponentInChildren<Light>();
    }

    private void Update()
    {
        lightComponent.color = currentCameraColor;
    }

    private void OnTriggerStay(Collider collider)
    {
        Debug.Log("Hit:" + collider.name);
        if (collider.name == "PlayerController")
        {
            Debug.Log("Hit");
            currentCameraColor = Color.darkRed;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("Unhit:" + collider.name);
        if (collider.name == "PlayerController")
        {
            Debug.Log("Unhit");
            currentCameraColor = Color.white;
        }
    }
}
