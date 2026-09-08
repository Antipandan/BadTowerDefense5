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
    [SerializeField] protected Transform forwardRotation;
    protected readonly HashSet<Enemy> enemies = new HashSet<Enemy>();
    protected float startingAngle = 0f;
    protected float offsetAngle = 0f;
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
        StartCoroutine(Attack());
    }
    

    protected virtual void SetupValues()
    {
        offsetAngle = forwardRotation is null ? 0f : forwardRotation.localEulerAngles.z;
        startingAngle = gameObject.transform.localEulerAngles.z;
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
        StopAllCoroutines();
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

    protected virtual void AdjustProjectileTravelDirection(Projectile projectile)
    {
        projectile.MovementDirection = new Vector2(Mathf.Cos((projectile.transform.rotation.eulerAngles.z + 90f) * Mathf.Deg2Rad),
            Mathf.Sin((projectile.transform.rotation.eulerAngles.z + 90f) * Mathf.Deg2Rad));
    }

    protected virtual Quaternion AdjustProjectileRotation()
    {
        return gameObject.transform.rotation;
    }

    protected virtual IEnumerator Attack()
    {
        // inte den bästa lösningen men måste få saker att fungera tillräckligt väl
        while (true)
        {
            while (enemies.Count >= 1)
            {
                Enemy targetedEnemy = FindSuitableEnemy();
                RotateTower(targetedEnemy.transform);
                Shoot();
                yield return new WaitForSeconds(attackTowerScriptObject.AttackDelaySeconds);
            }

            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }
        
    protected virtual void RotateTower(Transform target)
    {
        Vector2 deltaPosition = target.position - forwardRotation.position;
        float angle = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg - 90f - offsetAngle - startingAngle;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    protected virtual void Shoot()
    {
        for (int i = 0; i < attackTowerScriptObject.ProjectileVolley.Length; i++)
        {
            Transform spawn = forwardRotation is null ? transform : forwardRotation;
            GameObject projectile = Instantiate(attackTowerScriptObject.ProjectileVolley[i], spawn.transform.position, AdjustProjectileRotation());
            AdjustProjectileTravelDirection(projectile.GetComponent<Projectile>());            
        }
    }
}
