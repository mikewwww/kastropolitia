using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopUIManager : MonoBehaviour
{
    public GameObject shopPanel;
    public Transform itemListParent;
    public GameObject itemButtonPrefab;

    public Button buyButton;
    public Button closeButton;

    public List<ShopItem> shopItems;

    private ShopItem selectedItem;
    private ShopItemButton currentlySelected;

    private InventoryManager inventoryManager;
    private PlayerWallet playerWallet;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        playerWallet = FindObjectOfType<PlayerWallet>();

        PopulateShop();
        buyButton.onClick.AddListener(BuySelectedItem);
        closeButton.onClick.AddListener(CloseShop);
    }

    void PopulateShop()
    {
        foreach (Transform child in itemListParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in shopItems)
        {
            GameObject buttonGO = Instantiate(itemButtonPrefab, itemListParent);
            ShopItemButton button = buttonGO.GetComponent<ShopItemButton>();
            button.Setup(item, this);
        }
    }

    public void SelectItem(ShopItem item, ShopItemButton button)
    {
        selectedItem = item;

        if (currentlySelected != null)
            currentlySelected.SetSelected(false);

        currentlySelected = button;
        currentlySelected.SetSelected(true);
    }

    void BuySelectedItem()
    {
        if (selectedItem == null) return;

        if (playerWallet.SpendGold(selectedItem.price))
        {
            bool added = inventoryManager.AddItem(selectedItem.icon);
            if (!added)
            {
                playerWallet.AddGold(selectedItem.price); // Refund
                Debug.Log("Inventory full! Transaction cancelled.");
            }
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

    void CloseShop()
    {
        shopPanel.SetActive(false);
        MouseManager.Instance.SetUIOpen(false);
    }
    public void OpenShop(List<ShopItem> items)
    {
        shopItems = items;
        PopulateShop();
        shopPanel.SetActive(true);
        MouseManager.Instance.SetUIOpen(true);
    }

}
