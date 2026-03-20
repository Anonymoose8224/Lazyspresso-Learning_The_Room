using UnityEngine;
using UnityEngine.InputSystem;

public class PianoKey : MonoBehaviour
{
    [SerializeField] private PianoBrain pianoBran;
    [SerializeField] private int noteNbr;
    [SerializeField] private Animator anim;

    public void PlayNote()
    {
        pianoBran.ReadNote(noteNbr);

        anim.SetBool("IsClicked", true);
        anim.SetInteger("PianoKey", noteNbr);

    } 


}
