using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    void Start()
    {
        // Φορτώνει τις επιπλέον σκηνές προσθετικά
        SceneManager.LoadScene("Market", LoadSceneMode.Additive);
        SceneManager.LoadScene("CastleScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("Trees", LoadSceneMode.Additive);
    }
}
