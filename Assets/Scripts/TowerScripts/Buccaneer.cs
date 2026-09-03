using System;
using System.Collections;
using System.Collections.Generic;
using TowerScripts;
using UnityEngine;

public class Buccaneer : AttackTower, IUpgradable
{
    [SerializeField] private LevelSprites buccaneerLevelSprite;
    private HashSet<Bloon> bloons = new HashSet<Bloon>();
    private Level level;

    private void Awake()
    {
        level = new Level(GameConstants.towerStartingLevel);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bloon bloon = other.gameObject.GetComponent<Bloon>();
        if (bloon == null) return;
    }

    protected override IEnumerator Attack(Bloon targetBloon)
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
