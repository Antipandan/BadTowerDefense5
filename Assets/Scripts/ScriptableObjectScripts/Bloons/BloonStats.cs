using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility.Logging;

[CreateAssetMenu(fileName = "BloonStats", menuName = "Scriptable Objects/BloonStats")]
public class BloonStats : ScriptableObject
{
    [Tooltip("Reference for sound that will be played when bloon is popped")]
    [SerializeField] protected AudioClip popSound;
    [Tooltip("How many children should spawn when bloon is popped?")]
    [SerializeField] [Range(1, 100)] protected uint nrBloonsSpawned = 1;
    [Tooltip("Offset to spawn subsequent child bloons.")]
    [SerializeField] protected Vector2 BloonSpawnOffset = Vector2.zero;
    [Tooltip("Damage types bloon is resistant to")] 
    [SerializeField] protected List<DamageTypes> resistantDamageTypes;

    protected virtual void OnEnable()
    {
        if (popSound is null) LogNullReferenceError(nameof(popSound), ErrorSeverity.None, this);
    }

    public List<DamageTypes> ResistantDamageTypes
    {
        get => resistantDamageTypes;
    }

    public AudioClip PopSound
    {
        get => popSound;
    }

    public uint NrBloonsSpawned
    {
        get => nrBloonsSpawned;
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
