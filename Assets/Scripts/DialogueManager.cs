using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] private DialogueSystem[] dialogues;
    public GameObject dialogueBox;
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI dialogueText;

    public DialogueSystem currentSystem;

    private int index;

    public void StartDialogue(DialogueSystem system)
    {
        dialogueBox.SetActive(true);
        currentSystem = system;

        index = 0;

        npcName.text = currentSystem.npcName;

        dialogueText.text = currentSystem.lines[index];
    }

    public void NextDialogue()
    {
        index++;

        if (index < currentSystem.lines.Length) 

        dialogueText.text = currentSystem.lines[index];

        else EndDialogue();
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        currentSystem = null;
    }

    void Start()
    {
        //change text to be =  dialogues[0].dialogue[0]
    }

    
    void Update()
    {
        
    }
}

public enum DialogueNPCS
{
    Denial,
    Anger,
    Bargaining,
    Depression,
    Acceptance,
    Baby,
    Branch
};
