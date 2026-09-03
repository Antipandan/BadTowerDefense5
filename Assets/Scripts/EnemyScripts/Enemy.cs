using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageAble
{
    protected uint health;
    protected float movementSpeed;

    public void TakeDamage(uint amount)
    {
        health -= amount;
        if (health <= 0) Destroy(gameObject);
    }

    public uint CurrentHealth
    {
        get => health;
    }
}
