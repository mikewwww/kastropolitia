using UnityEngine;
using TMPro;

public class QuestUIManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public void ShowQuestInfo(QuestData quest)
    {
        titleText.text = quest.questName;
        descriptionText.text = quest.description;
    }
}
