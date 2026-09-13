using UnityEngine;
using System;
using TMPro;
using Utility;

public class CashCounter : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private TextMeshProUGUI cashText;
    private void Awake()
    {
        CheckReferences();
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        gameEvents.onMoneyChanged += ChangeMoneyAmount;
    }

    private void ChangeMoneyAmount()
    {
        if (cashText is null || Economy.Instance is null) return;
        cashText.text = $"$: {Economy.Instance.CurrentMoney}";
    }

    private void CheckReferences()
    {
        gameEvents ??= FindFirstObjectByType<GameEvents>();
        if (gameEvents is null) Logging.LogNullReferenceError(nameof(gameEvents), ErrorSeverity.Warning, gameObject);
        cashText ??= gameObject.GetComponent<TextMeshProUGUI>() ?? GetComponentInChildren<TextMeshProUGUI>();
        if (cashText is null) Logging.LogNullReferenceError(nameof(cashText), ErrorSeverity.Warning, gameObject);
    }
}