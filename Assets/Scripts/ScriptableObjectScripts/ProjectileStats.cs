using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStats", menuName = "Scriptable Objects/ProjectileStats")]
public class ProjectileStats : ScriptableObject
{
    [SerializeField] [Range(0f, 100f)] private float travelSpeed = 1f;
    [SerializeField] [Range(0, byte.MaxValue)] private uint pierce = 1;
    [SerializeField] [Range(0, ushort.MaxValue)] private uint layers = 1;
    [SerializeField] private DamageTypes damageType = DamageTypes.Regular;
    [SerializeField] private Sprite sprite;
    private Action OnValueChanged;

    public float TravelSpeed
    {
        get => travelSpeed;
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
}