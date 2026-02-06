using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemData itemData;
    public int quantity;
    public bool isEmpty => itemData == null;
    public InventorySlot(ItemData item, int amount)
    {
        itemData = item;
        quantity = amount;
    }
    public void Clear()
    {
        itemData = null;
        quantity = 0;
    }
}
