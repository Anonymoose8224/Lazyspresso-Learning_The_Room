using UnityEngine;

public class RingPuzzle : MonoBehaviour
{
    public Transform[] slots; 
    public InventorySystem inventory;

    public void PlaceRings()
    {
        for (int i = 0; i < inventory.inventorySlots.Length; i++)
        {
            InventorySlot invSlot = inventory.inventorySlots[i];

            if (!invSlot.isEmpty)
            {
                Ring ring = invSlot.item.GetComponent<Ring>();

                if (ring != null)
                {
                    int index = ring.slotIndex;

                    ring.transform.position = slots[index].position;
                    ring.transform.rotation = slots[index].rotation;
                    ring.transform.parent = slots[index];
                    ring.gameObject.SetActive(true);

                    invSlot.Clear();
                }
            }
        }
    }
}