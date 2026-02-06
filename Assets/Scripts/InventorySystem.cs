using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private const int inventorySize = 1;
    [SerializeField] private InventorySlot[] inventorySlots;
    private void Awake()
    {
        inventorySlots = new InventorySlot[inventorySize];
        for(int i = 0; i < inventorySize; i++)
        {
            inventorySlots[i] = new InventorySlot(null, 0);
        }
    }
    public bool AddItem(ItemData itemData, int quantity = 1)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].isEmpty)
            {
                inventorySlots[i].itemData = itemData;
                inventorySlots[i].quantity = quantity;
                return true;
            }
        }
        Debug.Log("Inventory is full");
        return false;
    }
}
