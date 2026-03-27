using UnityEngine;

public abstract class Baseinteractable : MonoBehaviour
{
    public abstract void Interact(Ray ray, float maxDistance);
}
