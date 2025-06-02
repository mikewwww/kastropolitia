using UnityEngine;

public class SceneUIHelper : MonoBehaviour
{
    public CanvasGroup[] canvasGroupsToHide;

    void Start()
    {
        foreach (CanvasGroup group in canvasGroupsToHide)
        {
            if (group != null)
            {
                group.alpha = 0;
                group.interactable = false;
                group.blocksRaycasts = false;
            }
        }

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
