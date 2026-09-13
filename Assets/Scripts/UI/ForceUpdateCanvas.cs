using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// Work around to get misbehaving canvases to work
/// </summary>
public class ForceUpdateCanvas : MonoBehaviour
{
    [Tooltip("Fill in reference!")]
    [SerializeField] private Canvas canvas;
    [Tooltip("Fill in reference!")]
    [SerializeField] private RectTransform root;

    private void Awake()
    {
        Refresh();
    }

    private void OnValidate()
    {
        Refresh();
    }

    public void Refresh()
    {
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(root);
        canvas.enabled = !canvas.enabled;
        canvas.enabled = !canvas.enabled;
    }
}