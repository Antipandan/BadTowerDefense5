using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Map : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private List<Collider2D> placeableAres; 
    [SerializeField] private List<Round> rounds = new List<Round>();
    private Queue<Enemy> enemies;
    private uint currentRound = 1;

    private void Awake()
    {
        if (gameEvents is null) Debug.LogWarning($"{nameof(gameEvents)} is null. Fill in this reference", this);
        FillEnemies();
    }

    private void StartRound()
    {
        StartCoroutine(SpawnEnemies());
    }
    
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            
        }
    }

    private uint ConvertRoundToIndex()
    {
        return (uint)Mathf.Max(currentRound - 1, 0);
    }

    private void FillEnemies()
    {
        if (rounds.Count < 0) return;
        enemies = new Queue<Enemy>(rounds[(int)currentRound].EnemiesToSpawn);
    }
}