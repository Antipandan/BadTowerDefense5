using System;
using System.Collections;
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
    private bool isRoundStarted = false;
    private uint enemiesToBeSpawned = 0;
    private uint enemiesRemaining = 0;
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

    public bool IsRoundStarted
    {
        get => isRoundStarted;
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

    private void Start()
    {
        StartCoroutine(CheckIsRoundOver());
    }

    private void SetupNumbers()
    {
        enemiesRemaining = enemiesToBeSpawned;
    }

    public void EnemySpawned()
    {
        enemiesRemaining++;
    }

    private void OnEnemySpawned()
    {
        enemiesToBeSpawned--;
    }

    private void OnEnemyDestroyed()
    {
        enemiesRemaining--;
    }
    

    private void SubscribeEvents()
    {
        gameEvents.onRequestRoundStart += SpawnSingleRound;
        if (end is not null) end.onFoundEnemy += OnEnemyReachedEnd;
        EnemyFamily.onEnemySpawned += OnEnemySpawned;
        EnemyFamily.onEnemyKilled += OnEnemyDestroyed;
    }

    private void UnsubscribeEvents()
    {
        gameEvents.onRequestRoundStart -= SpawnSingleRound;
        if (end is not null) end.onFoundEnemy -= OnEnemyReachedEnd;
        EnemyFamily.onEnemySpawned -= OnEnemySpawned;
        EnemyFamily.onEnemyKilled -= OnEnemyDestroyed;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
        StopCoroutine(CheckIsRoundOver());
    }
    
    private void OnEnemyReachedEnd(Enemy enemy)
    {
        gameEvents.PublishLivesLost(enemy.TotalHealth());
        EnemyFamily.PublishOnEnemyKilled();
        Destroy(enemy.gameObject);
    }

    private IEnumerator CheckIsRoundOver()
    {
        while (true)
        {
            if (enemiesRemaining > int.MaxValue) enemiesRemaining = 0;
            isRoundStarted = enemiesRemaining > 0 || enemiesToBeSpawned > 0;
            Debug.Log($"isRoundStarted: {isRoundStarted},  enemiesRemaining: {enemiesRemaining}, enemiesToBeSpawned: {enemiesToBeSpawned}");
            if (!isRoundStarted && roundNumber >= rounds.Count)
            {
                if (Economy.Instance is not null && Economy.Instance.CurrentHealth > 0) gameEvents.PublishGameWon();
                else if (Economy.Instance is not null && Economy.Instance.CurrentHealth <= 0) gameEvents.PublishGameLost();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SpawnSingleRound()
    {
        if (isRoundStarted) return;
        Round currentRound = rounds[(int)RoundNumber];
        enemiesToBeSpawned = currentRound.SpawnEnemiesCount;
        SetupNumbers();
        isRoundStarted = true;
        roundNumber++;
        for (int i = 0; i < currentRound.SpawnData.Count; i++)
        {
            StartCoroutine(currentRound.SpawnData[i].SpawnEnemies());
        }
        gameEvents.PublishOnRoundStarted();
    }

    private void Singleton()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

}