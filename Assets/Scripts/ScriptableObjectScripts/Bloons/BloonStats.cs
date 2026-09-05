using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BloonStats", menuName = "Scriptable Objects/BloonStats")]
public class BloonStats : ScriptableObject
{
    [Tooltip("Reference for sound that will be played when bloon is popped")]
    [SerializeField] protected AudioClip popSound;
    [Tooltip("Damage types bloon is resistant to")]
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
}
