using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Utility;

public sealed class RoundDisplay : MonoBehaviour
{
    [Tooltip("This reference can be null. Reference should not be null. Assign if you can")]
    [SerializeField] private TextMeshProUGUI roundDisplay;
    [Tooltip("This reference can be null. Reference should not be null. Assign if you can")]
    [SerializeField] private GameEvents gameEvents;
    private int currentRound = 0;
    private int totalRounds = -1;

    public int CurrentRound
    {
        get => currentRound;
        private set =>  currentRound = value;
    }

    public int TotalRounds
    {
        get => totalRounds;
        private set => totalRounds = (int)Mathf.Max(value, CurrentRound);
    }

    private void Awake()
    {
        CheckReferences();
        ConfigureRoundDisplay();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void ConfigureRoundDisplay()
    {
        roundDisplay.text = $"Round {currentRound} / {totalRounds}";
    }

    private void CheckReferences()
    {
        if (!FindImportantGameReferences.AssignReferencesProperly(ref roundDisplay, gameObject))
        {
            Logging.LogNullReferenceError(nameof(roundDisplay), ErrorSeverity.Warning, gameObject);
        }

        if (!FindImportantGameReferences.AssignReferencesProperly(ref gameEvents, gameObject))
        {
            Logging.LogNullReferenceError(nameof(gameEvents), ErrorSeverity.Warning, gameObject);
        }
    }

    private void SubscribeEvents()
    {
        return;
    }

    private void UnsubscribeEvents()
    {
        return;
    }
}