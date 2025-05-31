using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTriggerLoader : MonoBehaviour
{
    public string sceneName;
    private bool isLoaded = false;

    void OnTriggerEnter(Collider other)
    {
        if (!isLoaded && other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isLoaded && other.CompareTag("Player"))
        {
            SceneManager.UnloadSceneAsync(sceneName);
            isLoaded = false;
        }
    }
}
