using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using static Utility.Logging;

public abstract class AttackTower : Tower
{
    [Tooltip("Reference to important data. Fill in!")]
    [SerializeField] protected AttackTowerScriptObject attackTowerScriptObject;
    [Tooltip("What method of picking the most suitable enemy should this tower employ?")]
    [SerializeField] protected TargetingModes targetingMode = GameConstants.defaultTargetingMode;
    [Tooltip("Finds enemies that enters the towers radius. Should find it on a child GameObject belonging to the tower. Fill if possible")]
    [SerializeField] protected AddThingScript<Enemy> findEnemy;
    [Tooltip("Circle that decides what the range of the towers attack is. Small equals smaller attack range and vice versa")]
    [SerializeField] protected CircleCollider2D towerAttackRange;
    [Tooltip("Visual representation of what direction is forward on the tower. The direction the green" +
             " (y-axis) arrow is pointing is the direction that is considered forward. You should find this transform /" +
             " gameObject as a child of the tower. Fill this reference if possible")]
    [SerializeField] protected Transform forwardRotation;
    protected readonly HashSet<Enemy> enemies = new HashSet<Enemy>();
    protected float offsetAngle = 0f;
    protected bool disabled = true;
    public GameObject[] PrefabProjectiles
    {
        get => attackTowerScriptObject.ProjectileVolley;
    }

    public CircleCollider2D TowerAttackRange
    {
        get => towerAttackRange;
    }

    public bool Disabled
    {
        get => disabled;
        set => disabled = value;
    }

    protected virtual void Start()
    {
        SetupValues();
        StartCoroutine(Attack());
    }

    protected override void PlaceTower()
    {
        disabled = false;
        base.PlaceTower();
    }

    protected virtual void SetupValues()
    {
        offsetAngle = forwardRotation is null ? 0f : forwardRotation.localEulerAngles.z;
    }

    protected override void OnValidate()
    {
        base.OnValidate();
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
        if (attackTowerScriptObject is null) LogNullReferenceError(nameof(attackTowerScriptObject),  ErrorSeverity.Warning, this);
        else
        {
            if (attackTowerScriptObject.ProjectileVolley == null ||
                attackTowerScriptObject.ProjectileVolley.Length == 0)
            {
                Debug.LogWarning($"Warning: No projectile volley! No projectile(s) will be shoot");
            }
        }
        if (findEnemy is null)
        {
            Debug.Log($"is empty");
            LogNullReferenceError(nameof(findEnemy),  ErrorSeverity.Warning, this);
        }
        if (forwardRotation is null) LogNullReferenceError(nameof(forwardRotation), ErrorSeverity.Warning, this);
        if (towerAttackRange is null) LogNullReferenceError(nameof(towerAttackRange), ErrorSeverity.Warning, this);
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
        return Targeting.TargetingMode(enemies, this, targetingMode);
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
        while (true)
        {
            while (enemies.Count >= 1 && !disabled)
            {
                Enemy targetedEnemy = FindSuitableEnemy();
                RotateTower(targetedEnemy.transform);
                Shoot();
                yield return new WaitForSeconds(attackTowerScriptObject.AttackDelaySeconds);
            }

            yield return new WaitForSeconds(attackTowerScriptObject.AttackDelaySeconds);
        }
    }
        
    protected virtual void RotateTower(Transform target)
    {
        Vector2 deltaPosition = target.position - forwardRotation.position;
        float angle = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg - 90f - offsetAngle;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    protected virtual void Shoot()
    {
        for (int i = 0; i < attackTowerScriptObject.ProjectileVolley.Length; i++)
        {
            Transform spawn = forwardRotation ?? transform;
            GameObject projectile = Instantiate(attackTowerScriptObject.ProjectileVolley[i], spawn.transform.position, forwardRotation.rotation);
            AdjustProjectileTravelDirection(projectile.GetComponent<Projectile>());            
        }
    }
}
