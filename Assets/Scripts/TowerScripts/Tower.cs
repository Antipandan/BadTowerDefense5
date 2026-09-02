using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float radius = 2f;
    [SerializeField] protected uint cost = 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InstantiateNewTower()
    {
        Instantiate(this);
    }
    
    
    
    
}
