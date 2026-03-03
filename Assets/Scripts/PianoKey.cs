using UnityEngine;
using UnityEngine.InputSystem;

public class PianoKey : MonoBehaviour
{
    [SerializeField] private PianoBrain pianoBran;
    [SerializeField] private int noteNbr;

    public void PlayNote()
    {
        pianoBran.ReadNote(noteNbr);
    }

    
}
