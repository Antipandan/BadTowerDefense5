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
    private uint currentRound = 0;
    private uint totalRounds = 0;

    public uint CurrentRound
    {
        get => currentRound;
        private set =>  currentRound = value;
    }

    public uint TotalRounds
    {
        get => totalRounds;
        private set => totalRounds = Math.Max(value, CurrentRound);
    }

    private void Awake()
    {
        CheckReferences();
    }

    private void Start()
    {
        UpdateRequestRoundDisplay();
    }

    private void UpdateRoundCount()
    {
        totalRounds = RoundSpawner.Instance.NumberOfRounds;
        currentRound = RoundSpawner.Instance.RoundNumber;
    }

    private void UpdateRequestRoundDisplay()
    {
        UpdateRoundCount();
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
        gameEvents.onRoundStarted += UpdateRequestRoundDisplay;
    }

    private void UnsubscribeEvents()
    {
        gameEvents.onRoundStarted -= UpdateRequestRoundDisplay;
    }
}