using System.Globalization;
using UnityEngine;

[CreateAssetMenu]
public class DialogueSystem : ScriptableObject
{
    public DialogueNPCS[] characters;

    public string randomNPCName;
    public Sprite randomNPCPortrait;

    [Header("Dialogue")]
    [TextArea]
    public string[] dialogue;

    public string[] optionText;

    public DialogueSystem option0; // depends how many options we have
    public DialogueSystem option1;
    public DialogueSystem option2;

}
