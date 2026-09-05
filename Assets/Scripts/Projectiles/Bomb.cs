using System;
using UnityEngine;

public sealed class Bomb : Projectile
{
    [SerializeField] private BombExplosionSounds sounds;
    [SerializeField] private AudioSource explosionPlayer;

    private void PlaySound()
    {
        sounds.PlayRandomSound(explosionPlayer);
    }
}