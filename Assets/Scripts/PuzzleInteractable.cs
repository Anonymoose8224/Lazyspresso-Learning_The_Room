using UnityEngine;

public class PuzzleInteractable : Baseinteractable
{
    
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private AudioPlayer AudClip;
    [SerializeField] private int IndexSound;
    /*
    [SerializeField] private PuzzleSolving puzzleSolving;
    [SerializeField] private string itemTagPickup = "PuzzlePiece";
    [SerializeField] private string itemTagDeposit = "PuzzleInteractable";
    [SerializeField] private string PianoTagPuzzle = "PianoTag";
    */
    public override void Interact(Ray ray, float maxDistance)
    {
        Debug.Log("PlayingSound");
        AudClip.PlaySound(IndexSound);
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
