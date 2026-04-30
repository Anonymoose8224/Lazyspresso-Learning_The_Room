using UnityEngine;

public class NPCInteraction : Baseinteractable
{
    [SerializeField] PuzzleSolving PzSolve;
    [SerializeField] private AudioPlayer AudClip;
    [SerializeField] private int IndexSound;
    public override void Interact(Ray ray, float maxDistance)
    {
        AudClip.PlaySound(IndexSound);
        PzSolve.PuzzleSystem();
    }

}
