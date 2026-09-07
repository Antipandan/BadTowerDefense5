using System;
using Utility;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Buccaneer : AttackTower, IUpgradable
{
    [Tooltip("Reference to tower upgrades. Fill in!!!")]
    [SerializeField] private LevelSprites buccaneerLevelSprite;
    [SerializeField] private AddThingScript<Enemy> findEnemyScript;
    private readonly HashSet<Enemy> enemies = new HashSet<Enemy>();
    private bool isAttacking = false;
    private Level level;

    public HashSet<Enemy> Enemies
    {
        get => enemies;
    }

    public Level Level
    {
        get => level;
    }

    private void Awake()
    {
        level = new Level(GameConstants.towerStartingLevel);
        defaultRotation = gameObject.transform.rotation;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RemoveItemFromEnemies(Enemy enemy)
    {
        enemies.Remove(enemy);
        currentTarget = FindSuitableEnemy();
    }

    private void AddItemToEnemies(Enemy enemy)
    {
        enemies.Add(enemy);
        currentTarget = FindSuitableEnemy();
    }

    private void SubscribeEvents()
    {
        findEnemyScript.onFoundEnemy += AddItemToEnemies;
        findEnemyScript.onEnemyDisappear += RemoveItemFromEnemies;
    }

    private void UnsubscribeEvents()
    {
        findEnemyScript.onFoundEnemy -= AddItemToEnemies;
        findEnemyScript.onEnemyDisappear -= RemoveItemFromEnemies;
    }

    private void OnStopTrackingBloons()
    {
        StopCoroutine(Attack());
        gameObject.transform.rotation = defaultRotation;
    }

    private Enemy FindSuitableEnemy()
    {
        return Targeting.TargetingMode(enemies, targetingMode);
    }

    protected override IEnumerator Attack()
    {
        while (enemies.Count > 0)
        {
            RotateTower(currentTarget.transform);
            Shoot();
            yield return new WaitForSeconds(attackTowerScriptObject.AttackDelaySeconds);
        }
        yield return null;
    }

    protected override void RotateTower(Transform target)
    {
        base.RotateTower(target);
    }

    public void FixedUpdate()
    {
        if (!isAttacking)
        {
            StartCoroutine(Attack());
            isAttacking = true;
        }
        else if (enemies.Count <= 0)
        {
            StopCoroutine(Attack());
            isAttacking = false;
        }
    }

    protected override void Shoot()
    {
        Debug.Log($"shoot!");
        for (int i = 0; i < PrefabProjectiles.Length; i++)
        {
            if (!PrefabProjectiles[i].TryGetComponent(out Projectile proj)) continue;
            Instantiate(PrefabProjectiles[i], transform.position, transform.rotation);
            projectiles.Add(proj);
        }
    }

    public void Upgrade(LevelPath path)
    {
        level = new Level(level.UpgradePath(path));
    }

    public void Upgrade01()
    {
        
    }

    public void Upgrade02()
    {
        
    }

    public void Upgrade10()
    {
        
    }

    public void Upgrade20()
    {
        
    }
    
}
