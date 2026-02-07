using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] private float speed = 5;

    public void Move(Vector2 input)
    {
        Vector3 direction = transform.forward * input.y + transform.right * input.x;

        characterController.Move(direction * speed * Time.deltaTime);

        characterController.SimpleMove(direction * speed * Time.deltaTime);

    }

    public void Rotate(float inputX)
    {
        Vector3 direction = new Vector3(0, inputX, 0);
        transform.Rotate(direction);
    }
}
