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
    private Level level;
    private Coroutine attack;

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
        if (enemies.Count <= 0 || currentTarget is null) OnStopTrackingBloons();
    }

    private void AddItemToEnemies(Enemy enemy)
    {
        enemies.Add(enemy);
        currentTarget = FindSuitableEnemy();
        if (currentTarget is null) return;
        StartCoroutine(Attack());
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
            yield return new WaitForSeconds(attackTowerScriptObject.AttackDelaySeconds);
        }

        yield return null;
    }

    protected override void RotateTower(Transform target)
    {
        base.RotateTower(target);
    }

    protected override void Shoot()
    {
        base.Shoot();
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
