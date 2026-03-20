using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PianoBrain : MonoBehaviour
{
    [SerializeField] private PlayerControls pianoControls;
    [SerializeField] private List<int> correctPswd;
    [SerializeField] private PianoInteractable pianointer;
    [SerializeField] private PuzzleSolving puzzSolve;
    [SerializeField] public bool IsSolved = true;


    private List<int> playerComb = new List<int>();

    private void Awake()
    {
            pianoControls = new PlayerControls();
            pianoControls.Enable();

            pianoControls.Piano.PianoButtonClick.performed += PianoKeyClick;
    }

    public void ReadNote(int note)
    {
        playerComb.Add(note);

        int i = playerComb.Count - 1;


        if (correctPswd[i] != note)
        {
            Debug.Log("wrong note");
            playerComb.Clear();
            return;
        }

        if (playerComb.Count == correctPswd.Count)
        {
            Debug.Log("Correct");
            playerComb.Clear();
            pianointer.PianoExit();

            if (IsSolved)
            {
                Debug.Log("Inventory item succefully added");
                puzzSolve.PuzzleSystem();
                IsSolved = false;

            }
;

        }
    }

    private void PianoKeyClick(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                PianoKey pianoKeys = hit.collider.GetComponent<PianoKey>();
                if (pianoKeys != null)
                {
                    pianoKeys.PlayNote();
                }
            }
        
    
    }

}
