using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [Tooltip("The radius of the tower")]
    [SerializeField] protected float radius = 2f;
    [Tooltip("How much should the tower cost to purchase")]
    [SerializeField] protected uint towerCost = 0;
    [Tooltip("Refence to internal tower events! Please fill!!!")]
    [SerializeField] protected TowerEvents towerEvent;

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
