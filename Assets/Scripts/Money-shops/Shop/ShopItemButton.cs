using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemButton : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text priceText;
    public Image iconImage;
    public Image highlightImage;

    private ShopItem shopItem;
    private ShopUIManager shopUI;

    public void Setup(ShopItem item, ShopUIManager ui)
    {
        shopItem = item;
        shopUI = ui;

        nameText.text = item.itemName;
        priceText.text = item.price + "g";
        iconImage.sprite = item.icon;
        highlightImage.enabled = false;

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        shopUI.SelectItem(shopItem, this);
    }

    public void SetSelected(bool selected)
    {
        highlightImage.enabled = selected;
    }
}
