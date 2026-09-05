using System;
using System.Collections;
using System.Collections.Generic;
using TowerScripts;
using UnityEngine;

public class Buccaneer : AttackTower, IUpgradable
{
    [SerializeField] private LevelSprites buccaneerLevelSprite;
    private HashSet<Enemy> enemies = new HashSet<Enemy>();
    private Level level;

    private void Awake()
    {
        level = new Level(GameConstants.towerStartingLevel);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy == null) return;
        enemies.Add(enemy);
    }

    protected override IEnumerator Attack(Enemy targetBloon)
    {
        return base.Attack(targetBloon);
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
