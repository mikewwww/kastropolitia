using UnityEngine;

[CreateAssetMenu(menuName = "Quests/QuestData")]
public class QuestData : ScriptableObject
{
    public string questName;
    public string description;
    public bool isAccepted = false;
    public bool isCompleted = false;

    public void Accept()
    {
        isAccepted = true;
        Debug.Log($"🟡 Αποστολή '{questName}' έγινε αποδεκτή.");
    }

    public void Complete()
    {
        isCompleted = true;
        Debug.Log($"🏁 Αποστολή '{questName}' ολοκληρώθηκε.");
    }
}
