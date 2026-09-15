using UnityEngine;
using System;
using UnityEngine.UI;
using Utility;

/// <summary>
/// Work around to get misbehaving canvases to work
/// </summary>
public class ForceUpdateCanvas : MonoBehaviour
{
    [Tooltip("Fill in reference! Canvas to be forcibly reloaded")]
    [SerializeField] private Canvas canvas;
    [Tooltip("Fill in reference!. Transfrom that contains everything in a given canvas")]
    [SerializeField] private RectTransform root;

    private void Awake()
    {
        if (canvas is null) Logging.LogNullReferenceError(nameof(canvas), ErrorSeverity.Error, gameObject);
        if (root is null) Logging.LogNullReferenceError(nameof(root), ErrorSeverity.Error, gameObject);
        Refresh();
    }

    private void OnValidate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (canvas is null || root is null) return;
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(root);
        canvas.enabled = !canvas.enabled;
        canvas.enabled = !canvas.enabled;

    }
}