using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "BombExplosionSounds", menuName = "Scriptable Objects/BombExplosionSounds")]
public sealed class BombExplosionSounds : ScriptableObject
{
    [SerializeField] private List<AudioClip> sounds;
    [SerializeField] private Range pitchRange;
    private Random random;

    public void OnEnable()
    {
        random = new Random();
    }
    
    public List<AudioClip> Sounds { get => sounds; }
    
    public Range PitchRange { get => pitchRange; }
    
    public Random Random { get => random; }

    public void PlayRandomSound(AudioSource source, float volume = 1f)
    {
        PlayRandomSound(source, random, volume);
    }

    public void PlayRandomSound(AudioSource source, Random rand, float volume = 1f)
    {
        source.pitch = pitchRange.GetRandomValue(rand);
        source.PlayOneShot(sounds[rand.Next(sounds.Count)], volume);
    }
    
}
