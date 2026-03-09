using Unity.VisualScripting;
using UnityEngine;

public class PianoInteractable : Baseinteractable
{
    [SerializeField] private CameraShift camshift;
    [SerializeField] private Transform pianoCamPoint;
    [SerializeField] private GameObject PianoUI;
    [SerializeField] private PianoBrain pinBrain;


    void Update()
    {
        if (!pinBrain.IsSolved)
            return;
    }

    public override void Interact(Ray ray, float maxDistance)
    {
        camshift.EnterCamShift(pianoCamPoint);
        PianoUI.SetActive(true);
    }

    public void PianoExit()
    {
        camshift.ExitCamShift();
        PianoUI.SetActive(false);
    }

}
