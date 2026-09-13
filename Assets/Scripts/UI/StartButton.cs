using System;
using UnityEngine;
using UnityEngine.UI;
using Utility;
public sealed class StartButton : MonoBehaviour
{
    [Tooltip("Fill this reference. Reference can be left null, but should not be left null")]
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private Button button;
    private bool isRoundStarted = false;
    private void Awake()
    {
        SetupReferences();
        button.onClick = new Button.ButtonClickedEvent();
        button.onClick.AddListener(OnClick);
    }

    private void SetupReferences()
    {
        AssignReferenceProperly(gameEvents,true);
        AssignReferenceProperly(button);
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        gameEvents.onRoundStart += ChangeRoundStatus;
        gameEvents.onRoundEnd += ChangeRoundStatus;
    }
    

    private void ChangeRoundStatus()
    {
        isRoundStarted ^= true;
    }

    private void OnClick()
    {
        if (!isRoundStarted)
        {
            gameEvents.PublishOnRoundStart();
        }
    }

    private void AssignReferenceProperly<T>(T referece, bool LookSceneWide = false) where T : MonoBehaviour
    {
        referece ??= GetComponent<T>();
        if (referece is null && LookSceneWide) referece = FindFirstObjectByType<T>();
        if (referece is null) Logging.LogNullReferenceError(nameof(referece), ErrorSeverity.Warning, this);
    }
    
}