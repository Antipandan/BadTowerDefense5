using System;
using UnityEngine;

public sealed class Dart : Projectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"trigger entered");
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy is null) return;
        OnHit(enemy);
    }
    
}