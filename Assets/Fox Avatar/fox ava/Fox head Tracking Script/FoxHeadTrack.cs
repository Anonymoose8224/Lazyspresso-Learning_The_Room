using UnityEngine;

public class SimpleHeadTurn : MonoBehaviour
{
    [Header("References")]
    public Transform headObject;        // The fox's head
    public Transform player;            // The player
    public float detectionRange = 10f;  // How close player needs to be

    [Header("Turn Settings")]
    public float turnSpeed = 3f;        // How fast head turns
    public float maxTurnAngle = 70f;    // How far head can turn left/right

    private float targetAngle = 0f;
    private float currentAngle = 0f;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (headObject == null || player == null) return;

        // Check distance to player
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            // Calculate angle to player
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0; // Ignore height difference

            float angle = Vector3.SignedAngle(transform.forward, directionToPlayer, Vector3.up);

            // Limit how far the head can turn
            targetAngle = Mathf.Clamp(angle, -maxTurnAngle, maxTurnAngle);
        }
        else
        {
            // Return to facing forward when player leaves
            targetAngle = 0;
        }

        // Smoothly rotate head towards target angle
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * turnSpeed);

        // Apply rotation (only left/right turning)
        headObject.localRotation = Quaternion.Euler(0, currentAngle, 0);
    }

    // Draw the detection range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}