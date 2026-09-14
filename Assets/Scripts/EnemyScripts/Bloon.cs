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

    protected virtual void InstantiateEnemies(Enemy bloonInstantiate)
    {
        for (int i = 0; i < stats.NrBloonsSpawned; i++)
        {
            Enemy bloon = Instantiate(bloonInstantiate, transform.position + new Vector3(1, 0, 0) * 1/10f * (i - 1), transform.rotation);
            bloon.ConfigureSpline(this);
        }
    }

    public override void ConfigureSpline(Enemy enemy)
    {
        base.ConfigureSpline(enemy);
    }

    public override void TakeDamage(Projectile projectile)
    {
        if (projectile == null) return;
        if (stats.ResistantDamageTypes.Contains(projectile.Stats.DamageType)) return;
        health -= projectile.Stats.Layers;
        OnLayerPopped();
        if (Economy.Instance is not null) Economy.Instance.EarnMoney(projectile.Stats.Layers + GameConstants.extraMoneyAwarded);
        Enemy nextBloon = FindNextBloon(projectile.Stats.Layers);
        if (nextBloon != null)
        {
            InstantiateEnemies(nextBloon);
            if (RoundSpawner.Instance is not null) RoundSpawner.Instance.EnemySpawned();
        }
        EnemyFamily.PublishOnEnemyKilled();
        Destroy(gameObject);
    }

    private void OnLayerPopped()
    {
        GameObject soundPlayer = null;
        if (soundPlayerPrefab != null) soundPlayer = Instantiate(soundPlayerPrefab, transform.position, transform.rotation);
        soundPlayer?.GetComponent<SoundPlayer>().PlaySound(stats.PopSound);
    }
    
}
