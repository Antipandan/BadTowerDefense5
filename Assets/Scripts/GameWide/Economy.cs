using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class Economy : MonoBehaviour
{
    [SerializeField] private float startingMoney = GameConstants.startingMoney;
    [SerializeField] private uint startingHealth = GameConstants.startingHealth;
    [SerializeField] private GameEvents gameEvents;
    private float currentMoney;
    private uint currentHealth;
    private Economy instance;
    
    private void Awake()
    {
        Singleton();
        AssignMoney();
    }

    private void OnEnable()
    {
        Singleton();
        CheckImportantValues();
    }

    private void CheckImportantValues()
    {
       if (gameEvents is null) Debug.LogWarning($"Warning! {nameof(gameEvents)} is null."); 
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
        if (!CheckIfSingleton()) Destroy(gameObject);
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
