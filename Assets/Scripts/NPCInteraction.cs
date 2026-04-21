using UnityEngine;

public class NPCInteraction : Baseinteractable
{
    [SerializeField] PuzzleSolving PzSolve;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] InventorySystem inventorySystem;

    [SerializeField] DialogueSystem hasItemDialogue;
    [SerializeField] DialogueSystem NoItemDialogue;

    [SerializeField] string requiredItem = "";

    public override void Interact(Ray ray, float maxDistance)
    {
        //if(inventorySystem
       // {
          //  dialogueManager.StartDialogue(hasItemDialogue);
            PzSolve.PuzzleSystem();
        //}
        //else
        //{
       //     dialogueManager.StartDialogue(NoItemDialogue);
       // }
        
    }

}
