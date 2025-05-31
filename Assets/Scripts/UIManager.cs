using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject crosshair;
    public CanvasGroup helpCanvasGroup;
    public CanvasGroup pauseMenuCanvasGroup;
    public CanvasGroup inventoryPanel;

    private bool isHelpVisible = false;
    private bool isPaused = false;
    private bool isInventoryOpen = false;
    private bool cameFromPause = false;
    private float fadeDuration = 0.3f;

    private CameraFollow cameraFollow;

    void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        if (crosshair != null)
            crosshair.SetActive(cameraFollow.IsFirstPerson());

        if (helpCanvasGroup != null)
        {
            helpCanvasGroup.alpha = 0f;
            helpCanvasGroup.interactable = false;
            helpCanvasGroup.blocksRaycasts = false;
        }

        if (pauseMenuCanvasGroup != null)
        {
            pauseMenuCanvasGroup.alpha = 0f;
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;
        }

        if (inventoryPanel != null)
        {
            inventoryPanel.alpha = 0f;
            inventoryPanel.interactable = false;
            inventoryPanel.blocksRaycasts = false;
        }

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (cameraFollow != null && crosshair != null)
            crosshair.SetActive(cameraFollow.IsFirstPerson());

        // Κλείσιμο Help με ESC ή H
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.H)) && isHelpVisible)
        {
            CloseHelp();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                ShowPauseMenu();
            else
                ResumeGame();
        }
        else if (Input.GetKeyDown(KeyCode.H) && !isPaused && !isHelpVisible)
        {
            OpenHelp();
        }

        // Inventory toggle με I
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpen = !isInventoryOpen;

            if (inventoryPanel != null)
            {
                inventoryPanel.alpha = isInventoryOpen ? 1f : 0f;
                inventoryPanel.interactable = isInventoryOpen;
                inventoryPanel.blocksRaycasts = isInventoryOpen;
            }

            Cursor.visible = isInventoryOpen || cameraFollow.IsFirstPerson();
            Cursor.lockState = (isInventoryOpen || cameraFollow.IsFirstPerson()) ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void OpenHelp()
    {
        isHelpVisible = true;

        if (helpCanvasGroup != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeCanvasGroup(helpCanvasGroup, helpCanvasGroup.alpha, 1f));
            helpCanvasGroup.interactable = true;
            helpCanvasGroup.blocksRaycasts = true;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void CloseHelp()
    {
        isHelpVisible = false;

        if (helpCanvasGroup != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeCanvasGroup(helpCanvasGroup, helpCanvasGroup.alpha, 0f));
            helpCanvasGroup.interactable = false;
            helpCanvasGroup.blocksRaycasts = false;
        }

        if (cameFromPause)
        {
            cameFromPause = false;
            Invoke("ShowPauseMenu", 0.01f); // Καθυστέρηση 1 frame
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;

            Cursor.visible = cameraFollow.IsFirstPerson();
            Cursor.lockState = cameraFollow.IsFirstPerson() ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (pauseMenuCanvasGroup != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeCanvasGroup(pauseMenuCanvasGroup, pauseMenuCanvasGroup.alpha, 0f));
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;
        }

        Time.timeScale = 1f;
        Cursor.lockState = cameraFollow.IsFirstPerson() ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !cameraFollow.IsFirstPerson();
    }

    private void ShowPauseMenu()
    {
        isPaused = true;

        if (pauseMenuCanvasGroup != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeCanvasGroup(pauseMenuCanvasGroup, pauseMenuCanvasGroup.alpha, 1f));
            pauseMenuCanvasGroup.interactable = true;
            pauseMenuCanvasGroup.blocksRaycasts = true;
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool IsHelpMenuOpen()
    {
        return isHelpVisible;
    }

    public void QuitGame()
    {
        Debug.Log("Exit pressed");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private System.Collections.IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Lerp(start, end, t / fadeDuration);
            yield return null;
        }
        cg.alpha = end;
    }
}
