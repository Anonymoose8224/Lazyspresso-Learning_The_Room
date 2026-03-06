using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerCamera plCam;
    [SerializeField] PlayerMove plMove;
    PlayerControls plControls;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private string itemTagPickup = "PuzzlePiece";
    [SerializeField] private string itemTagDeposit = "PuzzleInteractable";
    [SerializeField] private string doorPuzzle = "Door";
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] TempPauseMenu pauseMenu;

    Vector2 moveInput;
    bool isActive = true;

    private void Awake()
    {
        plControls = new PlayerControls();
        plControls.Enable();

        plControls.FPPlayer.Move.performed += Move;
        plControls.FPPlayer.Move.canceled += Move;

        plControls.UI.Pause.performed += TogglePause;

        plControls.FPPlayer.Look.performed += Look;

        plControls.FPPlayer.Interact.performed += Interact;

        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;
        inventorySystem = GetComponent<InventorySystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive || pauseMenu.IsPaused() || plMove == null)
            return;

        plMove.Move(moveInput);
       

    }

    private void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        Debug.Log("Move value: " + moveInput);
    }

    private void Look(InputAction.CallbackContext ctx)
    {
        if (pauseMenu.IsPaused() || plMove == null || plCam == null)
            return;

        Vector2 inputValues = ctx.ReadValue<Vector2>();
        plMove.Rotate(inputValues.x);
        plCam.Rotate(inputValues.y);
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            return;

        GameObject hitObj = hit.collider.gameObject;

        // Pickup item
        if (hitObj.CompareTag(itemTagPickup))
        {
            inventorySystem.AddItem(hitObj);
            return;
        }

        // Try puzzle on this object or its parent
        PuzzleSolving puzzle = hitObj.GetComponent<PuzzleSolving>();

        if (puzzle == null)
            puzzle = hitObj.GetComponentInParent<PuzzleSolving>();

        if (puzzle != null)
        {
            puzzle.PuzzleSystem();
        }


        if (hitObj.CompareTag(doorPuzzle))
        {
            PuzzleSolving door = hitObj.GetComponentInParent<PuzzleSolving>();

            if (door != null)
            {
                door.PuzzleSystem();
            }
        }
    }

    private void TogglePause(InputAction.CallbackContext ctx)
    {
        if (pauseMenu != null)
            pauseMenu.TogglePause();
    }

    private void OnDisable()
    {
        plControls.FPPlayer.Disable();
        plControls.UI.Disable();
    }
}
