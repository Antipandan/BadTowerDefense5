using System.Runtime.CompilerServices;

public struct Level
{
     private uint currentLevel;

     public Level(uint currentLevel)
     {
          this.currentLevel = currentLevel;
     }

     [MethodImpl(MethodImplOptions.AggressiveInlining)]
     private static bool IsLevelInsideRange(uint level)
     {
          return level <= GameConstants.maxLevel && level % 10 <= GameConstants.maxPathLevel;
     }

     public void TryChangeLevel(uint newLevel, out bool successful)
     {
          successful = IsLevelInsideRange(newLevel);
          if (successful) currentLevel = newLevel;
     }

     public void TryChangeLevel(uint tier, LevelPath path, out bool successful)
     {
          uint level = tier * (uint)path;
          successful = IsLevelInsideRange(level);
          if (successful) currentLevel = level;
     }

     public void TryChangeLevel(uint newLevel)
     {
          if (IsLevelInsideRange(newLevel)) currentLevel = newLevel;
     }
     
     
}
