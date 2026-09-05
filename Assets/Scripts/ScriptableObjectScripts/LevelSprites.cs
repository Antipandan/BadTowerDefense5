using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelSprites", menuName = "Scriptable Objects/LevelSprites")]
public sealed class LevelSprites : ScriptableObject
{
    [Tooltip("Information regarding upgrade 0-1")]
    [SerializeField] private TowerUpgrades level01;
    [Tooltip("Information regarding upgrade 0-2")]
    [SerializeField] private TowerUpgrades level02;
    [Tooltip("Information regarding upgrade 1-0")]
    [SerializeField] private TowerUpgrades level10;
    [Tooltip("Information regarding upgrade 2-0")]
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

    public TowerUpgrades Level20
    {
        get => level20;
    }

    #endregion
    
}
