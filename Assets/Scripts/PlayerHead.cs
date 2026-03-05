using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHead : MonoBehaviour
{
    [SerializeField] PlayerCamera plCam;
    [SerializeField] PlayerMove plMove;
    PlayerControls plControls;
    //[SerializeField] Baseinteractable plInteractable;
    [SerializeField] private float maxDistance = 10f;

    Vector2 moveInput;
    public bool isActive = true;

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
        if (plCam.CanLook == true)
        {
            Vector2 inputValues = ctx.ReadValue<Vector2>();
            plMove.Rotate(inputValues.x);
            plCam.Rotate(inputValues.y);
        }
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        Ray ray = new Ray(plCam.transform.position, plCam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            Baseinteractable interact = hit.collider.GetComponent<Baseinteractable>();
        
            if(interact != null)
            {
                interact.Interact(ray, maxDistance);
            }
        
        }
    }

    public void OnDrawGizmos()
    {
        
    }
}
