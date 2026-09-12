using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
public abstract class Bloon<TBloonStats> : Enemy where TBloonStats : BloonStats
{
    [Tooltip("Scriptable Object which decides what happens after bloon is popped")]
    [SerializeField] protected TBloonStats stats;
    [SerializeField] protected GameObject soundPlayerPrefab;
    
    protected override void Awake()
    {
        base.Awake();
        if (stats == null) Debug.LogWarning($"Warning field {nameof(stats)} is null." +
                                            $" This field must be filled", this);
    }
    [CanBeNull]
    protected virtual Enemy FindNextBloon(uint damageTaken)
    {
        Enemy nextBloon = null;
        for (int i = 0; i < damageTaken; i++)
        {
            nextBloon = enemyStats.NextBloons.Child;
        }
        return nextBloon;
    }

    protected virtual void InstantiateEnemies(Bloon<TBloonStats> bloonInstantiate)
    {
        for (int i = 0; i < stats.NrBloonsSpawned; i++)
        {
            Bloon<TBloonStats> bloon = Instantiate(bloonInstantiate, transform.position + new Vector3(1, 0, 0) * 1/10f * (i - 1), transform.rotation);
            bloon.ConfigureSpline(this);
        }
    }

    public override void TakeDamage(Projectile projectile)
    {
        if (projectile == null) return;
        if (stats.ResistantDamageTypes.Contains(projectile.Stats.DamageType)) return;
        health -= projectile.Stats.Layers;
        OnLayerPopped();
        Bloon<TBloonStats> nextBloon = (Bloon<TBloonStats>)FindNextBloon(projectile.Stats.Layers);
        if (nextBloon != null) InstantiateEnemies(nextBloon);
        Destroy(gameObject);
    }

    private void OnLayerPopped()
    {
        GameObject soundPlayer = null;
        if (soundPlayerPrefab != null) soundPlayer = Instantiate(soundPlayerPrefab, transform.position, transform.rotation);
        soundPlayer?.GetComponent<SoundPlayer>().PlaySound(stats.PopSound);
    }
    
}
