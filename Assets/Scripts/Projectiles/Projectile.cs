using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileStats stats;
    [SerializeField] private Collider2D projectileCollider;
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