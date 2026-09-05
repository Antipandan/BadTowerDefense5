using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public sealed class EnemyFamily
{
    [Tooltip("Which bloon / enemy comes before the bloon. Reference doesn't need to be filled")]
    [SerializeField] [CanBeNull] private Enemy ParentEnemy;
    [Tooltip("Which bloon / enemy comes after the bloon is popped. Reference doesn't need to be filled. Users are recommended to fill this reference")]
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