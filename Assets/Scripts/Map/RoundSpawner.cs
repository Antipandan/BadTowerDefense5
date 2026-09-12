using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Utility;

public class RoundSpawner : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private List<Round> rounds = new List<Round>();
    [SerializeField] private SplineContainer bloonPath;
    private uint roundNumber = 0;
    private RoundSpawner instance;

    public uint RoundNumber
    {
        get => roundNumber;
        set => roundNumber = (uint)Mathf.Min(value, rounds.Count);
    }

    public void SpawnSingleRound()
    {
        Debug.Log($"spawn!");
        Debug.Log($"current round number: {roundNumber}");
        Round currentRound = rounds[(int)RoundNumber];
        roundNumber++;
        Debug.Log($"{currentRound.SpawnData.Count}");
        for (int i = 0; i < currentRound.SpawnData.Count; i++)
        {
            Debug.Log($"for");
            StartCoroutine(currentRound.SpawnData[i].SpawnEnemies());
        }
    }

    private void Singleton()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Awake()
    {
        Singleton();
        if (bloonPath == null) bloonPath = FindFirstObjectByType<SplineContainer>();
        if (bloonPath is null) Logging.LogNullReferenceError(nameof(bloonPath), ErrorSeverity.Error, this);
    }

    private void Start()
    {
        SpawnSingleRound();
    }
    
}