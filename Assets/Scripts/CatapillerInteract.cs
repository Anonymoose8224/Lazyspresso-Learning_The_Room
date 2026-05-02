using UnityEngine;

public class CaterpillarInteraction : Baseinteractable
{
    [SerializeField] private CaterpillarPuzzleThrowing caterpillarPuzzle;

    public override void Interact(Ray ray, float maxDistance)
    {
        if (caterpillarPuzzle.IsFinished())
        {
            Debug.Log("The caterpillar is already full!");
            return;
        }

        caterpillarPuzzle.ActivatePuzzle();
    }
}