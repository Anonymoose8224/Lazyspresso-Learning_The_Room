using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{ 
    public DialogueManager manager;
    public NPCDialogue currentNPCDialogue;

    private void Update()
    {
        if(currentNPCDialogue != null & Input.GetKeyDown(KeyCode.E))
        {
            manager.StartDialogue(currentNPCDialogue.mySystem);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            manager.NextDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PuzzleInteractable"))
        {
            currentNPCDialogue = other.gameObject.GetComponent<NPCDialogue>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PuzzleInteractable"))
        {
            currentNPCDialogue = null;
        }
    }
}
