using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Splines;
using Utility;

public abstract class Enemy : MonoBehaviour, IDamageAble, IValidTarget
{
    [Tooltip("Decides the basics of all enemies in the game")]
    [SerializeField] protected EnemyStats enemyStats;
    [Tooltip("Spline bloon will follow. Must be assigned in scene or when game is started")]
    [SerializeField] [CanBeNull] protected SplineContainer bloonPath;
    [Tooltip("Spline animate used to control speed, Must be assigned in scene or when game is started")]
    [SerializeField] protected SplineAnimate splineAnimate;
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
        SetupSpline();
    }

    public static uint TotalEnemyHealth(Enemy enemy)
    {
        uint totalHealth = 0;
        Enemy childEnemy = enemy;
        while (childEnemy is not null)
        {
            totalHealth += childEnemy.enemyStats.HealthToPop;
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
            totalHealth += currentEnemy.enemyStats.HealthToPop + enemyStats.ExtraStrength;
            currentEnemy = currentEnemy.EnemyStats.NextBloons.Child;
        }

        return totalHealth;
    }

    protected virtual void SetupUpInitialVariables()
    {
        health = EnemyStats.HealthToPop;
        movementSpeed = EnemyStats.AbsoluteMovementSpeed;
        bloonPath ??= FindFirstObjectByType<SplineContainer>(FindObjectsInactive.Include);
        if (bloonPath is null) Logging.LogNullReferenceError(nameof(bloonPath), ErrorSeverity.Error, this);
        splineAnimate ??= GetComponent<SplineAnimate>();
        if (splineAnimate is null) Logging.LogNullReferenceError(nameof(splineAnimate), ErrorSeverity.Error, this);
    }

    public virtual void TakeDamage(Projectile projectile)
    {
        health -= projectile.Stats.Layers;
        Economy.Instance.EarnMoney(projectile.Stats.Layers);
        if (health <= 0)
        {
            EnemyFamily.PublishOnEnemyKilled();
            Destroy(gameObject);
        }
    }

    protected virtual void ConfigureSpline(Enemy enemy)
    {
        splineAnimate.ElapsedTime = Mathf.Max(((enemy.splineAnimate.ElapsedTime /MovementSpeed) * enemy.MovementSpeed) - 0.1f, 0);
    }

    protected virtual void SetupSpline()
    {
        SetupSplineAnimate.SetupSpline(splineAnimate, bloonPath, movementSpeed);
        splineAnimate.Play();
    }
}
