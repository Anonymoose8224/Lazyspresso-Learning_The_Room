using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public const int inventorySize = 10;
    public const int specialInventorySize = 4;
    public InventorySlot[] inventorySlots;
    public InventorySlot[] specialInventory;

    private void Awake()
    {
        inventorySlots = new InventorySlot[inventorySize];
        specialInventory = new InventorySlot[specialInventorySize];
        for(int i = 0; i < inventorySize; i++)
        {
            inventorySlots[i] = new InventorySlot(null);
        }
    }
    public bool AddItem(GameObject itemAdded)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].isEmpty)
            {
                inventorySlots[i].item = itemAdded;
                itemAdded.SetActive(false);
                Debug.Log($"{inventorySlots[i].item.name} was added to the inventory");
                return true;
            }
        }
        Debug.Log($"Inventory is full, couldn't add {itemAdded.name}");
        return false;
    }
    public bool AddItemSpecial(GameObject itemAdded)
    {
        for (int i = 0; i < specialInventory.Length; i++)
        {
            if (specialInventory[i].isEmpty)
            {
                specialInventory[i].item = itemAdded;
                itemAdded.SetActive(false);
                Debug.Log($"{specialInventory[i].item.name} was added to the inventory");
                return true;
            }
        }
        Debug.Log($"Inventory is full, couldn't add {itemAdded.name}");
        return false;
    }
    public bool RemoveItem(GameObject itemRemoved)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (!inventorySlots[i].isEmpty && inventorySlots[i].item == itemRemoved)
            {
                inventorySlots[i].Clear();
                Debug.Log($"{inventorySlots[i].item.name} Removed");
                return true;
            }
        }
        Debug.Log($"{itemRemoved.name} was not found in inventory");
        return false;
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitObject))
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(hitObject.point, 0.10f);
        }
    }
}
