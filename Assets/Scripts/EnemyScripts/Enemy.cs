using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageAble
{
    [SerializeField] protected BloonStats bloonStats;
    protected uint health;
    protected float movementSpeed;
    

    protected virtual void SetupUpInitialVariables()
    {
        health = bloonStats.HealthToPop;
        movementSpeed = bloonStats.RelativeMovementSpeed;
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
