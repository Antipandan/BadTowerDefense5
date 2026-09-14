using System;
using UnityEngine;

public sealed class Dart : Projectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemyHit is null) enemyHit = enemy;
        else return;
        if (enemy is null) return;
        OnHit(enemy);
    }
}