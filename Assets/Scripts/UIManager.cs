using UnityEngine;
using UnityEngine.UI;

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

        if (Input.GetKeyDown(KeyCode.H) && !isPaused && !isHelpVisible && !IsOtherUIOpen())
        {
            OpenHelp();
        }
        else if (Input.GetKeyDown(KeyCode.H) && isHelpVisible)
        {
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
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SetCanvasGroup(pauseMenuCanvasGroup, false);
    }

    public void OpenHelp()
    {
        isHelpVisible = true;
        SetCanvasGroup(helpCanvasGroup, true);
    }

    public void CloseHelp()
    {
        isHelpVisible = false;
        SetCanvasGroup(helpCanvasGroup, false);
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        SetCanvasGroup(inventoryCanvasGroup, isInventoryOpen);
    }

    private void SetCanvasGroup(CanvasGroup group, bool visible)
    {
        if (group == null) return;

        group.alpha = visible ? 1 : 0;
        group.interactable = visible;
        group.blocksRaycasts = visible;
    }

    public bool IsHelpMenuOpen()
    {
        return isHelpVisible;
    }
}
