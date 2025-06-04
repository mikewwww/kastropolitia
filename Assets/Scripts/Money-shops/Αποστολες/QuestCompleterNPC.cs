using UnityEngine;

public class QuestCompleterNPC : MonoBehaviour
{
    public QuestData quest;
    public QuestUIManager uiManager;
    public PlayerWallet wallet;
    public int rewardAmount = 50;

    [Header("Minimap Icon")]
    [SerializeField] private GameObject questCompleteIcon;

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"🔍 Πατήθηκε E στον Completer - quest = {quest}, accepted = {quest?.isAccepted}, completed = {quest?.isCompleted}");

            if (quest != null && quest.isAccepted && !quest.isCompleted)
            {
                InteractionPrompt.Instance.HidePrompt();
                uiManager.ShowQuestCompleteUI(quest, this);
            }
            else
            {
                Debug.LogWarning("⚠️ Quest δεν είναι έτοιμη για ολοκλήρωση ή έχει ήδη ολοκληρωθεί.");
            }
        }

        UpdateIconVisibility();
    }

    private void UpdateIconVisibility()
    {
        if (questCompleteIcon != null)
        {
            questCompleteIcon.SetActive(quest.isAccepted && !quest.isCompleted);
        }
    }

    public void CompleteQuest()
    {
        Debug.Log("🟢 Πατήθηκε το κουμπί Ολοκλήρωση.");

        if (quest == null)
        {
            Debug.LogError("❌ Quest is null!");
            return;
        }

        if (wallet == null)
        {
            Debug.LogError("❌ Wallet is null!");
            return;
        }

        if (quest.isAccepted && !quest.isCompleted)
        {
            quest.Complete();
            wallet.AddGold(rewardAmount);
            Debug.Log($"✅ Ολοκληρώθηκε η αποστολή '{quest.questName}' και δόθηκαν {rewardAmount} gold!");
        }
        else
        {
            Debug.LogWarning("⚠️ Quest δεν έχει γίνει αποδεκτή ή έχει ήδη ολοκληρωθεί.");
        }

        uiManager.HideQuestCompleteUI();
    }

    public void CloseUI()
    {
        uiManager.HideQuestCompleteUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = true;
        if (quest.isAccepted && !quest.isCompleted)
        {
            InteractionPrompt.Instance.ShowPrompt("Πάτα [E] για ολοκλήρωση αποστολής");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            uiManager.HideQuestCompleteUI();
            InteractionPrompt.Instance.HidePrompt();
        }
    }
}
