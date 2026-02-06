using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private const int inventorySize = 1;
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private string itemTagPickup = "PuzzlePiece";
    [SerializeField] private string itemTagDeposit = "PuzzleInteractable";
    [SerializeField] private float maxDistance = 10f;
    private void Awake()
    {
        inventorySlots = new InventorySlot[inventorySize];
        for(int i = 0; i < inventorySize; i++)
        {
            inventorySlots[i] = new InventorySlot(null);
        }
    }
    public void RaycastInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitObject, maxDistance))
        {
            GameObject objectPuzzle = hitObject.collider.gameObject;
            GameObject objectInteracting = hitObject.collider.gameObject;
            if (objectPuzzle.CompareTag(itemTagPickup))
            {
                AddItem(objectPuzzle);
            }
            else if (objectInteracting.CompareTag(itemTagDeposit))
            {
                //TODO
            }
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
    public bool RemoveItem(GameObject itemRemoved)
    {
        for (int i = 0;i < inventorySlots.Length; i++)
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
