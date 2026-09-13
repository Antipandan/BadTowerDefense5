using System;
using UnityEngine;

/// <summary>
/// Just a placeholder to access static functions. Kinda stupid but I dont make the rules
/// </summary>
public class GameObjectUtility : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Utility.SceneChange.ChangeScene(sceneName);   
    }

    public void ChangeScene(int index)
    {
        Utility.SceneChange.ChangeScene(index);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    
}