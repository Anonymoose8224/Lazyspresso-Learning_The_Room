using System;
using System.Dynamic;
using UnityEngine;

public class RingPuzzleData : MonoBehaviour, IThrowable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float pushForceForward = 300f;
    [SerializeField] private float pushForceUpwards = 100f;
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [SerializeField] private bool thrown = false;
    [SerializeField] private MeshCollider mc;
    [SerializeField] private GameObject completedRing;
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
        Debug.Log("Thrown the ring!");
        thrown = true;
        rb.useGravity = true;
        rb.AddRelativeForce(Vector3.forward * pushForceForward);
        rb.AddForce(Vector3.up * pushForceUpwards);
    }

    public void FailThrow()
    {
        if(thrown == true)
        {
            Debug.Log("Failed to get ring in success area! Returning to hand!");
            thrown = false;
            rb.useGravity = false;
            rb.angularVelocity = Vector3.zero;
            mc.enabled = false;
            ResetPosition();
        }
        ResetPosition();
    }
    public void SuccessThrow()
    {
        if(thrown == true)
        {
            completedRing.SetActive(true);
            mc.enabled = false;
            Debug.Log("Got ring in success area! Destroying the ring and showing the success ring!");
            thrown = false;
            rb.useGravity = false;
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
        if (other.gameObject.name == "TriggerAreaForComplete")
        {
            SuccessThrow();
        }
        else if (other.gameObject.name == "TriggerAreaForFail")
        {
            ResetPosition();
            FailThrow();
        }
    }
}
