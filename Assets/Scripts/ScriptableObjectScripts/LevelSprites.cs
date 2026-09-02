using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelSprites", menuName = "Scriptable Objects/LevelSprites")]
public sealed class LevelSprites : ScriptableObject
{
    [SerializeField] private SpriteLevelPair level00;
    [SerializeField] private SpriteLevelPair level01;
    [SerializeField] private SpriteLevelPair level02;
    [SerializeField] private SpriteLevelPair level10;
    [SerializeField] private SpriteLevelPair level11;
    [SerializeField] private SpriteLevelPair level12;
    [SerializeField] private SpriteLevelPair level20;
    [SerializeField] private SpriteLevelPair level21;
    [SerializeField] private SpriteLevelPair level22;

    #region Getters and Setters

    public SpriteLevelPair Level00
    {
        get => level00;
    }

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

    public SpriteLevelPair Level11
    {
        get => level11;
    }

    public SpriteLevelPair Level12
    {
        get => level12;
    }

    public SpriteLevelPair Level20
    {
        get => level20;
    }

    public SpriteLevelPair Level21
    {
        get => level21;
    }

    public SpriteLevelPair Level22
    {
        get => level22;
    }

    #endregion
    
}
