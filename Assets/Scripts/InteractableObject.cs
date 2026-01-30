using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] UnityEvent onInteract;

    public void OnInteract()
    {
        onInteract?.Invoke();
    }
}
