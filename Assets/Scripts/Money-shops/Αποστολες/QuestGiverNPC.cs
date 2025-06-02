using UnityEngine;

public class QuestGiverNPC : MonoBehaviour
{
    public QuestData quest;
    public GameObject questUI;
    public QuestUIManager uiManager;

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"🔍 Πατήθηκε E στον QuestGiver - quest = {quest}, completed = {quest?.isCompleted}");

            if (quest != null && !quest.isCompleted)
            {
                questUI.SetActive(true);
                uiManager.ShowQuestInfo(quest);
            }
            else
            {
                Debug.LogWarning("⚠️ Quest is null ή ήδη completed!");
            }
        }
    }

    public void AcceptQuest()
    {
        if (quest != null)
        {
            quest.Accept();
            questUI.SetActive(false);
        }
    }

    public void CloseUI()
    {
        if (questUI != null)
            questUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            questUI.SetActive(false);
        }
    }
}
