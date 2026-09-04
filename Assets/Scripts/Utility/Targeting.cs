using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Utility
{
    public static class Targeting
    {
        public static Enemy<BloonStats> TargetingMode(IEnumerable<Enemy<BloonStats>> enemies, TargetingModes mode = TargetingModes.First)
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
        public static Enemy<BloonStats> FirstTargetingMode(IEnumerable<Enemy<BloonStats>> enemies)
        {
            IEnumerable<Enemy<BloonStats>> enumerable = enemies as Enemy<BloonStats>[] ?? enemies.ToArray();
            return enumerable.Any() ? enumerable.First() : null;
        }

        [CanBeNull]
        public static Enemy<BloonStats> StrongTargetingMode(IEnumerable<Enemy<BloonStats>> enemies)
        {
            Enemy<BloonStats> strongestEnemy = null;
            foreach (Enemy<BloonStats> enemy in enemies)
            {
                if (strongestEnemy is null || enemy.CurrentHealth > strongestEnemy.CurrentHealth)
                {
                    strongestEnemy = enemy;
                }
            }
            return strongestEnemy;
        }

        [CanBeNull]
        public static Enemy<BloonStats> CloseTargetingMode(IEnumerable<Enemy<BloonStats>> enemies)
        {
            Enemy<BloonStats> closestEnemy = null;
            foreach (Enemy<BloonStats> enemy in enemies)
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
        public static Enemy<BloonStats> LastTargetingMode(IEnumerable<Enemy<BloonStats>> enemies)
        {
            IEnumerable<Enemy<BloonStats>> enumerable = enemies as Enemy<BloonStats>[] ?? enemies.ToArray();
            return enumerable.Any() ? enumerable.Last() : null;
        }
    }
}