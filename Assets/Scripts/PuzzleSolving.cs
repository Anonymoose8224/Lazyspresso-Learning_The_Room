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

    //The main puzzle logic
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
            /*if(hasAnimation == true && puzzleCompleted == true)
            {
                clip.Play("CompletedCaterpillarPuzzle");
            }*/
            GiveItemToPlayer();
            GiveSpecialItem();
            
            //Disables the door is door (Will need to be changed eventually)
            //door.SetActive(false);
        }
        else
        {
            Debug.Log("Puzzle Incomplete!");
        }

    }

    //Giving part of the code, can be separated (probably) as "normal" and "special"
    public void GiveSpecialItem()
    {
        if (specialItemGiven != null)
        {
            if (specialItemGiven.CompareTag("SpecialItem"))
                inventorySystem.AddItemSpecial(specialItemGiven);

            else
                inventorySystem.AddItem(specialItemGiven);
        }

        if (inventorySystem.IsFull() || door.CompareTag("Door"))
        {
            Debug.Log($"{door} Is Open");
            door.SetActive(false);
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

    //A checking part of the code, checks both normal and special items, will most likely need to be optimized for better usage
    public bool CheckingRequiredItems()
    {
        itemsSubmittedNumber = 0;

        for (int i = 0; i < itemsSubmitted.Length; i++)
        {
            itemsSubmitted[i] = null;
        }

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

        foreach (GameObject requiredSpecialItem in requiredSpecialItems)
        {
            if (!HasSpecialItem(requiredSpecialItem))
            {
                Debug.Log($"Missing special required item: {requiredSpecialItem.name}");
                return false;
            }
        }

        foreach (GameObject requiredItem in requiredItems)
        {
            itemsSubmitted[itemsSubmittedNumber] = requiredItem;
            inventorySystem.RemoveItem(requiredItem);
            itemsSubmittedNumber++;
        }

        return true;
    }

    //Part of checking, can be part of "special"
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
