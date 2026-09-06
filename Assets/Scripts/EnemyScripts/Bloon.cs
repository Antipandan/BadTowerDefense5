using System;
using UnityEngine;
public abstract class Bloon<TBloonStats> : Enemy where TBloonStats : BloonStats
{
    [Tooltip("Scriptable Object which decides what happens after bloon is popped")]
    [SerializeField] protected TBloonStats stats;
    [SerializeField] protected GameObject soundPlayerPrefab;
    
    private void Awake()
    {
        
    }

    protected override void SetupUpInitialVariables()
    {
        base.SetupUpInitialVariables();
    }

    public override void TakeDamage(Projectile projectile)
    {
        Debug.Log($"take damage {gameObject.name}");
        if (projectile == null) return;
        if (stats.ResistantDamageTypes.Contains(projectile.Stats.DamageType)) return;
        health -= projectile.Stats.Layers;
        OnLayerPopped();
        Destroy(gameObject);
        if (health > 0) return;

    }

    private void OnLayerPopped()
    {
        Debug.Log($"layer popped {gameObject.name}");
        GameObject soundPlayer = null;
        if (soundPlayerPrefab != null) soundPlayer = Instantiate(soundPlayerPrefab, transform.position, transform.rotation);
        soundPlayer?.GetComponent<SoundPlayer>().PlaySound(stats.PopSound);
    }
    
}
