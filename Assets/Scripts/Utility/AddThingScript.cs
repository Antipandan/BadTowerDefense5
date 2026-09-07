using System;
using UnityEngine;

public class AddThingScript<TValidTarget> : MonoBehaviour where TValidTarget : MonoBehaviour, IValidTarget
{
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
