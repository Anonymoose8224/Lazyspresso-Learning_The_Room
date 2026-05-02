using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public const int inventorySize = 10;
    public const int specialInventorySize = 4;
    public InventorySlot[] inventorySlots;
    public InventorySlot[] specialInventory;
    [SerializeField] private int count;
    [SerializeField] private InventoryUI inventoryUI;
    private void Awake()
    {
        {
            inventorySlots = new InventorySlot[inventorySize];
            specialInventory = new InventorySlot[specialInventorySize];

            for (int i = 0; i < inventorySize; i++)
            {
                inventorySlots[i] = new InventorySlot(null);
            }

            for (int i = 0; i < specialInventorySize; i++)
            {
                specialInventory[i] = new InventorySlot(null);
            }
        }
    }

    public bool AddItem(GameObject itemAdded)
    {
        if (itemAdded == null)
        {
            Debug.Log("Cannot add item: itemAdded is null");
            return false;
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].isEmpty)
            {
                inventorySlots[i].item = itemAdded;
                itemAdded.SetActive(false);
                Debug.Log($"{inventorySlots[i].item.name} was added to the inventory");
                inventoryUI.RefreshUI();
                return true;
            }
        }

        Debug.Log($"Inventory is full, couldn't add {itemAdded.name}");
        return false;
    }
    public bool AddItemSpecial(GameObject itemAdded)
    {
        if (itemAdded == null)
        {
            Debug.Log("Cannot add special item: itemAdded is null");
            return false;
        }

        for (int i = 0; i < specialInventory.Length; i++)
        {
            if (specialInventory[i].isEmpty)
            {
                specialInventory[i].item = itemAdded;
                itemAdded.SetActive(false);
                Debug.Log($"{specialInventory[i].item.name} was added to the special inventory");
                inventoryUI.RefreshUI();
                return true;
            }
        }

        Debug.Log($"Special inventory is full, couldn't add {itemAdded.name}");
        return false;
    }
    public bool RemoveItem(GameObject itemRemoved)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (!inventorySlots[i].isEmpty && inventorySlots[i].item == itemRemoved)
            {
                string removedItemName = inventorySlots[i].item.name;
                inventorySlots[i].Clear();
                Debug.Log($"{removedItemName} Removed");
                inventoryUI.RefreshUI();
                return true;
            }
        }

        Debug.Log($"{itemRemoved.name} was not found in inventory");
        return false;
    }

    public bool IsFull() 
    {
        foreach (InventorySlot item in specialInventory)
        {
            if (item.isEmpty)
            {
                Debug.Log("Its not full");
                return false;
            }
        }

        Debug.Log("It is Full");
        return true;
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
