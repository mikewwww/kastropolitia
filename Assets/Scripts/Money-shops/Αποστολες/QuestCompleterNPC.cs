using UnityEngine;

public class QuestCompleterNPC : MonoBehaviour
{
    public QuestData quest;
    public GameObject completionUI;
    public int rewardAmount = 50;

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"🔍 Πατήθηκε E στον Completer - quest = {quest}, accepted = {quest?.isAccepted}, completed = {quest?.isCompleted}");

            if (quest != null && quest.isAccepted && !quest.isCompleted)
            {
                completionUI.SetActive(true);

                QuestUIManager ui = completionUI.GetComponent<QuestUIManager>();
                if (ui != null)
                    ui.ShowQuestInfo(quest);
            }
            else
            {
                Debug.LogWarning("⚠️ Quest δεν είναι έτοιμη για ολοκλήρωση ή έχει ήδη ολοκληρωθεί.");
            }
        }
    }

    public void CompleteQuest(PlayerWallet wallet)
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

        Debug.Log($"🎯 Quest accepted: {quest.isAccepted}, completed: {quest.isCompleted}");

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

        completionUI.SetActive(false);
    }

    public void CloseUI()
    {
        if (completionUI != null)
            completionUI.SetActive(false);
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
            completionUI.SetActive(false);
        }
    }
}
