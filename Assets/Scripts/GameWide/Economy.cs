using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Utility.Logging;

public sealed class Economy : MonoBehaviour
{
    [Tooltip("How much money should a game start with?")]
    [SerializeField] private float startingMoney = GameConstants.startingMoney;
    [Tooltip("How many hearts should a game start with?")]
    [SerializeField] private uint startingHealth = GameConstants.startingHealth;
    [Tooltip("Fill this reference!!!")]
    [SerializeField] private GameEvents gameEvents;
    private float currentMoney;
    private uint currentHealth;
    private Economy instance;
    
    private void Awake()
    {
        Singleton();
        AssignMoney();
        CheckImportantValues();
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
    private void AssignMoney()
    {
        currentMoney = startingMoney;
    }

    public void SpendMoney(uint amount)
    {
        if (amount <= currentMoney) gameEvents.PublishMoneySpent(amount);
    }
}
