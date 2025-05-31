using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotParent;
    public int slotCount = 12;

    private List<InventorySlotUI> slots = new List<InventorySlotUI>();

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotParent);
            var slotUI = slotObj.GetComponent<InventorySlotUI>();
            slotUI.ClearItem();
            slots.Add(slotUI);
        }
    }

    public bool AddItem(Sprite itemSprite)
    {
        foreach (var slot in slots)
        {
            if (!slot.HasItem)
            {
                slot.SetItem(itemSprite);
                return true;
            }
        }

        Debug.Log("Inventory Full");
        return false;
    }
}
