using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerScripts
{
    public abstract class AttackTower : Tower
    {
        [Tooltip("Delay in milliseconds (ms)")]
        [SerializeField] [Range(0f, 60000f)] protected float attackDelay = 0f;
        [Tooltip("projectiles that a given attack tower will shoot")]
        [SerializeField] protected GameObject[] projectileVolley;
        protected List<Projectile> projectiles = new List<Projectile>();
        protected Quaternion defaultRotation = Quaternion.identity;

        
        private void Awake()
        {
            CheckImportantReferences();
        }

        protected override void CheckImportantReferences()
        {
            base.CheckImportantReferences();
            if (projectileVolley == null || projectileVolley.Length == 0) Debug.LogWarning($"Warning: No projectile volley! No projectile(s) will be shoot");
        }

        protected virtual void GetProjectileVolley()
        {
            for (int i = 0; i < projectileVolley.Length; i++)
            {
                GameObject currentProjectile = projectileVolley[i];
                if (projectileVolley[i] != null && currentProjectile.TryGetComponent(out Projectile projectile))
                {
                    projectiles.Add(projectile);
                }
            }
        }

        protected virtual IEnumerator Attack(Enemy targetBloon)
        {
            if (attackDelay != 0)
            {
                RotateTower(targetBloon.transform);
                Shoot();
                yield return new WaitForSeconds(attackDelay);
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
}
