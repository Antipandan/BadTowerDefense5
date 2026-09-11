using UnityEngine;
using UnityEngine.UI;
using Utility;
public sealed class StartButton : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    private Button button;
    private void Awake()
    {
        SetupReferences();
    }

    private void SetupReferences()
    {
        AssignReferenceProperly(gameEvents,true);
        AssignReferenceProperly(button);
    }

    private void AssignReferenceProperly<T>(T referece, bool LookSceneWide = false) where T : MonoBehaviour
    {
        referece ??= GetComponent<T>();
        if (referece is null && LookSceneWide) referece = FindFirstObjectByType<T>();
        if (referece is null) Utility.Utility.LogNullReferenceError(nameof(referece), ErrorSeverity.Warning, this);
    }
}