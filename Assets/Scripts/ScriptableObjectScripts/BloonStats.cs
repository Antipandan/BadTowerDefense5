using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BloonStats", menuName = "Scriptable Objects/BloonStats")]
public class BloonStats : ScriptableObject
{
    [SerializeField] private uint healthToPop = 1;
    [SerializeField] [Range(0f, 100f)]private float relativeMovementSpeed = 1f;
    [SerializeField] private AudioClip popSound;
    [SerializeField] private BloonFamily nextBloons;
    [SerializeField] private List<DamageTypes> resistantDamageTypes;

    public uint HealthToPop
    {
        get => healthToPop;
    }

    public float RelativeMovementSpeed
    {
        get => relativeMovementSpeed;
    }

    public List<DamageTypes> ResistantDamageTypes
    {
        get => resistantDamageTypes;
    }

    public AudioClip PopSound
    {
        get => popSound;
    }
    
    public uint ResistantDamagetypes
    {
        get
        {
            uint number = 0;
            foreach (DamageTypes type in resistantDamageTypes)
            {
                number += (uint)type;
            }
            return number;
        }
    }

    public BloonFamily NextBloons
    {
        get => nextBloons;
    }
}
