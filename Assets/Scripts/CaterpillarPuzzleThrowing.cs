using System;
using UnityEngine;

public class CaterpillarPuzzleThrowing : MonoBehaviour
{
    [SerializeField] private GameObject[] rings;
    [SerializeField] private GameObject startingRing;
    [SerializeField] private int counter = 0;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private RingPuzzleData[] switchingRings;
    [SerializeField] private bool inThrowingArea = false;

    private void Start()
{
    inThrowingArea = false;
    counter = 0;

    for (int i = 0; i < switchingRings.Length; i++)
    {
        switchingRings[i].onReachedArea += SwitchRings;
    }

    startingRing = rings[counter];
}

    public void SwitchRings(bool reached)
    {
        if (reached == true)
        {
            counter++;

            if (counter >= rings.Length)
            {
                //COMPLETED PUZZLE AREA
                Debug.Log("All rings completed!");
                return;
            }

            startingRing = rings[counter];

            for (int i = 0; i < inventorySystem.inventorySlots.Length; i++)
            {
                if (inventorySystem.inventorySlots[i].item != null &&
                    startingRing.name == inventorySystem.inventorySlots[i].item.name)
                {
                    inThrowingArea = true;
                    Debug.Log("Ring in inventory, showing it");
                    ShowRing();
                    return;
                }
                else
                {
                    Debug.Log("Ring not in inventory");
                }
            }
        }
    }
    public void ShowRing()
    {
        Debug.Log("Entered Caterpillar Game with the ring in inventory, showing in hand!");

        startingRing.SetActive(true);

        RingPuzzleData currentRing = startingRing.GetComponent<RingPuzzleData>();
        currentRing.ResetPosition();
    }
    public void HideRing()
    {
        Debug.Log("Exited Caterpillar Game with the ring in inventory, hiding in hand!");
        startingRing.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TriggerAreaForHand")
        {
            for (int i = 0; i < inventorySystem.inventorySlots.Length; i++)
            {
                if (startingRing.name == inventorySystem.inventorySlots[i].item.name)
                {
                    inThrowingArea = true;
                    Debug.Log("Ring in inventory, showing it");
                    ShowRing();
                }
                else
                {
                    Debug.Log("Ring not in inventory");
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "TriggerAreaForHand")
        {
            inThrowingArea = false;
            HideRing();
        }
    }
    public void ThrowRing()
    {
        if(inThrowingArea == true)
        {
            Debug.Log("In throwable area!");
            RingPuzzleData currentRing = startingRing.GetComponent<RingPuzzleData>();
            currentRing.RingThrown();
        }
        else
        {
            Debug.Log("Not in throwable area!");
        }
    }
}
