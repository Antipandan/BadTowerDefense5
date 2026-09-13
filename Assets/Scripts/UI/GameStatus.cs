using UnityEngine;
using System;
using TMPro;
using Utility;

public sealed class GameStatus : MonoBehaviour
{
    [Tooltip("Text displaying the current game status. Is either 'won' or 'lost'")]
    [SerializeField] private TextMeshProUGUI gameStatusText; 
    private static GameStatus instance;

    public static GameStatus Instance
    {
        get => instance;
    }

    private void Awake()
    {
        Singleton();
    }

    private void SetupReferences()
    {
        gameStatusText ??= GetComponentInChildren<TextMeshProUGUI>();
        if (gameStatusText is null) Logging.LogNullReferenceError(nameof(gameStatusText), ErrorSeverity.Warning, gameObject);
        
    }
    
    public void ConfigureGameStatusText(bool Lost = false)
    {
        if (gameStatusText is null) return;
        gameStatusText.text = Lost? "Game Lost!": "Game Won!";
    }
    
    private void Singleton()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}