using System;
using UnityEngine;
public abstract class Bloon<TBloonStats> : Enemy<BloonStats> where TBloonStats : BloonStats
{
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
        
    }
    
}
