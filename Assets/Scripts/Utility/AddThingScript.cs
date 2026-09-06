using System;
using UnityEngine;

public class AddThingScript<TValidTarget> : MonoBehaviour where TValidTarget : MonoBehaviour, IValidTarget
{
    public Action<TValidTarget> onFoundEnemy;
    public Action<TValidTarget> onEnemyDisappear;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"found {other.gameObject.name}");
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
