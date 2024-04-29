using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName; // Name of the scene to load
    public KeyCode switchKey = KeyCode.F; // Key to trigger scene switch

    
    private void Update()
    {
        // Check if the specified key is pressed
        if (Input.GetKeyDown(switchKey))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
    }
}