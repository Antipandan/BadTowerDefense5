using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Splines;
using Utility;

public class RoundSpawner : MonoBehaviour
{
    [Tooltip("Fill this reference")]
    [SerializeField] private GameEvents gameEvents;
    [Tooltip("Spline bloons will follow. Can be left null but if possible fill this reference")]
    [SerializeField] [CanBeNull] private SplineContainer bloonPath;
    [Tooltip("Collider that searches for bloons. Can be left null but certain systems wont work")]
    [SerializeField] [CanBeNull] private FindBloonDetector end;
    [Tooltip("The number of rounds in a map")]
    [SerializeField] private List<Round> rounds = new List<Round>();
    private uint roundNumber = 0;
    private RoundSpawner instance;

    public uint RoundNumber
    {
        get => roundNumber;
        set => roundNumber = (uint)Mathf.Min(value, rounds.Count);
    }
    
    private void Awake()
    {
        Singleton();
        if (bloonPath == null) bloonPath = FindFirstObjectByType<SplineContainer>();
        if (bloonPath is null) Logging.LogNullReferenceError(nameof(bloonPath), ErrorSeverity.Error, this);
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        gameEvents.onRoundStart += SpawnSingleRound;
    }
    

    public void SpawnSingleRound()
    {
        Round currentRound = rounds[(int)RoundNumber];
        roundNumber++;
        for (int i = 0; i < currentRound.SpawnData.Count; i++)
        {
            StartCoroutine(currentRound.SpawnData[i].SpawnEnemies());
        }
    }

    private void Singleton()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

}