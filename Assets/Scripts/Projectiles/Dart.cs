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

    private void Awake()
    {
        base.SetupValues();
        StartCoroutine(DestroyProjectile());
    }

    protected override void OnHit(Enemy enemy)
    {
        base.OnHit(enemy);
    }
}