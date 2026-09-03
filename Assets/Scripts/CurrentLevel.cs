using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public struct Level
{
     private uint currentLevel;

     public Level(uint currentLevel)
     {
          this.currentLevel = Math.Max(currentLevel, GameConstants.maxLevel);
     }

     public static void UpgradePath(LevelPath path, ref uint currentLevel)
     {
          currentLevel = (currentLevel & (uint)path) + 1;
     }

     public uint UpgradePath(LevelPath path)
     {
          return (currentLevel & (uint)path) + 1;
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

     /// <summary>
     ///  Returns the Level of a given tower. 
     /// </summary>
     /// <param name="path">Which path to get the level of. See enum description for more info</param>
     /// <param name="getSingleDigit">should function return path as a single digit or not</param>
     /// <returns></returns>
     public uint GetPathLevel(LevelPath path = LevelPath.BottomPath, bool getSingleDigit = false)
     {
          switch (path)
          {
               case LevelPath.BottomPath:
                    if (getSingleDigit) return (uint)LevelPath.BottomPath / CalculateLength(path);
                    return currentLevel & (uint)LevelPath.BottomPath;
               case LevelPath.TopPath:
                    if (getSingleDigit) return (uint)LevelPath.TopPath / CalculateLength(path);
                    return currentLevel & (uint)LevelPath.TopPath;
               default:
                    throw new ArgumentNullException($"{nameof(path)}", "Could not find path that was specified");
          }

          [MethodImpl(MethodImplOptions.AggressiveInlining)]
          uint CalculateLength(LevelPath levelPath)
          {
               return (uint)((uint)levelPath / (uint) CalculateDivisor());
          }

          [MethodImpl(MethodImplOptions.AggressiveInlining)]
          float CalculateDivisor()
          {
               return Mathf.Max(CalculateExponent() + 1, 1);
          }
          [MethodImpl(MethodImplOptions.AggressiveInlining)]
          float CalculateExponent()
          {
               return Mathf.Log10((uint)LevelPath.BottomPath);
          }
     }

     public uint CurrentLevel
     {
          get => currentLevel;
     }
     
}
