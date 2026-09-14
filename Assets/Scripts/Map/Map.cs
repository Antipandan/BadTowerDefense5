using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Utility.Logging;

public sealed class Map : MonoBehaviour
{
    [Tooltip("Fill this reference. Component should be present on the Map gameObject / prefab")]
    [SerializeField] private GameEvents gameEvents;
    [Tooltip("Canvas responsible for displaying game over / won")]
    [SerializeField] private GameStatus gameStatusCanvas;
    [Tooltip("Canvas responsible for displaying paused things")]
    [SerializeField] private PauseGame pauseCanvas;
    [Tooltip("Colliders that represent where land / water / bloon path is. To be used to determine is a tower " +
             "is able to be placed in a certain place.")]
    [SerializeField] private List<Collider2D> placeableAres;
    private bool isPaused = false;
    private static Map instance;
    private Queue<Enemy> enemies;
    private uint currentRoundNumber = 1;

    public List<Collider2D> PlaceableAres
    {
        get => placeableAres;
    }

    private void Awake()
    {
        Singleton();
        if (gameEvents is null) LogNullReferenceError(nameof(gameEvents), ErrorSeverity.Warning, this);
        SubscribeEvents();
    }

    public void OnPausePressed(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        isPaused = !isPaused;
        if (isPaused)
        {
            PauseGame.Pause();
            pauseCanvas.gameObject.SetActive(true);
        }
        else
        {
            PauseGame.Resume();
            pauseCanvas.gameObject.SetActive(false);
        }
    }

    private void GameWon()
    {
        if (isPaused) return;
        pauseCanvas.gameObject.SetActive(false);
        gameStatusCanvas.gameObject.SetActive(true);
        gameStatusCanvas.ConfigureGameStatusText();
        PauseGame.Pause();
    }

    private void GameLost()
    {
        if (isPaused) return;
        pauseCanvas.gameObject.SetActive(false);
        gameStatusCanvas.gameObject.SetActive(true);
        gameStatusCanvas.ConfigureGameStatusText(true);
        PauseGame.Pause();
    }
    
    private void Singleton()
    {
        if (instance is null) instance = this;
        else
        {
            LogSingletonError(nameof(Singleton), ErrorSeverity.Warning, this);
            Destroy(this);
        }
    }

    private List<Collider2D> GetPlaceableAreas()
    {
        return placeableAres;
    }

    private void ChangeStateAreas(bool newState, List<Collider2D> area)
    {
        for (int i = 0; i < area.Count; i++)
        {
            area[i].gameObject.SetActive(newState);
        }
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        if (gameEvents is null) return;
        gameEvents.onGetMapCollider2Ds += GetPlaceableAreas;
        gameEvents.onChangeMapCollider2DsState += ChangeStateAreas;
        gameEvents.onGameWon += GameWon;
        gameEvents.onGameLost += GameLost;
    }

    private void UnSubscribeEvents()
    {
        if (gameEvents is null) return;
        gameEvents.onGetMapCollider2Ds -= GetPlaceableAreas;
        gameEvents.onChangeMapCollider2DsState -= ChangeStateAreas;
        gameEvents.onGameWon -= GameWon;
        gameEvents.onGameLost -= GameLost;
    }
    
}