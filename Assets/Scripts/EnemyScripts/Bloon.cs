using System;
using UnityEngine;
public abstract class Bloon : Enemy
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

    private void OnLayerPopped()
    {
        
    }
    
}
