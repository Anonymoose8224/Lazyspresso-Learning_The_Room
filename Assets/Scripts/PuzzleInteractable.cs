using UnityEngine;

public class PuzzleInteractable : Baseinteractable
{
    
    [SerializeField] private InventorySystem inventorySystem;
    /*
    [SerializeField] private PuzzleSolving puzzleSolving;
    [SerializeField] private string itemTagPickup = "PuzzlePiece";
    [SerializeField] private string itemTagDeposit = "PuzzleInteractable";
    [SerializeField] private string PianoTagPuzzle = "PianoTag";
    */
    public override void Interact(Ray ray, float maxDistance)
    {
        inventorySystem.AddItem(gameObject);

        /*if (Physics.Raycast(ray, out RaycastHit hitObject, maxDistance))
        {
            GameObject objectPuzzle = hitObject.collider.gameObject;
            GameObject objectInteracting = hitObject.collider.gameObject;
            if (objectPuzzle.CompareTag(itemTagPickup))
            {
                inventorySystem.AddItem(objectPuzzle);
            }
            else if (objectInteracting.CompareTag(itemTagDeposit))
            {
                puzzleSolving.PuzzleSystem();
            }

        }*/
    }
}
