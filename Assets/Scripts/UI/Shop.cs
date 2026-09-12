using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility.Logging;

public sealed class Shop :  MonoBehaviour
{
    [Tooltip("Fill in please. Every map should have this gameObject")]
    [SerializeField] private GameEvents gameEvents;
    private Shop instance = null;
    private long currentMoney;

    public Shop Instance
    {
        get => instance;
    }

    public GameEvents GameEvents
    {
        get => gameEvents;
    }

    private void Awake()
    {
        CheckSingleton();
        CheckReferences();
    }

    private void CheckReferences()
    {
        if (gameEvents is null) LogNullReferenceError(nameof(gameEvents), ErrorSeverity.Warning, this);
    }
    
    private void OnEnable()
    {
        SetupValues();
    }

    private void Start()
    {
        PublishEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void SetupValues()
    {
        SubscribeEvents();
    }

    private void UpdateMoney(long amount)
    {
        currentMoney += amount;
    }

    private void SubscribeEvents()
    {
        if (gameEvents is null) return;
        gameEvents.onMoneyEarned += UpdateMoney;
        gameEvents.onMoneySpent += UpdateMoney;
        
    }

    private void UnsubscribeEvents()
    {
        if (gameEvents is null) return;
        gameEvents.onMoneyEarned -= UpdateMoney;
        gameEvents.onMoneySpent -= UpdateMoney;
    }

    private void PublishEvents()
    {
        if (gameEvents is null) return;
        gameEvents.onMoneyEarned += UpdateMoney;
    }

    private void CheckSingleton()
    {
        if (instance == null || instance == this) instance = this;
        else Destroy(this);
    }
    
}