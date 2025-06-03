using UnityEngine;

public class QuestGiverNPC : MonoBehaviour
{
    public QuestData quest;
    public QuestUIManager uiManager;

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"🔍 Πατήθηκε E στον QuestGiver - quest = {quest}, completed = {quest?.isCompleted}");

            if (quest != null && !quest.isCompleted)
            {
                InteractionPrompt.Instance.HidePrompt();
                uiManager.ShowQuestGiverUI(quest, this);
            }
            else
            {
                Debug.LogWarning("⚠️ Quest is null ή ήδη completed!");
            }
        }
    }

    public void AcceptQuest()
    {
        Debug.Log($"📥 AcceptQuest CALLED for quest: {quest?.questName}");

        if (quest != null)
        {
            quest.Accept();  // Αυτό αλλάζει το isAccepted
            uiManager.HideQuestGiverUI();
        }
        else
        {
            Debug.LogWarning("⚠️ Quest is NULL!");
        }
    }


    public void CloseUI()
    {
        uiManager.HideQuestGiverUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = true;
        if (!quest.isAccepted && !quest.isCompleted)
        {
            InteractionPrompt.Instance.ShowPrompt("Πάτα [E] για αποστολή");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            uiManager.HideQuestGiverUI();
            InteractionPrompt.Instance.HidePrompt();
        }
    }
}
