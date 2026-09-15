using UnityEngine;
using System;
using TMPro;
using Utility;
public abstract class UICounter : MonoBehaviour
{
    [Tooltip("Important reference. Fill this reference! Can be left null. Should not be left null")]
    [SerializeField] protected GameEvents gameEvents;
    [Tooltip("Text to configure in some way. Expected to be used for health / money")]
    [SerializeField] protected TextMeshProUGUI UIText;

    protected virtual void CheckReferences()
    {
        gameEvents ??= FindFirstObjectByType<GameEvents>();
        if (gameEvents is null) Logging.LogNullReferenceError(nameof(gameEvents), ErrorSeverity.Warning, gameObject);
        UIText ??= gameObject.GetComponent<TextMeshProUGUI>() ?? GetComponentInChildren<TextMeshProUGUI>();
        if (UIText is null) Logging.LogNullReferenceError(nameof(UIText), ErrorSeverity.Warning, gameObject);
    }
}