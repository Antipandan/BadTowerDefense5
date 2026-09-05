using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public sealed class EnemyFamily
{
    [SerializeField] [CanBeNull] private Enemy ParentEnemy;
    [SerializeField] [CanBeNull] private Enemy ChildEnemy;
    
    public Enemy Parent
    {
        get => ParentEnemy;
    }

    public Enemy Child
    {
        get => ChildEnemy;
    }
}