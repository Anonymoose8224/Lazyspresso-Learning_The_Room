using Unity.VisualScripting;
using UnityEngine;

public class PianoInteractable : Baseinteractable
{
    [SerializeField] private CameraShift camshift;
    [SerializeField] private Transform pianoCamPoint;
    [SerializeField] private GameObject PianoUI;
    [SerializeField] private GameObject PlayerUI;


    public override void Interact(Ray ray, float maxDistance)
    {
        camshift.EnterCamShift(pianoCamPoint);
        PianoUI.SetActive(true);
        PlayerUI.SetActive(false);
    }

    public void PianoExit()
    {
        camshift.ExitCamShift();
        PianoUI.SetActive(false);
        PlayerUI.SetActive(true);
    }

}
