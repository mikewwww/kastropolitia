using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    public static QuestUIManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject questGiverUI;
    [SerializeField] private GameObject questCompleteUI;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Optional Scroll")]
    [SerializeField] private ScrollRect descriptionScrollRect;

    private QuestData currentQuest;
    private QuestGiverNPC currentGiver;
    private QuestCompleterNPC currentCompleter;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowQuestGiverUI(QuestData quest, QuestGiverNPC giver)
    {
        currentQuest = quest;
        currentGiver = giver;

        titleText.text = quest.questName;
        descriptionText.text = quest.description;

        questGiverUI.SetActive(true);
        MouseManager.Instance.SetUIOpen(true);
        InteractionPrompt.Instance.HidePrompt();

        if (descriptionScrollRect != null)
            descriptionScrollRect.verticalNormalizedPosition = 1f; // scroll to top
    }

    public void AcceptCurrentQuest()
    {
        if (currentGiver != null)
        {
            currentGiver.AcceptQuest();
        }
    }

    public void HideQuestGiverUI()
    {
        questGiverUI.SetActive(false);
        MouseManager.Instance.SetUIOpen(false);
    }

    public void ShowQuestCompleteUI(QuestData quest, QuestCompleterNPC completer)
    {
        currentQuest = quest;
        currentCompleter = completer;

        titleText.text = quest.questName;
        descriptionText.text = quest.completionDescription; // ✅ εμφανίζει άλλο description

        questCompleteUI.SetActive(true);
        MouseManager.Instance.SetUIOpen(true);
        InteractionPrompt.Instance.HidePrompt();

        if (descriptionScrollRect != null)
            descriptionScrollRect.verticalNormalizedPosition = 1f;
    }

    public void CompleteCurrentQuest()
    {
        if (currentCompleter != null)
        {
            currentCompleter.CompleteQuest();
        }
    }

    public void HideQuestCompleteUI()
    {
        questCompleteUI.SetActive(false);
        MouseManager.Instance.SetUIOpen(false);
    }

    public bool IsAnyQuestUIOpen()
    {
        return questGiverUI.activeSelf || questCompleteUI.activeSelf;
    }
}
