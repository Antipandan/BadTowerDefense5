using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Round
{
    [Tooltip("All enemies to spawn during a round")]
    [SerializeField] private List<Enemy> enemiesToSpawn;

    [Tooltip("SpawnDelay in milliseconds")] 
    [SerializeField] private float spawnDelay = 25f;
    
    public List<Enemy> EnemiesToSpawn
    {
        get => enemiesToSpawn;
    }

    public float SpawnDelay
    {
        get => spawnDelay;
    }
}