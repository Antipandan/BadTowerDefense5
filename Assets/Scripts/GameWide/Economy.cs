using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Utility.Logging;

public sealed class Economy : MonoBehaviour
{
    [Tooltip("How much money should a game start with?")]
    [SerializeField] private long startingMoney = GameConstants.startingMoney;
    [Tooltip("How many hearts should a game start with?")]
    [SerializeField] private long startingHealth = GameConstants.startingHealth;
    [Tooltip("Fill this reference!!!")]
    [SerializeField] private GameEvents gameEvents;
    private static Economy instance;
    private long currentMoney;
    private long currentHealth;

    public static Economy Instance
    {
        get => instance;
    }

    public long CurrentMoney
    {
        get => currentMoney;
    }

    public long CurrentHealth
    {
        get => currentHealth;
    }

    private void Awake()
    {
        Singleton();
        AssignCurrencies();
        CheckImportantValues();
    }

    private void Start()
    {
        gameEvents.PublishMoneyEarned(currentMoney);
        gameEvents.PublishLivesLost(0);
    }
    
    private void CheckImportantValues()
    {
        if (gameEvents is null) LogNullReferenceError(nameof(gameEvents), ErrorSeverity.Warning, this);
    }

    /// <summary>
    /// Checks if there exists a different instance of the class
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool CheckIfSingleton()
    {
        return instance == null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Singleton()
    {
        if (!CheckIfSingleton())
        {
            LogSingletonError(nameof(Economy), ErrorSeverity.Warning, this);
            Destroy(gameObject);
        }
        else instance = this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AssignCurrencies()
    {
        currentMoney = startingMoney;
        currentHealth = startingHealth;
    }
    

    public void SpendMoney(long spendAmount)
    {
        currentMoney -= spendAmount;
        gameEvents.PublishMoneySpent(spendAmount);
    }

    public void LoseHealth(uint livesLost)
    {
        currentHealth -= livesLost;
        if (currentHealth > 0) return;
        currentHealth = 0;
        gameEvents.PublishGameLost();
    }

    public void EarnMoney(long earnAmount)
    {
        currentMoney += earnAmount;
        gameEvents.PublishMoneyEarned(earnAmount);
    }
}
