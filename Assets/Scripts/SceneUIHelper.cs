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
                group.gameObject.SetActive(true); // Ενεργό για να λειτουργεί σωστά το canvasGroup
                group.alpha = 0;
                group.interactable = false;
                group.blocksRaycasts = false;
            }
        }

        // Δεν πειράζουμε το ποντίκι ή το Time.timeScale εδώ
        // Αυτά τα διαχειρίζονται οι MouseManager και UIManager
    }
}
