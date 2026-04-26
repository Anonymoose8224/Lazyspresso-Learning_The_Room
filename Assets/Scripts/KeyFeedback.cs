using System.Collections;
using UnityEngine;

public class KeyFeedback : MonoBehaviour
{
    [SerializeField] private AudioPlayer auds;
    [SerializeField] private AnimPlayer anims;
    [SerializeField] private float animationSpeed = 1f;


    public void Play(int noteIndex)
    {
        auds.PlaySound(noteIndex);
        anims.PlayAnimation(noteIndex);
    }
}