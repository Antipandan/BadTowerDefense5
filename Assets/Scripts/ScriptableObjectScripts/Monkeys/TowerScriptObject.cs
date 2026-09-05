using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TowerScriptObject", menuName = "Scriptable Objects/Towers/Base Tower")]
public class TowerScriptObject : ScriptableObject
{
    [Tooltip("The radius of the tower")]
    [SerializeField] private float towerRadius = GameConstants.baseTowerRadius;
    [Tooltip("How much should the tower cost to purchase")]
    [SerializeField] private uint towerCost = GameConstants.baseTowerCost;

    public float TowerRadius
    {
        get => towerRadius;
    }
    
    public uint TowerCost
    {
        get => towerCost;
    }
}