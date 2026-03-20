using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHead : MonoBehaviour
{
    [SerializeField] PlayerCamera plCam;
    [SerializeField] PlayerMove plMove;
    PlayerControls plControls;
    //[SerializeField] Baseinteractable plInteractable;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] TempPauseMenu pauseMenu;

    Vector2 moveInput;
    public bool isActive = true;

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

        if (pauseMenu.IsPaused() || plMove == null || plCam == null || !plCam.CanLook)
        {
            Debug.Log("Game is not pausing!");
            return;
        }

        Vector2 inputValues = ctx.ReadValue<Vector2>();
        plMove.Rotate(inputValues.x);
        plCam.Rotate(inputValues.y);

    }

    private void Interact(InputAction.CallbackContext ctx)
    {

        Ray ray = new Ray(plCam.transform.position, plCam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            Baseinteractable interact = hit.collider.GetComponent<Baseinteractable>();

            if (interact != null)
            {
                interact.Interact(ray, maxDistance);


            }

        }
    }

    private void TogglePause(InputAction.CallbackContext ctx)
    {
        if (pauseMenu != null)
        {
            pauseMenu.TogglePause();
            Debug.Log("Game is toggling paused!");

        }
    }

    private void OnDisable()
    {
        plControls.FPPlayer.Disable();
        plControls.UI.Disable();

    }

    
}
