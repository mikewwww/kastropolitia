using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    private ShopItem selectedItem;

    public void SelectItem(ShopItem item)
    {
        selectedItem = item;
    }

    public ShopItem GetSelectedItem()
    {
        return selectedItem;
    }
}
