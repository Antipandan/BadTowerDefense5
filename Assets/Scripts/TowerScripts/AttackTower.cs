using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackTower : Tower
{
    [SerializeField] protected AttackTowerScriptObject attackTowerScriptObject;
    [SerializeField] protected TargetingModes targetingMode = GameConstants.defaultTargetingMode;
    protected readonly List<Projectile> projectiles = new List<Projectile>();
    protected Quaternion defaultRotation = Quaternion.identity;

    public List<Projectile> Projectiles { get => projectiles;}
        
    private void Awake()
    {
        CheckImportantReferences();
    }

    protected override void CheckImportantReferences()
    {
        base.CheckImportantReferences();
        if (attackTowerScriptObject.ProjectileVolley == null || attackTowerScriptObject.ProjectileVolley.Length == 0) Debug.LogWarning($"Warning: No projectile volley! No projectile(s) will be shoot");
    }

    protected virtual void GetProjectileVolley()
    {
        for (int i = 0; i < attackTowerScriptObject.ProjectileVolley.Length; i++)
        {
            GameObject currentProjectile = attackTowerScriptObject.ProjectileVolley[i];
            if (attackTowerScriptObject.ProjectileVolley[i] != null && currentProjectile.TryGetComponent(out Projectile projectile))
            {
                projectiles.Add(projectile);
            }
        }
    }

    protected virtual IEnumerator Attack(Enemy targetBloon)
    {
        if (attackTowerScriptObject.AttackDelay != 0)
        {
            RotateTower(targetBloon.transform);
            Shoot();
            yield return new WaitForSeconds(attackTowerScriptObject.AttackDelay);
        }
        yield return null;
    }
        
    protected virtual void RotateTower(Transform target)
    {
        gameObject.transform.LookAt(target);
    }

    protected virtual void Shoot()
    {
        return;
    }
}
