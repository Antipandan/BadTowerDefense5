using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Map : MonoBehaviour
{
    [Tooltip("Fill this reference")]
    [SerializeField] private GameEvents gameEvents;
    [Tooltip("Colliders that represent where land / water / bloon path is. To be used to determine is a tower " +
             "is able to be placed in a certain place.")]
    [SerializeField] private List<Collider2D> placeableAres;
    [Tooltip("Contains how many enemies to be spawned at a given time / round and which interval to spawn new ones")]
    [SerializeField] private List<Round> rounds = new List<Round>();
    private Round currentRound;
    private Queue<Enemy> enemies;
    private uint currentRoundNumber = 1;

    private void Awake()
    {
        if (gameEvents is null) Utility.Utility.LogWarningStandardNullReference(gameEvents);
        // FillEnemies();
    }

    private void StartRound()
    {
        StartCoroutine(SpawnEnemies());
    }
    
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
        }
    }

    private uint ConvertRoundToIndex()
    {
        return (uint)Mathf.Max(currentRoundNumber - 1, 0);
    }

    private void FillEnemies()
    {
        if (rounds.Count < 0) return;
        enemies = new Queue<Enemy>(rounds[(int)currentRoundNumber].EnemiesToSpawn);
    }
}