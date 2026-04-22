using System;
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
    [SerializeField] public bool hasGivenReward = false;

    private static PianoBrain instance;
    private bool inputLocked;

    private List<int> playerComb = new List<int>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void OnEnable()
    {
        Debug.Log($"PianoBrain CREATED: {GetInstanceID()}");
        if (pianoControls == null)
            pianoControls = new PlayerControls();

        pianoControls.Piano.PianoButtonClick.performed += PianoKeyClick;
        pianoControls.Enable();
        playerComb.Clear();
    }

    private void OnDisable()
    {
        pianoControls.Piano.PianoButtonClick.performed -= PianoKeyClick;
        pianoControls.Disable();

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

            if (!hasGivenReward)
            {
                Debug.Log("Inventory item successfully added");
                puzzSolve.PuzzleSystem();
                hasGivenReward = true;

            }
;

        }
    }
    private void PianoKeyClick(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        Debug.Log($"INPUT FIRED on: {GetInstanceID()}");

        if (inputLocked) return;
        inputLocked = true;
        Invoke(nameof(ResetInput), 0.05f);

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
    private void ResetInput()
    {
        inputLocked = false;
    }

}
