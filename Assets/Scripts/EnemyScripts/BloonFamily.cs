using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public sealed class BloonFamily
{
    [SerializeField] [CanBeNull] private Bloon ParentBloon;
    [SerializeField] [CanBeNull] private Bloon ChildBloon;
    
    public Bloon Parent
    {
        get => ParentBloon;
    }

    public Bloon Child
    {
        get => ChildBloon;
    }
}