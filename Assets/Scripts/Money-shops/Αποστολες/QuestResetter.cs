using UnityEngine;

[DefaultExecutionOrder(-1000)] // για να εκτελείται νωρίς
public class QuestResetter : MonoBehaviour
{
    [SerializeField] private QuestData[] questsToReset;

    private void OnApplicationQuit()
    {
        ResetQuests();
    }

    private void ResetQuests()
    {
        foreach (QuestData quest in questsToReset)
        {
            quest.isAccepted = false;
            quest.isCompleted = false;
        }

        Debug.Log("[QuestResetter] Όλα τα quests επαναφέρθηκαν.");
    }
}
