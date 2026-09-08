using System;
using UnityEngine;

public sealed class Bomb : Projectile
{
    [SerializeField] private BombExplosionSounds sounds;
    [SerializeField] private GameObject soundPlayerPrefab;

    protected override void Awake()
    {
        base.Awake();
        if (sounds is null) Utility.Utility.LogWarningStandardNullReference(sounds);
        if (soundPlayerPrefab is null) Utility.Utility.LogWarningStandardNullReference(soundPlayerPrefab);
    }
    private void PlaySound()
    {
        SoundPlayer player = Instantiate(soundPlayerPrefab, transform.position, transform.rotation).GetComponent<SoundPlayer>();
        sounds.PlayRandomSound(player.AudioSource);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent(out Enemy enemy)) return;
        OnHit(enemy);
        PlaySound();
    }

    protected override void OnHit(Enemy enemy)
    {
        PlaySound();
        base.OnHit(enemy);
    }
}