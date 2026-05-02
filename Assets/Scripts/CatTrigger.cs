using UnityEngine;

public class CaterpillarTriggerZone : MonoBehaviour
{
    public enum ZoneType
    {
        Hand,
        Fail,
        Complete
    }

    [SerializeField] private ZoneType zoneType;
    [SerializeField] private CaterpillarPuzzleThrowing puzzle;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[{zoneType}] Trigger hit by: {other.name}, Tag: {other.tag}");
        if (other.CompareTag("Player"))
        {
            if (zoneType == ZoneType.Hand) puzzle.EnterThrowingArea();
            return;
        }
        if (!other.CompareTag("PuzzlePiece"))
        {
            return;
        }

        if (other.CompareTag("PuzzlePiece"))
        {
            RingPuzzleData ring = other.GetComponentInParent<RingPuzzleData>();
            if (ring == null)
            {
                Debug.Log("No RingPuzzleData found on: " + other.name);
                return;
            }
            if (!ring.IsThrown())
            {
                return;
            }

            if (zoneType == ZoneType.Complete)
            {
                Debug.Log("RING HIT COMPLETE!");
                ring.SuccessThrow();
            }
            else if (zoneType == ZoneType.Fail)
            {
                Debug.Log("RING HIT FAIL!");
                ring.FailThrow();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (zoneType == ZoneType.Hand)
        {
            puzzle.ExitThrowingArea();
        }
    }
}