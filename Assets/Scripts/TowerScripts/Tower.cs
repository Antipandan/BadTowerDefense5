using System;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float radius = 2f;
    protected uint cost = 0;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"game object: '{other.gameObject.name}' was triggered by '");
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"game object: '{other.gameObject.name}' exited");
    }
    
}
