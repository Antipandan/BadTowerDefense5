using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BloonStats", menuName = "Scriptable Objects/BloonStats")]
public class BloonStats : ScriptableObject
{
    [SerializeField] protected AudioClip popSound;
    [SerializeField] protected EnemyFamily nextBloons;
    [SerializeField] protected List<DamageTypes> resistantDamageTypes;

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

    public EnemyFamily NextBloons
    {
        get => nextBloons;
    }
}
