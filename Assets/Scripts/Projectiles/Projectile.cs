using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public abstract class Projectile : MonoBehaviour
{
    [Tooltip("What are some of the basic stats of this projectile?")]
    [SerializeField] private ProjectileStats stats;
    [Tooltip("Reference to collider for when projectile hits enemies / bloons")]
    [SerializeField] private Collider2D projectileCollider;
    [Tooltip("normalized movement direction for the projectile")]
    [SerializeField] private Vector2 movementDirection = Vector2.up;
    
    public Vector2 MovementDirection
    {
        get => movementDirection;
    }

    private void Awake()
    {
        CheckImportantValues();
    }
    
    private void Start()
    {
        movementDirection = movementDirection.normalized;
    }
    
    private void OnValidate()
    {
        if (gameObject.TryGetComponent(out SpriteRenderer spriteRenderer)) spriteRenderer.sprite = stats.Sprite;
    }
    
    #region helperFunctions

    private void CheckImportantValues()
    {
        if (CheckSingleType(projectileCollider)) Debug.LogWarning("Collider is null", this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool CheckSingleType(Component component)
    {
        return component is null;
    }

    #endregion

}