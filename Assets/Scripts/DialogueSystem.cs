using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "LTR/Create new dialogue", order = 0)]
public class DialogueSystem : ScriptableObject
{
    public DialogueNPCS[] characters;

    public string randomNPCName;
    public Sprite randomNPCPortrait;


    [Header("Dialogue")]
    

    public string npcName;

    [TextArea]
    public string[] lines;

    public string[] optionText;



    public DialogueSystem option0; // depends how many options we have
    public DialogueSystem option1;
    public DialogueSystem option2;

}
