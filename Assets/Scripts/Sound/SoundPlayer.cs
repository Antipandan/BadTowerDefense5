using UnityEngine;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using static Utility.Logging;

public sealed class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private float defaultDelay = 500f;

    private void Awake()
    {
        if (audioSource is null) LogNullReferenceError(nameof(audioSource), ErrorSeverity.Warning, this);
    }
    
    public void PlaySound(AudioClip clip, float pitch = 1f)
    {
        float oldPitch = audioSource.pitch;
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(DestroyObject());
    }
    
    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(Mathf.Min(defaultDelay, audioSource.clip.length));
        Destroy(gameObject);
    }
    

    public void PlayAtPosition(AudioClip clip, Vector3 position, float pitch = 1f)
    {
        gameObject.transform.position = position;
        PlaySound(clip, pitch);
        Destroy(this);
    }

    public AudioSource AudioSource
    {
        get => audioSource;
    }
}