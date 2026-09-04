using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelSprites", menuName = "Scriptable Objects/LevelSprites")]
public sealed class LevelSprites : ScriptableObject
{
    [SerializeField] private TowerUpgrades level01;
    [SerializeField] private TowerUpgrades level02;
    [SerializeField] private TowerUpgrades level10;
    [SerializeField] private TowerUpgrades level20;

    #region Getters and Setters

    public TowerUpgrades Level01
    {
        get => level01;
    }

    public TowerUpgrades Level02
    {
        get => level02;
    }

    public TowerUpgrades Level10
    {
        get => level10;
    }

    #endregion
    
}
