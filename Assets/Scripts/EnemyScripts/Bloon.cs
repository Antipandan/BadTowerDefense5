using System;
using UnityEngine;
public class Bloon : Enemy
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
