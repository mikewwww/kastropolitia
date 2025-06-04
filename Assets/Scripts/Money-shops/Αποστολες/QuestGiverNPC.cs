using UnityEngine;

public class QuestGiverNPC : MonoBehaviour
{
    [SerializeField] private QuestData quest;
    [SerializeField] private QuestUIManager uiManager;

    [Header("Minimap Icon")]
    [SerializeField] private GameObject questStartIcon;

    private bool isPlayerInRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInRange)
        {
            if (!quest.isAccepted && !quest.isCompleted)
            {
                uiManager.ShowQuestGiverUI(quest, this);
                InteractionPrompt.Instance.HidePrompt();
            }
        }

        UpdateIconVisibility();
    }

    private void UpdateIconVisibility()
    {
        if (questStartIcon != null)
        {
            questStartIcon.SetActive(!quest.isAccepted && !quest.isCompleted);
        }
    }

    public void AcceptQuest()
    {
        quest.Accept();
        uiManager.HideQuestGiverUI();
    }

    public void CancelQuest()
    {
        uiManager.HideQuestGiverUI();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerInRange = true;

        if (!quest.isAccepted && !quest.isCompleted)
        {
            InteractionPrompt.Instance.ShowPrompt("Πάτα [E] για αποστολή");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerInRange = false;
        InteractionPrompt.Instance.HidePrompt();
    }
}
