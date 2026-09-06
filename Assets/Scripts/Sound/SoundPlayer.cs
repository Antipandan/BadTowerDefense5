using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }
    
    public void PlaySound(float pitch = 1f)
    {
        float oldPitch = audioSource.pitch;
        audioSource.pitch = pitch;
        audioSource.Play();
        audioSource.pitch = oldPitch;
    }

    public void PlayAtPosition(Vector3 position, float pitch = 1f)
    {
        gameObject.transform.position = position;
        PlaySound(pitch);
    }
    
}