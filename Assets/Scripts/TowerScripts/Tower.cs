using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Utility.Utility;

public abstract class Tower : MonoBehaviour, IValidTarget
{
    [Tooltip("Reference to the towers own collider." +
             " To be used for detecting if tower is able to be placed somewhere on the map")]
    [SerializeField] protected Collider2D towerCollider;
    [Tooltip("Reference to important data. Fill in!")]
    [SerializeField] protected TowerScriptObject towerScriptObject;

    protected bool isBeingDragged = false;
    
    public Transform Transform
    {
        get => transform;
    }
    
    public float Radius
    {
        get => towerScriptObject.TowerRadius;
    }

    public uint TowerCost
    {
        get => towerScriptObject.TowerCost;
    }

    public bool IsBeingDragged
    {
        get => isBeingDragged;
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void OnValidate()
    {
        
    }

    protected virtual void FollowTarget(Transform target)
    {
        transform.position = target.position;
    }

    /// <summary>
    /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html. If function is to be overriden,
    /// make sure to include base.Awake() at the top of the overriden function
    /// </summary>
    protected virtual void Awake()
    {
        CheckImportantReferences();
    }

    protected virtual void CheckImportantReferences()
    {
        if (towerCollider is not null) return;
        if (gameObject.TryGetComponent(out Collider2D colliderComponent)) towerCollider = colliderComponent;
        else LogNullReferenceError($"{nameof(Collider2D)}", ErrorSeverity.Warning, this);
    }
    
}
