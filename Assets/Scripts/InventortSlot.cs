using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public GameObject item;
    public bool isEmpty => item == null;
    public InventorySlot(GameObject item)
    {
        this.item = item;
    }
    public void Clear()
    {
        item = null;
    }
}
