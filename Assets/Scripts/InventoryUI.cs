using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private InventorySlotUI[] inventorySlotUI;
    [SerializeField] private InventorySlotUI[] specialSlotUI;

    public void RefreshUI()
    {
        for (int i = 0; i < inventorySlotUI.Length; i++)
        {
            if (i < inventorySystem.inventorySlots.Length)
            {
                inventorySlotUI[i].SetItem(inventorySystem.inventorySlots[i].item);
            }
        }

        for (int i = 0; i < specialSlotUI.Length; i++)
        {
            if (i < inventorySystem.specialInventory.Length)
            {
                specialSlotUI[i].SetItem(inventorySystem.specialInventory[i].item);
            }
        }
    }
}