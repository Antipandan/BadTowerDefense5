using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using static Utility.Utility;

public sealed class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

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
        audioSource.pitch = oldPitch;
        Destroy(this);
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