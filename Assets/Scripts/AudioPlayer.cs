using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] noteSounds;
    [SerializeField] private float pitch = 1f;
    public void PlaySound(int noteIndex)
    {
        if (audioSource == null || noteSounds == null || noteSounds.Length == 0)
        {
            Debug.LogWarning("No sounds assigned!");
            return;
        }

        if (noteIndex < 0 || noteIndex >= noteSounds.Length)
        {
            Debug.LogWarning($"Invalid note index: {noteIndex}");
            return;
        }

        Debug.LogWarning($"Playing sound at: {noteIndex}");
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(noteSounds[noteIndex]);
    }
}
