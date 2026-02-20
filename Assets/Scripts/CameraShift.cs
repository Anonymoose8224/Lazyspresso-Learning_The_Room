using UnityEngine;

public class CameraShift : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerCamera plCam;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private Vector3 origCamPos;
    [SerializeField] private Quaternion origCamRot;
    public void EnterCamShift(Transform CamPoint)
    {
        player.enabled = false;

        origCamPos = plCam.transform.position;
        origCamRot = plCam.transform.rotation;

        plCam.transform.position = CamPoint.position;
        plCam.transform.rotation = CamPoint.rotation;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitCamShift()
    {
        player.enabled = true;
        
        plCam.transform.position = origCamPos;
        plCam.transform.rotation = origCamRot;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
