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
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private PuzzleSolving puzzleSolving;

    Vector2 moveInput;
    bool isActive = true;

    private void Awake()
    {
        plControls = new PlayerControls();
        plControls.Enable();

        plControls.FPPlayer.Move.performed += Move;
        plControls.FPPlayer.Move.canceled += Move;

        plControls.FPPlayer.Look.performed += Look;

        plControls.FPPlayer.Interact.performed += Interact;

        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;
        inventorySystem = GetComponent<InventorySystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
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
        Vector2 inputValues = ctx.ReadValue<Vector2>();
        plMove.Rotate(inputValues.x);
        plCam.Rotate(inputValues.y);
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitObject, maxDistance))
        {
            GameObject objectPuzzle = hitObject.collider.gameObject;
            GameObject objectInteracting = hitObject.collider.gameObject;
            if (objectPuzzle.CompareTag(itemTagPickup))
            {
                inventorySystem.AddItem(objectPuzzle);
            }
            else if (objectInteracting.CompareTag(itemTagDeposit))
            {
                puzzleSolving.PuzzleSystem();
            }
        }
    }
}
