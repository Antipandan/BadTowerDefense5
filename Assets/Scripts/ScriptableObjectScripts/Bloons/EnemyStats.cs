using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [Tooltip("How much health to pop a bloon / layer")]
    [SerializeField] private uint healthToPop = 1;
    [Tooltip("How fast does the bloon / enemy move relative to a speed constant")]
    [SerializeField] [Range(0f, 100f)] private float relativeMovementSpeed = 1f;
    [Tooltip("Reference for sequence of bloons that will spawn when bloon is popped")]
    [SerializeField] protected EnemyFamily nextBloons;

    private void OnEnable()
    {
        if (nextBloons is null) Utility.Utility.LogWarningStandardNullReference(nextBloons);
    }
    
    public uint HealthToPop { get => healthToPop;}

    public uint ExtraStrength
    {
        get => nextBloons.ExtraStrength;
    }
    public float RelativeMovementSpeed { get => relativeMovementSpeed;}
    
    public float AbsoluteMovementSpeed {get => relativeMovementSpeed * GameConstants.movementMultiplier;}
    
    public EnemyFamily NextBloons { get => nextBloons;}
    
    
}