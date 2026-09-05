using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private uint healthToPop = 1;
    [SerializeField] [Range(0f, 100f)] private float relativeMovementSpeed = 1f;
    
    public uint HealthToPop { get => healthToPop;}
    public float RelativeMovementSpeed { get => relativeMovementSpeed;}
}