using UnityEngine;

[System.Serializable]
public sealed class TowerUpgrades
{
    [SerializeField] private Sprite inGameSprite;
    [SerializeField] private Sprite levelSprite;
    [SerializeField] private Upgrade upgrade;
    [SerializeField] private string towerDescription = "Description";
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

    public string TowerDescription
    {
        get => towerDescription;
    }
}
