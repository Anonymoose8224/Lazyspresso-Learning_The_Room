using UnityEngine;

public class NPCInteraction : Baseinteractable
{
    [SerializeField] PuzzleSolving PzSolve;
    public override void Interact(Ray ray, float maxDistance)
    {

        PzSolve.PuzzleSystem();
    }

}
