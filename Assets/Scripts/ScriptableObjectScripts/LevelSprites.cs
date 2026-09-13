using UnityEngine;
using System;
using static Utility.Targeting;
using static Utility.Logging;

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
    
    private void OnEnable()
    {
        CheckSingleReferenceForNull(level01);
        CheckSingleReferenceForNull(level02);
        CheckSingleReferenceForNull(level10);
        CheckSingleReferenceForNull(level20);
    }

    #region ScaryCode

    private static void CheckSingleReferenceForNull(TowerUpgrades level)
    {
        if (CheckIfTypeIsNull(level)) LogNullReferenceError(nameof(level));
        else CheckForNestedNullReferences(level);
    }
    
    private static void CheckForNestedNullReferences(TowerUpgrades instance)
    {
        if (CheckIfTypeIsNull(instance.InGameSprite)) LogNullReferenceError($"{instance} {nameof(instance.InGameSprite)}", ErrorSeverity.Warning);
        if (CheckIfTypeIsNull(instance.LevelSprite)) LogNullReferenceError($"{instance} {nameof(instance.LevelSprite)}", ErrorSeverity.Warning);
        if (CheckIfTypeIsNull(instance.Upgrade)) LogNullReferenceError($"{instance} {nameof(instance.Upgrade)}", ErrorSeverity.Warning);
    }

    #endregion
    
}
