using System;
using UnityEngine;
public abstract class Bloon<TBloonStats> : Enemy where TBloonStats : BloonStats
{
    [Tooltip("Scriptable Object which decides what happens after bloon is popped")]
    [SerializeField] private TBloonStats stats;
    [Tooltip("Reference to audioSource to play pop sound")]
    [SerializeField] private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource ??= GetComponent<AudioSource>();
    }

    protected override void SetupUpInitialVariables()
    {
        base.SetupUpInitialVariables();
    }

    public override void TakeDamage(Projectile projectile)
    {
        if (projectile == null) return;
        if (stats.ResistantDamageTypes.Contains(projectile.Stats.DamageType)) return;
        health -= projectile.Stats.Layers;
        if (health > 0) return;
        OnLayerPopped();
        Destroy(gameObject);
    }

    private void OnLayerPopped()
    {
        audioSource?.Play();
    }
    
}
