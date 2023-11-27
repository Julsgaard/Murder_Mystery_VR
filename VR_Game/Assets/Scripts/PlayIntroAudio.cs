using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIntroAudio : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public float delay = 5.0f; // Delay in seconds

    void Start()
    {
        StartCoroutine(PlayAudioAfterDelay());
    }

    IEnumerator PlayAudioAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}
