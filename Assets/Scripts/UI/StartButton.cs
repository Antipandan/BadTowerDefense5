using System;
using UnityEngine;
using UnityEngine.UI;
using Utility;
public sealed class StartButton : MonoBehaviour
{
    [Tooltip("Fill this reference. Reference can be left null, but should not be left null")]
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private Button button;
    private void Awake()
    {
        SetupReferences();
        button.onClick = new Button.ButtonClickedEvent();
        button.onClick.AddListener(OnClick);
    }

    private void SetupReferences()
    {
        gameEvents ??= FindFirstObjectByType<GameEvents>();
        if (gameEvents is null) Logging.LogNullReferenceError(nameof(gameEvents), ErrorSeverity.Warning, gameObject);
        button ??= GetComponent<Button>();
        if (button is null) Logging.LogNullReferenceError(nameof(button), ErrorSeverity.Warning, gameObject);
    }

    private void OnClick()
    {
        gameEvents.PublishOnRequestRoundStart();
    }
    
}