using System;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class PauseGame : MonoBehaviour
{
    [Tooltip("Not required to fill in used to ensure that components exit in gameObject parent / children")]
    [SerializeField] private Button resumeButton;
    [Tooltip("Not required to fill in used to ensure that components exit in gameObject parent / children")]
    [SerializeField] private Button ExitMainMenuButton;
    [Tooltip("Not required to fill in used to ensure that components exit in gameObject parent / children")]
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        resumeButton ??= GetComponent<Button>();
        if (resumeButton is null) Logging.LogNullReferenceError(nameof(resumeButton), ErrorSeverity.Warning, gameObject);
        ExitMainMenuButton ??= GetComponent<Button>();
        if (ExitMainMenuButton is null) Logging.LogNullReferenceError(nameof(ExitMainMenuButton), ErrorSeverity.Warning, gameObject);
        if (musicSource is null) Logging.LogNullReferenceError(nameof(musicSource), ErrorSeverity.Warning, gameObject);
    }

    public void ResumeGame()
    {
        Resume();
        gameObject.SetActive(false);
    }

    public void GamePause()
    {
        Pause();
        gameObject.SetActive(true);
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
    }

    public static void Pause()
    {
        Time.timeScale = 0f;
    }

    public static void ExitGame()
    {
        Utility.SceneChange.ChangeScene("MainMenu");
        Time.timeScale = 1f;
    }
}