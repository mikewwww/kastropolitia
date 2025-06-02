using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectableShopItemUI : MonoBehaviour
{
    public ShopItem item;
    public Image icon;
    public TMP_Text priceText;
    public Button selectButton;

    private ShopUIManager shopManager;

    private void Start()
    {
        priceText.text = item.price.ToString();
        icon.sprite = item.icon;

        shopManager = FindObjectOfType<ShopUIManager>();

        selectButton.onClick.AddListener(OnSelected);
    }

    private void OnSelected()
    {
        shopManager.SelectItem(item);
        Debug.Log("Selected item: " + item.name);
    }
}
