using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public AudioClip[] noteSounds;
    [SerializeField] private AudioClip musClip;
    
    [SerializeField] private float pitch = 1f;
    [SerializeField] private float volume = 1f;
    [SerializeField] private bool playMusicOnAwake = true;
    [SerializeField] private bool useCustomStartTime = false;
    [SerializeField] private float musStartTime = 0f;
    [SerializeField] private bool stopAfterDuration = false;
    [SerializeField] private float musicDuration = 5f;

    private void Awake()
    {
        if (playMusicOnAwake)
        {
            PlayMusic();
        }

    }
    public void PlaySound(int noteIndex)
    {
        if (audioSource == null || noteSounds == null || noteSounds.Length == 0)
        {
            Debug.LogWarning("no sounds assigned");
            return;
        }
        if (noteIndex < 0 || noteIndex >= noteSounds.Length)
        {
            Debug.LogWarning($"invalid note: {noteIndex}");
            return;
        }
        Debug.LogWarning($"Playing sound at: {noteIndex}");
        audioSource.pitch = pitch;
        AudioSource.PlayClipAtPoint(noteSounds[noteIndex], Camera.main.transform.position, volume);
    }

    public void PlayMusic()
    {
        if (audioSource == null || musClip == null)
        {
            Debug.LogWarning("clip missing!");
            return;
        }
        audioSource.clip = musClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = !stopAfterDuration;
        
        if (useCustomStartTime)
        {
            audioSource.time = musStartTime;
        }
        else
        {
            audioSource.time = 0f;
        }

        audioSource.Play();

        if (stopAfterDuration)
        {
            StartCoroutine(StopMusicAfterTime(musicDuration));
        }
    }

    private IEnumerator StopMusicAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        audioSource.Stop();
    }

    public void PauseMusic()
    {
        if (audioSource == null)
        {
            Debug.LogWarning("no audioSource assigned!");
            return;
        }
        audioSource.Pause();
    }

    public float GetMusLength()
    {
        return musicDuration;
    }
}


