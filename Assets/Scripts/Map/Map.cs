using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Map : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private List<Round> rounds = new List<Round>();
    private Queue<Enemy> enemies;

    private void Awake()
    {
        if (gameEvents is null) Debug.LogWarning($"{nameof(gameEvents)} is null. Fill in this reference", this);
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
}