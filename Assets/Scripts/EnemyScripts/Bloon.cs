using System;
using UnityEngine;
public class Bloon : Enemy, IDamageAble
{
    public void TakeDamage(uint damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }

    public uint CurrentHealth
    {
        get => health;
    }
}
