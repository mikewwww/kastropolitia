using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CanvasGroup pauseMenuCanvasGroup;
    public CanvasGroup helpCanvasGroup;
    public CanvasGroup inventoryCanvasGroup;

    private bool isPaused = false;
    private bool isHelpVisible = false;
    private bool isInventoryOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsOtherUIOpen())
        {
            if (!isPaused)
                ShowPauseMenu();
            else
                ResumeGame();
        }

        if (Input.GetKeyDown(KeyCode.H) && !isPaused && !IsOtherUIOpen())
        {
            if (!isHelpVisible)
                OpenHelp();
            else
                CloseHelp();
        }

        if (Input.GetKeyDown(KeyCode.I) && !isPaused && !IsOtherUIOpen())
        {
            ToggleInventory();
        }
    }

    private bool IsOtherUIOpen()
    {
        GameObject shop = GameObject.Find("ShopUI");
        GameObject questGiver = GameObject.Find("QuestUI_Giver");
        GameObject questComplete = GameObject.Find("QuestUI_Complete");

        return (shop != null && shop.activeSelf) ||
               (questGiver != null && questGiver.activeSelf) ||
               (questComplete != null && questComplete.activeSelf);
    }

    public void ShowPauseMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;
        SetCanvasGroup(pauseMenuCanvasGroup, true);
        FindObjectOfType<MouseManager>()?.SetUIOpen(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SetCanvasGroup(pauseMenuCanvasGroup, false);
        CheckAnyUIStillOpen();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenHelp()
    {
        isHelpVisible = true;
        SetCanvasGroup(helpCanvasGroup, true);
        FindObjectOfType<MouseManager>()?.SetUIOpen(true);
    }

    public void CloseHelp()
    {
        isHelpVisible = false;
        SetCanvasGroup(helpCanvasGroup, false);
        CheckAnyUIStillOpen();
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        SetCanvasGroup(inventoryCanvasGroup, isInventoryOpen);
        FindObjectOfType<MouseManager>()?.SetUIOpen(isInventoryOpen);
    }

    private void SetCanvasGroup(CanvasGroup group, bool visible)
    {
        if (group == null) return;

        group.gameObject.SetActive(visible);
        group.alpha = visible ? 1 : 0;
        group.interactable = visible;
        group.blocksRaycasts = visible;
    }

    private void CheckAnyUIStillOpen()
    {
        if (!isHelpVisible && !isInventoryOpen && !isPaused && !IsOtherUIOpen())
        {
            FindObjectOfType<MouseManager>()?.SetUIOpen(false);
        }
    }

    public bool IsHelpMenuOpen()
    {
        return isHelpVisible;
    }

    public void ToggleMusic()
    {
        AudioManager.Instance?.ToggleMusic();
    }
}
