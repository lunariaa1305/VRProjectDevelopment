using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagerScript : MonoBehaviour
{

    public float fadeDuration = 0f;
    public Material fadeMaterial;

    public void startGame()
    {
        Debug.Log("Start Game Recieved");

        while (fadeDuration < 5f)
        {
            Debug.Log("Start Game; FadeDuration : " + fadeDuration);
            fadeDuration += Time.deltaTime;
            fadeMaterial.color = new Color(0, 0, 0, 255 - fadeDuration * 51);
        }
        SceneManager.LoadScene(1);
    }
}
