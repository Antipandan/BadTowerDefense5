using System;
using System.Collections;
using UnityEngine;

namespace TowerScripts
{
    public abstract class AttackTower : Tower
    {
        [Tooltip("Delay in milliseconds (ms)")]
        [SerializeField] [Range(0f, 60000f)]protected float attackDelay = 0f;
        protected Quaternion defaultRotation = Quaternion.identity;
        protected GameObject[] projectileVolley;
        
        private void Awake()
        {
            
        }

        protected virtual IEnumerator Attack(Bloon targetBloon)
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
