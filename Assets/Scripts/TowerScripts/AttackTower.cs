using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public abstract class AttackTower : Tower
{
    [SerializeField] protected AttackTowerScriptObject attackTowerScriptObject;
    [SerializeField] protected TargetingModes targetingMode = GameConstants.defaultTargetingMode;
    [SerializeField] protected AddThingScript<Enemy> findEnemy;
    [SerializeField] protected CircleCollider2D towerAttackRange;
    protected readonly HashSet<Enemy> enemies = new HashSet<Enemy>();
    public GameObject[] PrefabProjectiles
    {
        get => attackTowerScriptObject.ProjectileVolley;
    }

    public CircleCollider2D TowerAttackRange
    {
        get => towerAttackRange;
    }
    
    protected virtual void Awake()
    {
        CheckImportantReferences();
    }

    protected virtual void Start()
    {
        SetupValues();
    }

    protected virtual void SetupValues()
    {
    }

    protected virtual void OnValidate()
    {
        towerAttackRange.radius = towerScriptObject.TowerRadius;
    }

    protected virtual void OnEnable()
        // ReSharper disable once CommentTypo
    {   // Det är förmodligen bättre att kolla alla individuellt
        // ReSharper disable once CommentTypo
        // men detta är lättare att återanvända funktionalitet så här
        if (findEnemy.NrFoundEnemySubscribedEvents == 1 && findEnemy.NrLostEnemySubscribedEvents == 1)
        {
            SubscribeEvents();
        }
    }

    protected virtual void OnDisable()
    {
        UnSubscribeEvents();
    }

    protected override void CheckImportantReferences()
    {
        base.CheckImportantReferences();
        SubscribeEvents();
        if (attackTowerScriptObject.ProjectileVolley == null || attackTowerScriptObject.ProjectileVolley.Length == 0)
        {
            Debug.LogWarning($"Warning: No projectile volley! No projectile(s) will be shoot");
        }
    }

    protected virtual void OnEnemyFound(Enemy foundEnemy)
    {
        enemies.Add(foundEnemy);
        StartCoroutine(Attack());
    }

    protected virtual void OnEnemyLost(Enemy lostEnemy)
    {
        enemies.Remove(lostEnemy);
    }

    protected virtual void SubscribeEvents()
    {
        findEnemy.onFoundEnemy += OnEnemyFound;
        findEnemy.onEnemyDisappear += OnEnemyLost;
    }

    protected virtual void UnSubscribeEvents()
    {
        findEnemy.onFoundEnemy -= OnEnemyFound;
        findEnemy.onEnemyDisappear -= OnEnemyLost;
    }
    
    protected Enemy FindSuitableEnemy()
    {
        return Targeting.TargetingMode(enemies, targetingMode);
    }

    protected virtual void AdjustProjectileTravelDirection()
    {
        
    }

    protected virtual Quaternion AdjustProjectileRotation()
    {
        return gameObject.transform.rotation;
    }

    protected virtual IEnumerator Attack()
    {
        if (enemies.Count <= 0) yield break;
        Enemy targetedEnemy = FindSuitableEnemy();
        RotateTower(targetedEnemy.transform);
        Shoot();
        yield return new WaitForSeconds(attackTowerScriptObject.AttackDelayMilliseconds);
    }
        
    protected virtual void RotateTower(Transform target)
    {
        Vector2 deltaPosition = target.position - transform.position;
        float angle = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg -90f;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    protected virtual void Shoot()
    {
        for (int i = 0; i < attackTowerScriptObject.ProjectileVolley.Length; i++)
        {
            Instantiate(attackTowerScriptObject.ProjectileVolley[i], gameObject.transform.position, AdjustProjectileRotation());
        }
    }
}
