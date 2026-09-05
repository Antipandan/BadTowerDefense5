using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Utility
{
    public static class Targeting
    {
        public static Enemy TargetingMode(IEnumerable<Enemy> enemies, TargetingModes mode = TargetingModes.First)
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
    }
}