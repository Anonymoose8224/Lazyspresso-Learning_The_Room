using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] private DialogueSystem[] dialogues;
    public GameObject dialogueBox;
    public TextMeshProUGUI NpcName;
    public TextMeshProUGUI dialogueText;

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
