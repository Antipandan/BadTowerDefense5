using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Utility.Utility;

public abstract class Tower : MonoBehaviour, IValidTarget
{
    [Tooltip("Fill in this reference. This reference can still be left null but it's not recommended")]
    [SerializeField] protected GameEvents gameEvents;
    [Tooltip("Reference to the towers own collider." +
             " To be used for detecting if tower is able to be placed somewhere on the map")]
    [SerializeField] protected Collider2D towerCollider;
    [Tooltip("Reference to important data. Fill in!")]
    [SerializeField] protected TowerScriptObject towerScriptObject;
    protected LayerMask intersectingLayers = new LayerMask();
    protected static Camera mainCamera;
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
        if (!Input.GetMouseButtonDown(0)) return;
        PlaceTower();
    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.activeInHierarchy) intersectingLayers.AddNewLayerMask(other.gameObject.layer);
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.activeInHierarchy) intersectingLayers.RemoveLayer(other.gameObject.layer);
    }

    protected virtual void PlaceTower()
    {
        isPlaced = true;
        followMouse = false;
        if (gameEvents is null) return;
        if (!IsTowerPlaceable()) return;
        gameEvents.PublishChangeMapCollider2DsState(false, gameEvents.PublishOnGetMapCollider2Ds());
        intersectingLayers.Clear();
    }

    protected virtual bool IsTowerPlaceable()
    {
        Debug.Log($"{intersectingLayers.HasLayer(GameConstants.BloonPathLayerMask)}");
        bool staticCheck = !intersectingLayers.HasLayer(GameConstants.TowerLayerMask) && !intersectingLayers.HasLayer(GameConstants.BloonPathLayerMask);
        return staticCheck;
    }
    
    protected virtual void OnMouseDrag()
    {
        if (isPlaced || followMouse) return;
        Vector2 WorldMousePosition = Utility.ConvertBetweenSpaces.ConvertScreenPointToWorldPoint(mainCamera, Input.mousePosition);
        gameObject.transform.position = WorldMousePosition;
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
        if (gameEvents is null) LogNullReferenceError(nameof(gameEvents), ErrorSeverity.Warning, this);
    }
}
