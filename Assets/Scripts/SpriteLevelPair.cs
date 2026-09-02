using UnityEngine;

[System.Serializable]
public sealed class SpriteLevelPair
{
    [SerializeField] private Sprite inGameSprite;
    [SerializeField] private Sprite levelSprite;
    [SerializeField] private uint upgradeCost;
        
    public Sprite InGameSprite
    {
        get => inGameSprite;
    }

    public Sprite LevelSprite
    {
        get => levelSprite;
    }

    public uint UpgradeCost
    {
        get => upgradeCost;
    }
}
