using UnityEngine;

public class CaterpillarPuzzleThrowing : MonoBehaviour
{
    [SerializeField] private GameObject[] rings;
    [SerializeField] private GameObject startingRing;
    [SerializeField] private int counter = 0;

    [SerializeField] private InventorySystem inventorySystem;
    //[SerializeField] private RingPuzzleData[] switchingRings;
    [SerializeField] private PuzzleSolving PuzzSyst;

    [SerializeField] private Collider triggerAreaForHand;
    [SerializeField] private Collider triggerAreaForFail;
    [SerializeField] private Collider triggerAreaForComplete;

    private bool puzzleActive = false;
    private bool inThrowingArea = false;
    private bool isFinished = false;
    private bool isRingFlying = false;

    private void Start()
    {
        counter = 0;
        puzzleActive = false;
        inThrowingArea = false;
        isFinished = false;
        isRingFlying = false;


        /*for (int i = 0; i < switchingRings.Length; i++)
        {
            switchingRings[i].onReachedArea += SwitchRings;
        }*/
        for (int i = 0; i < rings.Length; i++)
        {
            RingPuzzleData ringData = rings[i].GetComponent<RingPuzzleData>();

            if (ringData != null)
            {
                ringData.onReachedArea += SwitchRings;
            }
        }

        startingRing = rings[counter];
        SetPuzzleState(false);
    }

   
    public void ActivatePuzzle()
    {
        if (isFinished) return;

        Debug.Log("Caterpillar puzzle activated");
        puzzleActive = true;

        CheckIfInZone();
        if (triggerAreaForHand != null)
            triggerAreaForHand.enabled = true;
    }

    public bool IsFinished()
    {
        return isFinished;
    }

    private void CheckIfInZone()
    {
        Collider[] hitColliders = Physics.OverlapBox(triggerAreaForHand.bounds.center, triggerAreaForHand.bounds.extents, Quaternion.identity);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Player"))
            {
                EnterThrowingArea();
                return;
            }
        }
    }

    public void DeactivatePuzzle()
    {
        Debug.Log("Caterpillar puzzle deactivated");
        SetPuzzleState(false);
        HideRing();
    }

    private void SetPuzzleState(bool state)
    {
        puzzleActive = state;
        inThrowingArea = false;

        if (triggerAreaForHand != null)
            triggerAreaForHand.enabled = true;

        if (triggerAreaForFail != null)
            triggerAreaForFail.enabled = false;

        if (triggerAreaForComplete != null)
            triggerAreaForComplete.enabled = false;
    }

    private void SetThrowZones(bool state)
    {
        if (triggerAreaForFail != null)
            triggerAreaForFail.enabled = state;

        if (triggerAreaForComplete != null)
            triggerAreaForComplete.enabled = state;
    }

    public void TryShowRing()
    {
        if (!puzzleActive || !inThrowingArea || isFinished || isRingFlying) return;

        for (int i = 0; i < inventorySystem.inventorySlots.Length; i++)
        {
            var slot = inventorySystem.inventorySlots[i];

            if (inventorySystem.inventorySlots[i].item != null &&
                startingRing.name == inventorySystem.inventorySlots[i].item.name)
            {
                Debug.Log("Ring in inventory, showing it");
                ShowRing();
                return;
            }
        }
    }

    public void SwitchRings(bool reached)
    {
        if (!reached) return;
        Debug.Log("SwitchRings called");
        isRingFlying = false;
        HideRing();
        counter++;

        if (counter >= rings.Length)
        {
            Debug.Log("All rings completed!");
            FinishPuzzle(); 
            return;
        }
        Debug.Log("Counter is now: " + counter);
        startingRing = rings[counter];

        /*for (int i = 0; i < inventorySystem.inventorySlots.Length; i++)
        {
            if (inventorySystem.inventorySlots[i].item != null &&
                startingRing.name == inventorySystem.inventorySlots[i].item.name)
            {
                ShowRing();
                return;
            }
        }

        Debug.Log("Ring not in inventory");*/
        TryShowRing();
    }
    private void FinishPuzzle()
    {
        Debug.Log("All rings completed! Caterpillar is satisfied.");
        isFinished = true;
        puzzleActive = false;

        DeactivatePuzzle();

        Debug.Log("About to reward player...");


        if (PuzzSyst != null)
        {
            Debug.Log("PuzzleSolving found, calling PuzzleSystem()");
            PuzzSyst.GiveSpecialItem();
        }
    }

    public void ShowRing()
    {
        startingRing.SetActive(true);

        RingPuzzleData currentRing = startingRing.GetComponent<RingPuzzleData>();
        if (currentRing != null)
            currentRing.ResetPosition();
    }

    public void HideRing()
    {
        if (startingRing != null)
            startingRing.SetActive(false);
    }

    public void ThrowRing()
    {
        if (!puzzleActive || !inThrowingArea || isFinished) return;

        /*Debug.Log("Throw called. PuzzleActive=" + puzzleActive + "inThrowingArea=" + inThrowingArea);
        if (!puzzleActive)
        {
            Debug.Log("Puzzle not active");
            return;
        }

        if (!inThrowingArea)
        {
            Debug.Log("Not in throwable area!");
            return;
        }*/

        RingPuzzleData currentRing = startingRing.GetComponent<RingPuzzleData>();

        if (currentRing != null)
        {
            isRingFlying = true;
            currentRing.RingThrown();
        }
    }

    public void EnterThrowingArea()
    {
        if (!puzzleActive || isFinished) return;

        inThrowingArea = true;
        SetThrowZones(true);
        TryShowRing();
    }

    public void ExitThrowingArea()
    {
        if (!puzzleActive) return;
        inThrowingArea = false;
        if (!isRingFlying) HideRing();
    }
}