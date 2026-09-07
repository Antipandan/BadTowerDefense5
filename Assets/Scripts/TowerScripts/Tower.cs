using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Tower : MonoBehaviour, IValidTarget
{
    [SerializeField] protected TowerScriptObject towerScriptObject;
    [Tooltip("Refence to internal tower events! Please fill!!!")]
    [SerializeField] protected TowerEvents towerEvent;
    
    public Transform Transform
    {
        get => transform;
    }

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
        Debug.LogWarning($"Warning Reference to: {nameof(towerEvent)} is missing!", this);
    }

    public float Radius
    {
        get => towerScriptObject.TowerRadius;
    }

    public uint TowerCost
    {
        get => towerScriptObject.TowerCost;
    }
}
