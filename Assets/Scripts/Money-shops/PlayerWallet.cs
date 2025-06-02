using UnityEngine;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour
{
    [Header("Wallet Settings")]
    [SerializeField] private int startingGold = 0;

    public UnityEvent<int> OnGoldChanged;

    public int Gold { get; private set; }

    void Awake()
    {
        Gold = startingGold;
        OnGoldChanged?.Invoke(Gold);
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        OnGoldChanged?.Invoke(Gold);
    }

    public bool SpendGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            OnGoldChanged?.Invoke(Gold);
            return true;
        }
        return false;
    }
    public bool HasEnoughGold(int amount)
    {
        return Gold >= amount;
    }

    public void RemoveGold(int amount)
    {
        Gold -= amount;
        OnGoldChanged?.Invoke(Gold);
    }

}
