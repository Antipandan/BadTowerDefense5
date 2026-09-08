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

    private Level level;

    public HashSet<Enemy> Enemies
    {
        get => enemies;
    }

    public Level Level
    {
        get => level;
    }

    public Level TowerLevel { get; }

    protected override void Awake()
    {
        base.Awake();
        level = new Level(GameConstants.towerStartingLevel);
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
