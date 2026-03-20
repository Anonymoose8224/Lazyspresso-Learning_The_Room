using UnityEngine;

public class CameraShift : MonoBehaviour
{
    [SerializeField] PlayerHead pl;
    [SerializeField] PlayerCamera plCam;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private Vector3 origCamPos;
    [SerializeField] private Quaternion origCamRot;
    public void EnterCamShift(Transform CamPoint)
    {
        pl.isActive = false;

        plCam.CanLook = false;

        origCamPos = plCam.transform.position;
        origCamRot = plCam.transform.rotation;

        plCam.transform.position = CamPoint.position;
        plCam.transform.rotation = CamPoint.rotation;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitCamShift()
    {
        pl.isActive = true;

        plCam.CanLook = true;


        plCam.transform.position = origCamPos;
        plCam.transform.rotation = origCamRot;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
