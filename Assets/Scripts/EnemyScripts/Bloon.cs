using System;
using UnityEngine;
public abstract class Bloon<TBloonStats> : Enemy where TBloonStats : BloonStats
{
    [Tooltip("Scriptable Object which decides what happens after bloon is popped")]
    [SerializeField] private TBloonStats stats;
    [Tooltip("Reference to audioSource to play pop sound")]
    [SerializeField] private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource ??= GetComponent<AudioSource>();
    }
    public override void TakeDamage(uint amount)
    {
        base.TakeDamage(amount);
    }

    protected virtual void OnLayerPopped()
    {
        audioSource?.Play();
    }
    
}
