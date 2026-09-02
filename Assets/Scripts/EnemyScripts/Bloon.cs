using System;
using UnityEngine;
public class Bloon : Enemy, IDamageAble
{
    
    public void TakeDamage(uint damage)
    {
        
    }

    public uint CurrentHealth
    {
        get => health;
    }
}
