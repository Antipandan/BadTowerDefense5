using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageAble, IValidTarget
{
    [Tooltip("Decides the basics of all enemies in the game")]
    [SerializeField] protected EnemyStats enemyStats;
    protected uint health;
    protected float movementSpeed;
    
    public uint CurrentHealth
    {
        get => health;
    }

    public Transform Transform
    {
        get => gameObject.transform;
    }

    public float MovementSpeed
    {
        get => movementSpeed;
    }

    public EnemyStats EnemyStats
    {
        get => enemyStats;
    }

    /// <summary>
    /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html. If function is to be overriden,
    /// make sure to include base.Awake() at the top of the overriden function
    /// </summary>
    protected virtual void Awake()
    {
        if (enemyStats is null) Debug.LogWarning($"Warning field {nameof(enemyStats)} is null." +
                                                 $" This field must be filled", this);
        SetupUpInitialVariables();
    }

    public static uint TotalEnemyHealth(Enemy enemy)
    {
        uint totalHealth = 0;
        Enemy childEnemy = enemy;
        while (childEnemy is not null)
        {
            totalHealth += childEnemy.CurrentHealth;
            childEnemy = childEnemy.EnemyStats.NextBloons.Child;
        }
        return totalHealth;
    }

    public uint TotalHealth()
    {
        uint totalHealth = 0;
        Enemy currentEnemy = this;
        while (currentEnemy is not null)
        {
            totalHealth += currentEnemy.CurrentHealth + enemyStats.ExtraStrength;
            currentEnemy = currentEnemy.EnemyStats.NextBloons.Child;
        }
        return totalHealth;
    }

    protected virtual void SetupUpInitialVariables()
    {
        health = EnemyStats.HealthToPop;
        movementSpeed = EnemyStats.RelativeMovementSpeed;
    }

    public virtual void TakeDamage(Projectile projectile)
    {
        health -= projectile.Stats.Layers;
        if (health <= 0) Destroy(gameObject);
    }
    
    public virtual void Move()
    {
        gameObject.transform.position += Vector3.up * movementSpeed * Time.deltaTime;
    }
}
