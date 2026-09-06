using UnityEngine;
using System;
using System.Collections;
using System.Runtime.CompilerServices;

public abstract class Projectile : MonoBehaviour
{
    [Tooltip("What are some of the basic stats of this projectile?")]
    [SerializeField] private ProjectileStats stats;
    [Tooltip("Reference to collider for when projectile hits enemies / bloons")]
    [SerializeField] private Collider2D projectileCollider;
    [Tooltip("normalized movement direction for the projectile")]
    [SerializeField] private Vector2 movementDirection = Vector2.up;
    protected uint remainingPierce;

    public ProjectileStats Stats
    {
        get => stats;
    }

    public uint RemainingPierce
    {
        get => remainingPierce;
    }
    
    public Vector2 MovementDirection
    {
        get => movementDirection;
    }

    private void DecrementPierce(uint decrementAmount)
    {
        remainingPierce -= decrementAmount;
        if (remainingPierce <= 0) Destroy(gameObject);
    }

    protected virtual void SetupValues()
    {
        remainingPierce = stats.Pierce;
    }
    
    protected virtual void OnHit(Enemy enemy)
    {
        enemy.TakeDamage(this);
        DecrementPierce(1);
        if (remainingPierce <= 0) Destroy(gameObject);
    }

    /// <summary>
    /// Moves a projectile. To be used in Update()
    /// </summary>
    protected virtual void MoveProjectile()
    {
        gameObject.transform.position = movementDirection * stats.AbsoluteTravelSpeed * Time.deltaTime;
    }

    protected virtual IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(stats.LifeTimeMilliseconds);
        Destroy(gameObject);
    }
    
    #region helperFunctions

    protected virtual void CheckImportantValues()
    {
        if (CheckSingleType(projectileCollider)) Debug.LogWarning("Collider is null", this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckSingleType(Component component)
    {
        return component is null;
    }

    #endregion

}