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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent(out Enemy enemy)) return;
        if (enemyHit is not null) return;
        enemyHit = enemy;
        OnHit(enemy);
        PlaySound();

    }
    
}