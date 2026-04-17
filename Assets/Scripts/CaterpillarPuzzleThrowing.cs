using UnityEngine;

public class CaterpillarPuzzleThrowing : MonoBehaviour
{
    [SerializeField] private GameObject yellowRing;
    [SerializeField] private GameObject redRing;
    [SerializeField] private GameObject greenRing;
    [SerializeField] private GameObject blueRing;
    [SerializeField] private GameObject startingRing;

    private void Awake()
    {
        startingRing = blueRing;
    }
}
