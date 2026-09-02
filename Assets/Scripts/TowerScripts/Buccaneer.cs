using TowerScripts;
using UnityEngine;

public class Buccaneer : AttackTower, IUpgradable
{
    [SerializeField] private LevelSprites buccaneerLevelSprite;
    private Level currentLevel;

    public void Upgrade(uint newLevel)
    {
        currentLevel.TryChangeLevel(newLevel);
    }
}
