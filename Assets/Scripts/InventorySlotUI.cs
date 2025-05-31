using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image itemIcon;

    public bool HasItem => itemIcon != null && itemIcon.enabled;

    public void SetItem(Sprite sprite)
    {
        if (itemIcon != null)
        {
            itemIcon.sprite = sprite;
            itemIcon.enabled = true;
            itemIcon.gameObject.SetActive(true);
        }
    }

    public void ClearItem()
    {
        if (itemIcon != null)
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            itemIcon.gameObject.SetActive(false);
        }
    }
}
