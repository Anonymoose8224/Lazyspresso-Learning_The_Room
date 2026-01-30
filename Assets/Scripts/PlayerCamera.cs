using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform m_Camera;
    [SerializeField] private float minPitch = -90;
    [SerializeField] protected float maxPitch = 90;
    [SerializeField] private float currentPitch = 0;

    public void Rotate(float inputY)
    {
        currentPitch -= inputY;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        m_Camera.localEulerAngles = new Vector3(currentPitch, 0, 0);
    }
}
