using System;
using UnityEngine;
using UnityEngine.Splines;

public class MapEvent : MonoBehaviour
{
    public event Action<SplineContainer> onEnemySpawned;

    public void PublishOnEnemySpawned(SplineContainer bloonPath)
    { 
        onEnemySpawned?.Invoke(bloonPath);
    }
    
}