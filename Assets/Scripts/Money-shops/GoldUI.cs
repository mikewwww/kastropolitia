using UnityEngine;
using TMPro; // <- Σημαντικό!

public class GoldUI : MonoBehaviour
{
    public PlayerWallet wallet;
    public TextMeshProUGUI goldText;

    void Start()
    {
        if (wallet != null)
        {
            wallet.OnGoldChanged.AddListener(UpdateUI);
            UpdateUI(wallet.Gold);
        }
        else
        {
            Debug.LogWarning("GoldUI: PlayerWallet is not assigned.");
        }
    }

    void UpdateUI(int amount)
    {
        if (goldText != null)
            goldText.text = "Gold: " + amount;
        else
            Debug.LogWarning("GoldUI: goldText is not assigned.");
    }
}
