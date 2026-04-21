using System;
using UnityEngine;

public class RingPuzzleData : MonoBehaviour, IThrowable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float pushForce = 5f;
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [SerializeField] private bool thrown = false;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private bool throwable = false;
    public event Action<bool> onReachedArea;
     private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        this.gameObject.transform.position = startPosition;
        rb.useGravity = false;
        this.gameObject.SetActive(false);
    }
    public void ShowRing()
    {
        Debug.Log("Entered Caterpillar Game with the ring in inventory, showing in hand!");
        this.gameObject.SetActive (true);
    }
    public void HideRing()
    {
        Debug.Log("Exited Caterpillar Game with the ring in inventory, hiding in hand!");
        this.gameObject.SetActive (false);
    }

    public void RingThrown()
    {
        Debug.Log("Thrown the ring!");
        thrown = true;
        rb.useGravity = true;
        rb.AddForce(Vector3.forward * pushForce);
    }

    public void FailThrow()
    {
        if(thrown == true)
        {
            Debug.Log("Failed to get ring in success area! Returning to hand!");
            thrown = false;
            rb.useGravity = false;
            this.gameObject.transform.position = startPosition;
        }
    }
    public void SuccessThrow()
    {
        if(thrown == true)
        {
            Debug.Log("Got ring in success area! Destroying the ring and showing the success ring!");
            thrown = false;
            rb.useGravity = false;
            onReachedArea?.Invoke(true);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TriggerAreaForHand")
        {
            for (int i = 0; i < inventorySystem.inventorySlots.Length; i++)
            {
                if (this.gameObject.name == inventorySystem.inventorySlots[i].item.name)
                {
                    ShowRing();
                }
            }
        }
        else if (other.gameObject.name == "TriggerAreaForComplete")
        {
            SuccessThrow();
        }
        else if (other.gameObject.name == "TriggerAreaForFail")
        {
            FailThrow();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "TriggerAreaForHand")
        {
            HideRing();
        }
    }
}
