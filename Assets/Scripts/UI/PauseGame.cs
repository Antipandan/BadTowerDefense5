using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button ExitMainMenuButton;

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