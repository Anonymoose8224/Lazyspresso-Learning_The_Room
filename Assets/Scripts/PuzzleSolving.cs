using NUnit.Framework;
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
    private void Awake()
    {
        requiredItemsNumber = requiredItems.Count;
        itemsSubmitted = new GameObject[requiredItemsNumber];
        itemsSubmittedNumber = 0;
    }

    public void PuzzleSystem()
    {
        if (CheckingRequiredItems() == true)
        {
            Debug.Log("Puzzle Complete!");
            GiveItemToPlayer();
            GiveSpecialItem();
        }
        else
        {
            Debug.Log("Puzzle Incomplete!");
        }
    }
    public void GiveSpecialItem()
    {
        inventorySystem.AddItemSpecial(specialItemGiven);
    }
    public void GiveItemToPlayer()
    {
        int counterCheckEmpty = 0;
        for (int i = 0; i < inventorySystem.inventorySlots.Length; i++)
        {
            if (!inventorySystem.inventorySlots[i].isEmpty)
            {
                counterCheckEmpty++;
            }
            if(counterCheckEmpty == inventorySystem.inventorySlots.Length || counterCheckEmpty > inventorySystem.inventorySlots.Length)
            {
                Debug.Log("Inventory too full to recieve reward!");
                return;
            }
            else if(counterCheckEmpty > 0 && counterCheckEmpty < requiredItemsNumber)
            {
                Debug.Log("Space in inventory! You will recieve the reward!");
                foreach (GameObject item in itemsGiven)
                {
                    inventorySystem.AddItem(item);
                }
                return;
            }
        }
    }
    public bool CheckingRequiredItems()
    {
        foreach (GameObject item in requiredItems)
        {
            for (int i = 0; i < inventorySystem.inventorySlots.Length; i++)
            {
                for(int j = 0; j < requiredItemsNumber; j++)
                {
                    if (inventorySystem.inventorySlots[i].item == item && item != itemsSubmitted[j])
                    {
                        itemsSubmitted[j] = inventorySystem.inventorySlots[i].item;
                        inventorySystem.RemoveItem(item);
                        itemsSubmittedNumber++;
                    }
                    if(itemsSubmittedNumber == requiredItemsNumber)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
