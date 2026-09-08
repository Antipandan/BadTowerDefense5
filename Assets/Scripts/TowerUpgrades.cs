using UnityEngine;

[System.Serializable]
public sealed class TowerUpgrades
{
    [Tooltip("Sprite in game for a given upgrade path")]
    [SerializeField] private Sprite inGameSprite;
    [Tooltip("Sprite representation for a given upgrade")]
    [SerializeField] private Sprite levelSprite;
    [Tooltip("Information regarding the desired upgrade")]
    [SerializeField] private Upgrade upgrade;
    public Sprite InGameSprite
    {
        get => inGameSprite;
    }

    public Sprite LevelSprite
    {
        get => levelSprite;
    }

    public Upgrade Upgrade
    {
        get => upgrade;
    }

    public override string ToString()
    {
        return $"{nameof(TowerUpgrades)}";
    }
    
}
