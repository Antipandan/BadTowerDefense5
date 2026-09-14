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
        audioSource ??= GetComponent<AudioSource>();
        if (audioSource is null) LogNullReferenceError(nameof(audioSource), ErrorSeverity.Warning, this);
        StartCoroutine(DestroyObject());
    }
    
    public void PlaySound(AudioClip clip, float pitch = 1f)
    {
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.Play();
    }
    
    private IEnumerator DestroyObject()
    {
        // just make it work
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void PlayAtPosition(AudioClip clip, Vector3 position, float pitch = 1f)
    {
        gameObject.transform.position = position;
        PlaySound(clip, pitch);
    }

    public AudioSource AudioSource
    {
        get => audioSource;
    }
}