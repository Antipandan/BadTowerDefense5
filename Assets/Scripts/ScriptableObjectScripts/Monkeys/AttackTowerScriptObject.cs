using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackTowerScriptObject", menuName = "Scriptable Objects/Towers/Attack towers")]
public sealed class AttackTowerScriptObject : ScriptableObject
{
    [Tooltip("Delay in milliseconds (ms)")]
    [SerializeField] [Range(0f, 60000f)] private float attackDelayMilliseconds = GameConstants.baseFireDelay;
    [Tooltip("projectiles that a given attack tower will shoot")]
    [SerializeField] private GameObject[] projectileVolley;
    
    public GameObject[] ProjectileVolley { get => projectileVolley; }

    public float AttackDelayMilliseconds
    {
        get => attackDelayMilliseconds;
    }
    
    public float AttackDelaySeconds
    {
        get => attackDelayMilliseconds / 1000f;
    }
}