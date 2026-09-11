using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Utility
{
    public static class Targeting
    {
        #region IEnumerable

        public static Enemy TargetingMode(IEnumerable<Enemy> enemies, TargetingModes mode = GameConstants.defaultTargetingMode)
        {
            if (enemies == null) return null;
            switch (mode)
            {
                case TargetingModes.First:
                    return FirstTargetingMode(enemies);
                case TargetingModes.Strong:
                    return StrongTargetingMode(enemies);
                case TargetingModes.Close:
                    return CloseTargetingMode(enemies);
                case TargetingModes.Last:
                    return LastTargetingMode(enemies);
                default:
                    return null;
            }
        }

        [CanBeNull]
        public static Enemy FirstTargetingMode(IEnumerable<Enemy> enemies)
        {
            IEnumerable<Enemy> enumerable = enemies as Enemy[] ?? enemies.ToArray();
            return enumerable.Any() ? enumerable.First() : null;
        }

        [CanBeNull]
        public static Enemy StrongTargetingMode(IEnumerable<Enemy> enemies)
        {
            Enemy strongestEnemy = null;
            foreach (Enemy enemy in enemies)
            {
                if (strongestEnemy is null || enemy.CurrentHealth > strongestEnemy.CurrentHealth)
                {
                    strongestEnemy = enemy;
                }
            }
            return strongestEnemy;
        }

        [CanBeNull]
        public static Enemy CloseTargetingMode(IEnumerable<Enemy> enemies)
        {
            Enemy closestEnemy = null;
            foreach (Enemy enemy in enemies)
            {
                if (closestEnemy is null || enemy.gameObject.transform.position.magnitude <
                    closestEnemy.gameObject.transform.position.magnitude)
                {
                    closestEnemy = enemy;
                }
            }
            return closestEnemy;
        }

        [CanBeNull]
        public static Enemy LastTargetingMode(IEnumerable<Enemy> enemies)
        {
            IEnumerable<Enemy> enumerable = enemies as Enemy[] ?? enemies.ToArray();
            return enumerable.Any() ? enumerable.Last() : null;
        }

        #endregion

        #region ICollection

        [CanBeNull]
        public static Enemy TargetingMode(ICollection<Enemy> enemies,
            TargetingModes mode = GameConstants.defaultTargetingMode)
        {
            if (enemies == null) return null;
            switch (mode)
            {
                case TargetingModes.First:
                    return FirstTargetingMode(enemies);
                case TargetingModes.Strong:
                    return StrongTargetingMode(enemies);
                case TargetingModes.Close:
                    return CloseTargetingMode(enemies);
                case TargetingModes.Last:
                    return LastTargetingMode(enemies);
                default:
                    return null;
            }
        }
        
        [CanBeNull]
        public static Enemy FirstTargetingMode(ICollection<Enemy> enemies)
        {
            ICollection<Enemy> enumerable = enemies as Enemy[] ?? enemies.ToArray();
            return enumerable.Any() ? enumerable.First() : null;
        }

        [CanBeNull]
        public static Enemy StrongTargetingMode(ICollection<Enemy> enemies)
        {
            Enemy strongestEnemy = null;
            foreach (Enemy enemy in enemies)
            {
                if (strongestEnemy is null || enemy.TotalHealth() > strongestEnemy.TotalHealth())
                {
                    strongestEnemy = enemy;
                }
            }
            return strongestEnemy;
        }
        
        [CanBeNull]
        public static Enemy CloseTargetingMode(ICollection<Enemy> enemies)
        {
            Enemy closestEnemy = null;
            foreach (Enemy enemy in enemies)
            {
                if (closestEnemy is null || enemy.gameObject.transform.position.magnitude <
                    closestEnemy.gameObject.transform.position.magnitude)
                {
                    closestEnemy = enemy;
                }
            }
            return closestEnemy;
        }
        
        [CanBeNull]
        public static Enemy LastTargetingMode(ICollection<Enemy> enemies)
        {
            ICollection<Enemy> enumerable = enemies as Enemy[] ?? enemies.ToArray();
            return enumerable.Any() ? enumerable.Last() : null;
        }
        
        #endregion
        
        #region List
        
        [CanBeNull]
        public static Enemy TargetingMode(List<Enemy> enemies, TargetingModes mode = GameConstants.defaultTargetingMode)
        {
            if (enemies == null) return null;
            switch (mode)
            {
                case TargetingModes.First:
                    return FirstTargetingMode(enemies);
                case TargetingModes.Strong:
                    return StrongTargetingMode(enemies);
                case TargetingModes.Close:
                    return CloseTargetingMode(enemies);
                case TargetingModes.Last:
                    return LastTargetingMode(enemies);
                default:
                    return null;
            }
        }
        
        [CanBeNull]
        public static Enemy FirstTargetingMode(List<Enemy> enemies)
        {
            return enemies.Count > 0 ? enemies[0] : null;
        }
        
        [CanBeNull]
        public static Enemy StrongTargetingMode(List<Enemy> enemies)
        {
            Enemy strongestEnemy = null;
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy currentEnemy = enemies[i];
                if (strongestEnemy is null || currentEnemy.TotalHealth() > strongestEnemy.TotalHealth())
                {
                    strongestEnemy = enemies[i];
                }
            }
            return strongestEnemy;
        }
        
        [CanBeNull]
        public static Enemy CloseTargetingMode(List<Enemy> enemies)
        {
            Enemy closestEnemy = null;
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy currentEnemy = enemies[i];
                if (CheckIfTypeIsNull(closestEnemy) ||
                    CalculateEnemyDistance(currentEnemy) < CalculateEnemyDistance(closestEnemy))
                {
                    closestEnemy = currentEnemy;
                }
            }
            return closestEnemy;
        }
        
        [CanBeNull]
        public static Enemy LastTargetingMode(List<Enemy> enemies)
        {
            // rider förslag???
            return enemies.Count > 0 ? enemies[^1] : null;
        }
        
        #endregion

        #region Array

        [CanBeNull]
        public static Enemy TargetingMode(Enemy[] enemies, TargetingModes mode = GameConstants.defaultTargetingMode)
        {
            if (enemies == null) return null;
            switch (mode)
            {
                case TargetingModes.First:
                    return FirstTargetingMode(enemies);
                case TargetingModes.Strong:
                    return StrongTargetingMode(enemies);
                case TargetingModes.Close:
                    return CloseTargetingMode(enemies);
                case TargetingModes.Last:
                    return LastTargetingMode(enemies);
                default:
                    return null;
            }
        }
        
        [CanBeNull]
        public static Enemy FirstTargetingMode(Enemy[] enemies)
        {
            return enemies.Length > 0 ? enemies[0] : null;
        }
        
        [CanBeNull]
        public static Enemy StrongTargetingMode(Enemy[] enemies)
        {
            Enemy strongestEnemy = null;
            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy currentEnemy = enemies[i];
                if (strongestEnemy is null || currentEnemy.TotalHealth() > strongestEnemy.TotalHealth())
                {
                    strongestEnemy = enemies[i];
                }
            }
            return strongestEnemy;
        }
        
        [CanBeNull]
        public static Enemy CloseTargetingMode(Enemy[] enemies)
        {
            Enemy closestEnemy = null;
            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy currentEnemy = enemies[i];
                if (CheckIfTypeIsNull(closestEnemy) ||
                    CalculateEnemyDistance(currentEnemy) < CalculateEnemyDistance(closestEnemy))
                {
                    closestEnemy = currentEnemy;
                }
            }
            return closestEnemy;
        }
        
        [CanBeNull]
        public static Enemy LastTargetingMode(Enemy[] enemies)
        {
            // rider förslag???
            return enemies.Length > 0 ? enemies[^1] : null;
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CheckIfTypeIsNull<T>(T type) where T : class
        {
            return type is null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float CalculateEnemyDistance(Enemy enemy)
        {
            return enemy.gameObject.transform.position.magnitude;
        }


    }

    public static class CheckBits
    {
        public static bool EnemyHasDamageType(uint combinedType, params DamageTypes[] checkDamageTypes)
        {
            return false;
        }
    }

    public static class ConvertBetweenSpaces
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ConvertScreenPointToWorldPoint(Camera camera, Vector3 screenPoint)
        {
            Vector3 worldPoint = camera.ScreenToWorldPoint(screenPoint);
            return GetProperMousePosition(worldPoint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetProperMousePosition(Vector3 mousePosition)
        {
            return new Vector3(mousePosition.x, mousePosition.y, 0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetProperMousePosition(ref Vector3 mousePosition)
        {
            mousePosition.z = 0f;
        }
    }
    
}

// useless :(
public static class LayerMaskExtensions
{

    public static bool HasLayer(this LayerMask layerMask, int layerExponent)
    {
        if (InsideBoundsCheck(layerExponent)) return false;
        return layerMask == (layerMask | (1 << layerExponent));
    }

    public static LayerMask Clear(this LayerMask layerMask)
    {
        return new LayerMask();
    }

    public static LayerMask AddNewLayerMask(this LayerMask layerMask, int layerExponent)
    {
        if (!InsideBoundsCheck(layerExponent)) return layerMask;
        layerMask |= (1 << layerExponent);
        return layerMask;
    }

    public static LayerMask RemoveLayer(this LayerMask layerMask, int layerExponent)
    {
        if (!InsideBoundsCheck(layerExponent)) return layerMask;
        layerMask &= ~(1 << layerExponent);
        return layerMask;
    }

    public static bool[] HasLayers(this LayerMask layerMask)
    {
        bool[] hasLayers = new bool[32]; 
        
        for (int i = 0; i < 32; i++)
        {
            if (HasLayer(layerMask, i)) hasLayers[i] = true;
        }
        
        return hasLayers;
    }
    
    private static bool InsideBoundsCheck(int layer)
    {
        return layer < 0 || layer > 32;
    }
}

