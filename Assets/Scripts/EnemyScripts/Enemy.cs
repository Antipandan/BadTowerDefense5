using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageAble
{
    [Tooltip("Decides the basics of all enemies in the game")]
    [SerializeField] protected EnemyStats EnemyStats;
    protected uint health;
    protected float movementSpeed;
    
    protected virtual void SetupUpInitialVariables()
    {
        health = EnemyStats.HealthToPop;
        movementSpeed = EnemyStats.RelativeMovementSpeed;
    }

    public virtual void TakeDamage(uint amount)
    {
        health -= amount;
        if (health <= 0) Destroy(gameObject);
    }

    public uint CurrentHealth
    {
        get => health;
    }
    
    public virtual void Move()
    {
        gameObject.transform.position += Vector3.up * movementSpeed * Time.deltaTime;
    }
}
