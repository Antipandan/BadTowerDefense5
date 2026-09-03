using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public struct Level
{
     private uint currentLevel;

     public Level(uint currentLevel)
     {
          this.currentLevel = Math.Max(currentLevel, 44U);
     }

     [MethodImpl(MethodImplOptions.AggressiveInlining)]
     private static bool IsLevelInsideRange(uint level)
     {
          
          return level <= GameConstants.maxLevel && level % 10 <= GameConstants.maxPathLevel;
     }

     public static void UpgradePath(LevelPath path, ref uint currentLevel)
     {
          currentLevel = (currentLevel & (uint)LevelPath.BottomPath) + 1;
     }
     
     public uint TrySetSpecificLevel(uint desiredLevel)
     {
          LevelPath max = FindBiggestLevelPath((LevelPath[])Enum.GetValues(typeof(LevelPath)));
          return (uint)max >= desiredLevel ? desiredLevel : currentLevel;
     }

     public static void TrySetSpecificLevel(uint desiredLevel, ref uint currentLevel)
     {
          LevelPath max = FindBiggestLevelPath((LevelPath[])Enum.GetValues(typeof(LevelPath)));
          if ((uint)max >= desiredLevel) currentLevel = desiredLevel;
          return;
     }

     private static LevelPath FindBiggestLevelPath(LevelPath[] paths)
     {
          LevelPath max = paths[0];
          for (int i = 0; i < paths.Length; i++)
          {
               if ((int)paths[i] > (int)max) max = paths[i];
          }
          return max;
     }

     public uint GetPathLevel(LevelPath path)
     {
          switch (path)
          {
               case LevelPath.BottomPath:
                    return currentLevel & (uint)LevelPath.BottomPath;
               case LevelPath.TopPath:
                    return currentLevel & (uint)LevelPath.TopPath;
          }
          
     }

     public uint CurrentLevel
     {
          get => currentLevel;
     }
     
}
