using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TowerScriptObject", menuName = "Scriptable Objects/Towers/Base Tower")]
public class TowerScriptObject : ScriptableObject
{
    [Tooltip("The radius of the tower")]
    [SerializeField] private float towerRadius = GameConstants.baseTowerRadius;
    [Tooltip("How much should the tower cost to purchase")]
    [SerializeField] private uint towerCost = GameConstants.baseTowerCost;
    [Tooltip("Sound that will play when tower is placed")] 
    [SerializeField] private AudioClip placementSound;
    [Tooltip("What Layer(s) is this tower able to be placed on? It is recommended that this value has some value other than 'Nothing'")]
    [SerializeField] private LayerMask towerLayerMask;

    public float TowerRadius
    {
        get => towerRadius;
    }
    
    public uint TowerCost
    {
        get => towerCost;
    }

    public LayerMask TowerLayerMask
    {
        get => towerLayerMask;
    }

    public AudioClip PlacementSound
    {
        get => placementSound;
    }
}