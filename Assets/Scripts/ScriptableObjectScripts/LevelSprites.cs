using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelSprites", menuName = "Scriptable Objects/LevelSprites")]
public sealed class LevelSprites : ScriptableObject
{
    [SerializeField] private SpriteLevelPair level01;
    [SerializeField] private SpriteLevelPair level02;
    [SerializeField] private SpriteLevelPair level10;
    [SerializeField] private SpriteLevelPair level20;

    #region Getters and Setters

    public SpriteLevelPair Level01
    {
        get => level01;
    }

    public SpriteLevelPair Level02
    {
        get => level02;
    }

    public SpriteLevelPair Level10
    {
        get => level10;
    }

    #endregion
    
}
