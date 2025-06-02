using UnityEngine;

public class QuestRewarder : MonoBehaviour
{
    public int goldReward = 100;

    public void GiveReward(PlayerWallet wallet)
    {
        if (wallet != null)
        {
            wallet.AddGold(goldReward);
            Debug.Log($"🎉 Ο παίκτης έλαβε {goldReward} gold από αποστολή!");
        }
    }
}
