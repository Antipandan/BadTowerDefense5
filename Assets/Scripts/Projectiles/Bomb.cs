using System;
using UnityEngine;
using static Utility.Logging;

public sealed class Bomb : Projectile
{
    [SerializeField] private BombExplosionSounds sounds;
    [SerializeField] private GameObject soundPlayerPrefab;

    protected override void Awake()
    {
        base.Awake();
        if (sounds is null) LogNullReferenceError(nameof(sounds), ErrorSeverity.Warning, this);
        if (soundPlayerPrefab is null) LogNullReferenceError(nameof(soundPlayerPrefab), ErrorSeverity.Warning, this);
    }
    private void PlaySound()
    {
        SoundPlayer player = Instantiate(soundPlayerPrefab, transform.position, transform.rotation).GetComponent<SoundPlayer>();
        sounds.PlayRandomSound(player.AudioSource);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        PlaySound();
    }
    
}