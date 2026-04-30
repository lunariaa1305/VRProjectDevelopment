using Unity.VisualScripting;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    CapsuleCollider lightArea;
    Light lightComponent;
    public crawlScript crawlScript;

    public float rateOfSpeedChange;

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
        if (collider.name == "PlayerController")
        {
            currentCameraColor = Color.darkRed;
            crawlScript.speedModifier = crawlScript.speedModifier / rateOfSpeedChange;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.name == "PlayerController")
        {
            currentCameraColor = Color.white;
            crawlScript.speedModifier = Mathf.Lerp(crawlScript.speedModifier, 2, 0.2f);
        }
    }
}
