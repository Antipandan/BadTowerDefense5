using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Utility;
using static Utility.Logging;

public abstract class Tower : MonoBehaviour, IValidTarget
{
    [Tooltip("Fill in this reference. This reference can still be left null but it's not recommended")]
    [SerializeField] protected GameEvents gameEvents;
    [Tooltip("Reference to the towers own collider." +
             " To be used for detecting if tower is able to be placed somewhere on the map")]
    [SerializeField] protected Collider2D towerCollider;
    [Tooltip("Reference to important data. Fill in!")]
    [SerializeField] protected TowerScriptObject towerScriptObject;
    protected static Camera mainCamera;
    protected int illegalOverlap = 0;
    protected int totalOverlaps = 0;
    protected bool isPlaced = false;
    protected bool followMouse = false;
    

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

    public bool FollowMouse
    {
        get => followMouse;
        set => followMouse = value;
    }
    
    /// <summary>
    /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html. If function is to be overriden,
    /// make sure to include base.Awake() at the top of the overriden function
    /// </summary>
    protected virtual void Awake()
    {
        CheckImportantReferences();
    }

    private bool DetermineIfFollowMouse()
    {
        return followMouse && !isPlaced && mainCamera is not null;
    }

    protected virtual void Update()
    {
        // hardcode bc need to finish
        if (!DetermineIfFollowMouse()) return;
        gameObject.transform.position = Utility.ConvertBetweenSpaces.ConvertScreenPointToWorldPoint(mainCamera, Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();    
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        totalOverlaps++;
        if (!other.gameObject.activeInHierarchy) return;
        if (isLayerIllegal(other.gameObject.layer)) illegalOverlap++;
        Debug.Log($"totalOverlaps: {totalOverlaps}");
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        totalOverlaps--;
        if (isLayerIllegal(other.gameObject.layer)) illegalOverlap--;
        Debug.Log($"totalOverlaps: {totalOverlaps}");
    }

    protected virtual void PlaceTower()
    {
        if (!IsTowerPlaceable() || FailedPlacement())
        {
            Destroy(gameObject);
            return;
        }
        isPlaced = true;
        followMouse = false;
        gameEvents?.PublishChangeMapCollider2DsState(false, gameEvents.PublishOnGetMapCollider2Ds());
    }

    protected virtual bool FailedPlacement()
    {
        return totalOverlaps == 0;
    }

    protected virtual bool isLayerIllegal(int layer)
    {
        return layer == GameConstants.BloonPathLayerMask || layer == GameConstants.TowerLayerMask || !towerScriptObject.TowerLayerMask.HasLayer(layer);
    }

    protected virtual bool IsTowerPlaceable()
    {
        return illegalOverlap <= 0;
    }
    

    protected virtual void OnValidate()
    {
        
    }
    
    protected virtual void CheckImportantReferences()
    {
        if (towerCollider is null)
        {
            if (gameObject.TryGetComponent(out Collider2D colliderComponent)) towerCollider = colliderComponent;
            else LogNullReferenceError($"{nameof(Collider2D)}", ErrorSeverity.Warning, this);
        }
        mainCamera = Camera.main;
        if (mainCamera is null) LogNullReferenceError(nameof(mainCamera), ErrorSeverity.Error, this);
        gameEvents ??= FindFirstObjectByType<GameEvents>();
        if (gameEvents is null) LogNullReferenceError(nameof(gameEvents), ErrorSeverity.Warning, this);
    }
}
