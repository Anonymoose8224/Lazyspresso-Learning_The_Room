using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FoxHeadTrackZOnly : MonoBehaviour
{
    public Transform head;
    public Transform target;

    public float detectionRange = 8f;
    public float rotationSpeed = 5f;
    public float maxTurnAngle = 60f;

    [SerializeField] float roX = -90;
    [SerializeField] float roY = 90;
    [SerializeField] float roZ = 0;

    private float currentZ = 0f;

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            targetRotation *= Quaternion.Euler(roX, roY, 0f);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
            );
        }
    }


    /* public Transform player;
     public Transform head;

     public float detectionRange = 8f;
     public float rotationSpeed = 5f;
     public float maxTurnAngle = 60f;

     private float currentZ = 0f;

     void Update()
     {
         float distance = Vector3.Distance(transform.position, player.position);

         if (distance <= detectionRange)
         {
             Vector3 direction = player.position - head.position;
             direction.y = 0f;

             float targetAngle = Vector3.SignedAngle(
                 transform.forward,
                 direction,
                 Vector3.up
             );

             targetAngle = Mathf.Clamp(targetAngle, -maxTurnAngle, maxTurnAngle);

             currentZ = Mathf.Lerp(
                 currentZ,
                 targetAngle,
                 Time.deltaTime * rotationSpeed
             );
         }
         else
         {
             currentZ = Mathf.Lerp(
                 currentZ,
                 0f,
                 Time.deltaTime * rotationSpeed
             );
         }

         // ?? LOCK X and Y to 0 always
         head.localRotation = Quaternion.Euler(0f, 0f, currentZ);
     }*/
}