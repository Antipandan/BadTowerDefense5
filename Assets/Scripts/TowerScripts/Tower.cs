using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected TowerScriptObject towerScriptObject;
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
        get => towerScriptObject.TowerRadius;
    }

    public uint TowerCost
    {
        get => towerScriptObject.TowerCost;
    }
}
