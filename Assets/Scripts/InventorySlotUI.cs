using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;


    private void Awake()
    {
        if (itemIcon == null)
        {
            Debug.Log("The Ui is Empty");
            itemIcon = GetComponent<Image>();
        }
    }

    public void SetItem(GameObject item)
    {
        if (item == null)
        {
            Debug.Log("Clearing the slots");
            ClearSlot();
            return;
        }

        InventoryItemData itemData = item.GetComponent<InventoryItemData>();

        if (itemData != null && itemData.inventorySprite != null)
        {
            itemIcon.sprite = itemData.inventorySprite;
            itemIcon.enabled = true;
        }
        else
        {
            ClearSlot();
            Debug.LogWarning($"Missing InventoryItemData or sprite on {item.name}");
        }
    }

    public void ClearSlot()
    {
        itemIcon.sprite = null;
        itemIcon.enabled = false;
    }

    public bool ShowImage() 
    {
        if (itemIcon == null)
            return false;
        else
            return true;
        
    }
}