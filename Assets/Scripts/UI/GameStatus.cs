using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using Utility;

public sealed class GameStatus : MonoBehaviour
{
    [Tooltip("Text displaying the current game status. Is either 'won' or 'lost'")]
    [SerializeField] private TextMeshProUGUI gameStatusText;
    [Tooltip("Exit button. Fill this reference. Should exist within prefab. Fill the unity event slot with desired functions")]
    [SerializeField] private Button exitButton;
    [Tooltip("Play again button. Fill this reference. Should exist within prefab. Fill the unity event slot with desired functions")]
    [SerializeField] private Button playAgainButton;
    private static GameStatus instance;

    public static GameStatus Instance
    {
        get => instance;
    }

    public Button ExitButton
    {
        get => exitButton;
    }

    public Button PlayAgainButton
    {
        get => playAgainButton;
    }

    public TextMeshProUGUI GameStatusText
    {
        get => gameStatusText;
    }

    private void Awake()
    {
        Singleton();
        SetupReferences();
    }

    private void SetupReferences()
    {
        gameStatusText ??= GetComponentInChildren<TextMeshProUGUI>();
        if (gameStatusText is null) Logging.LogNullReferenceError(nameof(gameStatusText), ErrorSeverity.Warning, gameObject);
        if (playAgainButton is null) Logging.LogNullReferenceError(nameof(playAgainButton), ErrorSeverity.Warning, gameObject);
        else playAgainButton.onClick.AddListener(ReloadScene);
        exitButton?.onClick.AddListener(PauseGame.ExitGame);
        if (exitButton is null) Logging.LogNullReferenceError(nameof(exitButton), ErrorSeverity.Warning, gameObject);
    }
    
    public void ConfigureGameStatusText(bool lost = false)
    {
        if (gameStatusText is null) return;
        gameStatusText.text = lost? "Game Lost...": "Game Won!";
    }

    private static void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneChange.ReloadScene();
    }
    
    private void Singleton()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}