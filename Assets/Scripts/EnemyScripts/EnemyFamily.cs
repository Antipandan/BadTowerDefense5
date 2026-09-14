using System;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public sealed class EnemyFamily
{
    [Tooltip("Which bloon / enemy comes before the bloon. Reference doesn't need to be filled")]
    [SerializeField] [CanBeNull] private Enemy ParentEnemy;
    [Tooltip("Which bloon / enemy comes after the bloon is popped. Reference doesn't need to be filled. Users are recommended to fill this reference")]
    [SerializeField] [CanBeNull] private Enemy ChildEnemy;
    [Tooltip("Value to be used when evaluating what bloon to target. If there are two bloon with equal total Health, this modifier will help differentiate")]
    [SerializeField] private uint extraStrength;
    public static event Action onEnemyKilled;

    public static event Action onEnemySpawned;

    public static event Action onEnemyDeleted;
    
    public Enemy Parent
    {
        get => ParentEnemy;
    }

    public Enemy Child
    {
        get => ChildEnemy;
    }

    public uint ExtraStrength
    {
        get => extraStrength;
    }
    

    public static void PublishOnEnemyKilled()
    {
        Debug.Log($"enemy killed");
        onEnemyKilled?.Invoke();
    }

    public static void PublishOnEnemySpawned()
    {
        onEnemySpawned?.Invoke();
    }
    

}
