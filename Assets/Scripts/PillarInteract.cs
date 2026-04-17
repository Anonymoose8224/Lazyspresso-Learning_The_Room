using UnityEngine;

public class PillarInteract : Baseinteractable
{
    [SerializeField] PuzzleSolving PzSolve;
    public override void Interact(Ray ray, float maxDistance)
    {
        PzSolve.PuzzleSystem();
    }
}
