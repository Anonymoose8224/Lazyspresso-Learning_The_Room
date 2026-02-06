using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] UnityEvent onInteract;

    public void OnInteract()
    {
        onInteract?.Invoke();
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitObject))
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
