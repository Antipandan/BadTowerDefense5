using UnityEngine;

public static class GameConstants
{
    #region UINTs

    public const uint maxShardLevel = 22;
    public const uint maxLevel = 0b111_111U;
    public const uint towerStartingLevel = 0;
    public const uint startingMoney = 500;
    public const uint startingHealth = 100;
    // millisecond
    public const uint baseFireDelay = 500;
    public const uint baseTowerCost = 100; 
    
    #endregion

    #region Vector2

    public static readonly Vector2 defaultOffsetVector = new Vector2(1 / 10f, 0f);

    #endregion

    #region Vector3

    public static readonly Vector3 ShopItemOnHoverScale = new Vector3(1.1f, 1.1f, 1.1f);

    #endregion

    #region Floats

    public const float baseTowerRadius = 2f;
    public const float resaleMultiplier = 0.8f;
    public const float movementMultiplier = 1.2f;

    #endregion

    #region Other

    public const TargetingModes defaultTargetingMode = TargetingModes.First;

    #endregion

}
