using System;
using UnityEngine;

public sealed class Bomb : Projectile
{
    [SerializeField] private BombExplosionSounds sounds;
    [SerializeField] private GameObject soundPlayerPrefab;
    private void PlaySound()
    {
        SoundPlayer player = Instantiate(soundPlayerPrefab, transform.position, transform.rotation).GetComponent<SoundPlayer>();
        sounds.PlayRandomSound(player.AudioSource);
    }

    private void Awake()
    {
        base.SetupValues();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent<Enemy>(out Enemy enemy)) return;
        OnHit(enemy);
        PlaySound();
    }

    protected override void OnHit(Enemy enemy)
    {
        base.OnHit(enemy);
        PlaySound();
    }
}