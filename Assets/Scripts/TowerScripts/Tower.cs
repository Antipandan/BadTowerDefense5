using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected TowerEvents towerEvent;
    [SerializeField] protected float radius = 2f;
    [SerializeField] protected uint towerCost = 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InstantiateNewTower()
    {
        Instantiate(this);
    }

    private void Awake()
    {
        CheckImportantReferences();
    }

    protected virtual void CheckImportantReferences()
    {
        Debug.LogWarning($"Warning Reference to: {nameof(towerEvent)} is missing!");
    }

    public float Radius
    {
        get => radius;
    }

    public uint TowerCost
    {
        get => towerCost;
    }
}
