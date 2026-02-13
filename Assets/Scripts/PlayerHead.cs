using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerCamera plCam;
    [SerializeField] PlayerMove plMove;
    PlayerControls plControls;

    //Used for the inventory picking up
    [SerializeField] private float maxDistance = 3f;
    private InventorySystem inventorySystem;
    [SerializeField] private string itemTagPickup = "PuzzlePiece";
    [SerializeField] private string itemTagDeposit = "PuzzleInteractable";
    [SerializeField] private ItemData heldItem;
    
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
        if (ctx.performed)
        {
            RaycastInteraction();
        }
    }
    
    private void RaycastInteraction()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        if(Physics.Raycast(ray, out RaycastHit hitObject, maxDistance))
        {
            GameObject objectPuzzle = hitObject.collider.gameObject;
            if(objectPuzzle.CompareTag(itemTagPickup) && heldItem == null)
            {
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitObject))
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(hitObject.point, 0.10f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, hitObject.point);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 50);
        }
    }
}
