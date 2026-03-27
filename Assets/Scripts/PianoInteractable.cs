using Unity.VisualScripting;
using UnityEngine;

public class PianoInteractable : Baseinteractable
{
    [SerializeField] private CameraShift camshift;
    [SerializeField] private Transform pianoCamPoint;
    [SerializeField] private GameObject PianoUI;
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] private TempPauseMenu PauseMenu;


    public override void Interact(Ray ray, float maxDistance)
    {
        camshift.EnterCamShift(pianoCamPoint);
        PianoUI.SetActive(true);
        PlayerUI.SetActive(false);
        PauseMenu.enabled = false;
    }

    public void PianoExit()
    {
        camshift.ExitCamShift();
        PianoUI.SetActive(false);
        PlayerUI.SetActive(true);
        PauseMenu.enabled = true;

    }

}
