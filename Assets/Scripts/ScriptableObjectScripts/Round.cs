using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Splines;
using Utility;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "Round", menuName = "Scriptable Objects/Round")]
public class Round : ScriptableObject
{
    [SerializeField] private List<SpawnData> spawnData;
    
    public List<SpawnData> SpawnData { get => spawnData; set => spawnData = value; }

    public Queue<SpawnData> SpawnDataQueue
    {
        get => new Queue<SpawnData>(spawnData);
    }
    
    private void OnEnable()
    {
        if (spawnData is null) return;
        for (int i = 0; i < spawnData.Count; i++)
        {
            spawnData[i].CheckValues();
        }
    }
}

[System.Serializable]
public class SpawnData 
{
    [Tooltip("What enemy is going to be spawned?")]
    [SerializeField] private GameObject enemyToSpawn;
    [Tooltip("How long before a new enemy is spawned. Time in Milliseconds (ms)")]
    [SerializeField] private float spawnInterval;
    [Tooltip("How many enemies are to be spawned?")]
    [SerializeField] [Range(1, 1000)] private uint nrSpawned = 1;
    [Tooltip("Should an enemy spawn the first frame of the round?")]
    [SerializeField] private bool spawnFirstFrame = false;
    private uint totalEnemiesSpawned = 0;
    private GameObject spawnedGameObject;
    
    public float SpawnIntervalMilliseconds
    {
        get => spawnInterval;
    }

    public float SpawnIntervalSeconds
    {
        get => spawnInterval / 1000f;
    }

    public uint NrSpawned
    {
        get => nrSpawned;
    }

    public uint TotalEnemiesSpawned
    {
        get => totalEnemiesSpawned;
    }

    public GameObject SpawnedGameObject
    {
        get => spawnedGameObject;
    }

    public void CheckValues(Object parent = null)
    {
        if (enemyToSpawn is null) Logging.LogNullReferenceError(nameof(enemyToSpawn), ErrorSeverity.Warning, parent);
    }

    public IEnumerator SpawnEnemies(MapEvent mapEvent, SplineContainer bloonPath)
    {
        mapEvent.onEnemySpawned += SetupEnemy;
        // Delay to ensure event is sbuscribed
        yield return null;
        if (!spawnFirstFrame) yield return new WaitForSeconds(SpawnIntervalSeconds);
        while (totalEnemiesSpawned < nrSpawned)
        {
            totalEnemiesSpawned++;
            spawnedGameObject = Object.Instantiate(enemyToSpawn, Vector3.zero, Quaternion.identity);
            mapEvent?.PublishOnEnemySpawned(bloonPath);
            yield return new WaitForSeconds(SpawnIntervalSeconds);
        }
        yield break;
    }

    private void SetupEnemy(SplineContainer bloonPath)
    {
        
    }
    
}