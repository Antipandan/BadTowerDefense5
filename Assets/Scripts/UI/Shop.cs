using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using static Utility.Logging;

public sealed class Shop :  MonoBehaviour
{
    [Tooltip("Fill this reference")]
    [SerializeField] private GameEvents gameEvents;
    private static Shop instance = null;

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
        gameEvents ??= FindFirstObjectByType<GameEvents>();
        if (gameEvents == null) Logging.LogNullReferenceError(nameof(gameEvents), ErrorSeverity.Warning, gameObject);
    }
    
    
    private void CheckSingleton()
    {
        if (instance == null || instance == this) instance = this;
        else Destroy(this);
    }
    
}