using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public GameObject shopUI;
    public PlayerWallet playerWallet;
    public InventoryManager inventoryManager;
    public ShopUIManager shopManager;

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (shopUI.activeSelf)
                CloseShop();
            else
                OpenShop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            CloseShop();
        }
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
    }

    public void BuySelectedItem()
    {
        ShopItem item = shopManager.GetSelectedItem();

        if (item == null)
        {
            Debug.LogWarning("No item selected.");
            return;
        }

        if (!playerWallet.HasEnoughGold(item.price))
        {
            Debug.LogWarning("Not enough gold.");
            return;
        }

        playerWallet.RemoveGold(item.price);
        inventoryManager.AddItem(item.icon);
        Debug.Log($"Item {item.itemName} bought!");
    }
}
