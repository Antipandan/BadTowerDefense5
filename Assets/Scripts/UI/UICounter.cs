using UnityEngine;
using System;
using TMPro;
using Utility;
public abstract class UICounter : MonoBehaviour
{
    [SerializeField] protected GameEvents gameEvents;
    [SerializeField] protected TextMeshProUGUI UIText;

    protected virtual void CheckReferences()
    {
        gameEvents ??= FindFirstObjectByType<GameEvents>();
        if (gameEvents is null) Logging.LogNullReferenceError(nameof(gameEvents), ErrorSeverity.Warning, gameObject);
        UIText ??= gameObject.GetComponent<TextMeshProUGUI>() ?? GetComponentInChildren<TextMeshProUGUI>();
        if (UIText is null) Logging.LogNullReferenceError(nameof(UIText), ErrorSeverity.Warning, gameObject);
    }
}