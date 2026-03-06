using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolving : MonoBehaviour
{
    [SerializeField] private List<GameObject> requiredItems;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private int requiredItemsNumber;
    [SerializeField] private GameObject[] itemsSubmitted;
    [SerializeField] private int itemsSubmittedNumber;
    [SerializeField] private GameObject[] itemsGiven;
    [SerializeField] private GameObject specialItemGiven;
    [SerializeField] private bool puzzleCompleted = false;
    [SerializeField] private GameObject door;
    [SerializeField] private List<GameObject> requiredSpecialItems;
    private void Awake()
    {
        requiredItemsNumber = requiredItems.Count;
        itemsSubmitted = new GameObject[requiredItemsNumber];
        itemsSubmittedNumber = 0;
    }

    public void PuzzleSystem()
    {
        if (puzzleCompleted)
        {
            Debug.Log("Puzzle already complete!");
            return;
        }

        if (CheckingRequiredItems())
        {
            puzzleCompleted = true;
            Debug.Log("Puzzle Complete!");
            GiveItemToPlayer();
            GiveSpecialItem();

            //Disables the door is door (Will need to be changed eventually)
            door.SetActive(false);
        }
        else
        {
            Debug.Log("Puzzle Incomplete!");
        }
    }
    public void GiveSpecialItem()
    {
        if (specialItemGiven != null)
        {
            inventorySystem.AddItemSpecial(specialItemGiven);
        }
    }
    public void GiveItemToPlayer()
    {
        int emptySlots = 0;

        for (int i = 0; i < inventorySystem.inventorySlots.Length; i++)
        {
            if (inventorySystem.inventorySlots[i].isEmpty)
            {
                emptySlots++;
            }
        }

        if (emptySlots < itemsGiven.Length)
        {
            Debug.Log("Inventory too full to receive reward!");
            return;
        }

        Debug.Log("Space in inventory! You will receive the reward!");

        foreach (GameObject item in itemsGiven)
        {
            inventorySystem.AddItem(item);
        }
    }
    public bool CheckingRequiredItems()
    {
        itemsSubmittedNumber = 0;

        for (int i = 0; i < itemsSubmitted.Length; i++)
        {
            itemsSubmitted[i] = null;
        }

        // First make sure all normal items exist
        foreach (GameObject requiredItem in requiredItems)
        {
            bool foundItem = false;

            for (int i = 0; i < inventorySystem.inventorySlots.Length; i++)
            {
                if (!inventorySystem.inventorySlots[i].isEmpty &&
                    inventorySystem.inventorySlots[i].item == requiredItem)
                {
                    foundItem = true;
                    break;
                }
            }

            if (!foundItem)
            {
                Debug.Log($"Missing normal required item: {requiredItem.name}");
                return false;
            }
        }

        // Then make sure all special items exist
        foreach (GameObject requiredSpecialItem in requiredSpecialItems)
        {
            if (!HasSpecialItem(requiredSpecialItem))
            {
                Debug.Log($"Missing special required item: {requiredSpecialItem.name}");
                return false;
            }
        }

        // Only now remove the normal items
        foreach (GameObject requiredItem in requiredItems)
        {
            itemsSubmitted[itemsSubmittedNumber] = requiredItem;
            inventorySystem.RemoveItem(requiredItem);
            itemsSubmittedNumber++;
        }

        return true;
    }
    public bool HasSpecialItem(GameObject requiredSpecialItem)
    {
        for (int i = 0; i < inventorySystem.specialInventory.Length; i++)
        {
            if (!inventorySystem.specialInventory[i].isEmpty &&
                inventorySystem.specialInventory[i].item == requiredSpecialItem)
            {
                return true;
            }
        }

        return false;
    }
}
