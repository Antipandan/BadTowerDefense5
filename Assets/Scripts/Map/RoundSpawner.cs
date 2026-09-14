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
    [SerializeField] private List<Round> rounds;
    private uint roundNumber = 0;
    private static RoundSpawner instance;

    public uint RoundNumber
    {
        get => roundNumber;
        set => roundNumber = (uint)Mathf.Min(value, rounds.Count);
    }

    public uint NumberOfRounds
    {
        get => (uint)rounds.Count;
    }

    public static RoundSpawner Instance
    {
        get => instance;
    }
    
    private void Awake()
    {
        Singleton();
        bloonPath ??= FindFirstObjectByType<SplineContainer>();
        if (bloonPath is null) Logging.LogNullReferenceError(nameof(bloonPath), ErrorSeverity.Error, gameObject);
        gameEvents ??= FindFirstObjectByType<GameEvents>();
        if (gameEvents is null) Logging.LogNullReferenceError(nameof(gameEvents), ErrorSeverity.Error, gameObject);
        end ??= FindFirstObjectByType<FindBloonDetector>();
        if (end is null) Logging.LogNullReferenceError(nameof(end), ErrorSeverity.Warning, gameObject);
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        gameEvents.onRoundStart += SpawnSingleRound;
        if (end is not null) end.onFoundEnemy += OnEnemyReachedEnd;
    }

    private void OnEnemyReachedEnd(Enemy enemy)
    {
        gameEvents.PublishLivesLost(enemy.TotalHealth());
        Destroy(enemy.gameObject);
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