using System;
using UnityEngine;
public abstract class Bloon<TBloonStats> : Enemy where TBloonStats : BloonStats
{
    [SerializeField] private TBloonStats stats;
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
