using System;
using UnityEngine;
using Utility;

[RequireComponent(typeof(Collider2D))]
public class AddThingScript<TValidTarget> : MonoBehaviour where TValidTarget : MonoBehaviour, IValidTarget
{
    [Tooltip("Used to check if gameObject has a collider or not. Fill in reference if you want")]
    [SerializeField] private new Collider2D collider;
    public Action<TValidTarget> onFoundEnemy;
    public Action<TValidTarget> onEnemyDisappear;

    public int NrFoundEnemySubscribedEvents
    {
        get => onFoundEnemy is null ? 0 : onFoundEnemy.GetInvocationList().Length;
    }

    public int NrLostEnemySubscribedEvents
    {
        get => onEnemyDisappear is null ? 0 : onEnemyDisappear.GetInvocationList().Length;
    }

    protected void Awake()
    {
        collider ??= GetComponent<Collider2D>();
        if (collider is null) Logging.LogNullReferenceError(nameof(collider), ErrorSeverity.Warning, this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TValidTarget foundObject = other.gameObject.GetComponent<TValidTarget>();
        if (foundObject is null) return;
        onFoundEnemy?.Invoke(foundObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TValidTarget foundObject = other.gameObject.GetComponent<TValidTarget>();
        if (foundObject is null) return;
        onEnemyDisappear?.Invoke(foundObject);
    }
}
