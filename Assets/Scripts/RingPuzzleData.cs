using System;
using System.Dynamic;
using UnityEngine;

public class RingPuzzleData : MonoBehaviour, IThrowable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float pushForceForward = .5f;
    [SerializeField] private float pushForceUpwards = .2f;
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [SerializeField] private bool thrown = false;
    [SerializeField] private MeshCollider mc;
    [SerializeField] private GameObject completedRing;
    [SerializeField] private GameObject pickupableRing;
    [SerializeField] private float maxThrowSpeed = 5f;
    [SerializeField] private InventorySystem inventorySystem;
    public event Action<bool> onReachedArea;
     private void Awake()
    {
        completedRing.SetActive(false);
        mc = GetComponent<MeshCollider>();
        mc.enabled = false;
        rb = GetComponent<Rigidbody>();
        this.gameObject.transform.localPosition = startPosition;
        rb.useGravity = false;
        this.gameObject.SetActive(false);
    }

    public void RingThrown()
    {
        ResetPosition();

        mc.enabled = true;
        thrown = true;
        rb.useGravity = true;

        Transform cam = Camera.main.transform;

        Vector3 throwDirection = (cam.forward + Vector3.up * 0.25f).normalized;

        rb.linearVelocity = throwDirection * maxThrowSpeed;
        rb.angularVelocity = Vector3.zero;
    }
    private void FixedUpdate()
    {
        if (thrown && rb.linearVelocity.magnitude > maxThrowSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxThrowSpeed;
        }
    }

    public void FailThrow()
    {
        if(thrown == true)
        {
            Debug.Log("Failed to get ring in success area! Returning to hand!");
            thrown = false;
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            mc.enabled = false;
            ResetPosition();
        }
    }
    public void SuccessThrow()
    {
        if (thrown == true)
        {
            completedRing.SetActive(true);
            mc.enabled = false;

            Debug.Log("Got ring in success area!");

            thrown = false;
            rb.useGravity = false;

            inventorySystem.RemoveItem(pickupableRing);


            onReachedArea?.Invoke(true);

            Destroy(this.gameObject);
        }
    }
    public void ResetPosition()
    {
        this.gameObject.transform.localPosition = startPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (thrown == false)
        {
            return;
        }

        if (other.gameObject.name == "TriggerAreaForComplete")
        {
            SuccessThrow();
        }
        else if (other.gameObject.name == "TriggerAreaForFail")
        {
            FailThrow();
        }
    }
}
