using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStats", menuName = "Scriptable Objects/ProjectileStats")]
public class ProjectileStats : ScriptableObject
{
    [Tooltip("Relative speed of projectile")]
    [SerializeField] [Range(0f, 100f)] private float relativeTravelSpeed = 1f;
    [Tooltip("How many bloons / enemies can projectile pass through before despawning")]
    [SerializeField] [Range(0, byte.MaxValue)] private uint pierce = 1;
    [Tooltip("How many layers does the projectile rip through before despawning")]
    [SerializeField] [Range(0, ushort.MaxValue)] private uint layers = 1;
    [Tooltip("Life time of a projectile measured in ms (milliseconds)")]
    [SerializeField] [Range(1f, 10000f)]private float lifeTime = 1f;
    [Tooltip("What damage type is this projectile?")]
    [SerializeField] private DamageTypes damageType = DamageTypes.Regular;
    [Tooltip("What sprite shall this projectile have?")]
    [SerializeField] private Sprite sprite;
    private Action OnValueChanged;

    private void OnEnable()
    {
        Utility.Utility.LogWarningStandardNullReference(sprite);
    }

    public float RelativeTravelSpeed
    {
        get => relativeTravelSpeed;
    }

    public float AbsoluteTravelSpeed
    {
        get => relativeTravelSpeed * GameConstants.movementMultiplier;
    }

    public uint Pierce
    {
        get => pierce;
    }

    public uint Layers
    {
        get => layers;
    }

    public DamageTypes DamageType
    {
        get => damageType;
    }

    public Sprite Sprite
    {
        get => sprite;
    }

    public float LifeTime
    {
        get => lifeTime;
    }
    
    public float LifeTimeMilliseconds
    {
        get => lifeTime / 1000f;
    }
}