using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using Object = UnityEngine.Object;

namespace Utility
{
    public static class Targeting
    {
        #region IEnumerable

        /// <summary>
        /// Decides what enemy an AttackTower will target based on their targeting mode
        /// </summary>
        /// <param name="enemies">ICollection of enemies that are visible for a given AttackTower</param>
        /// <param name="tower">Tower that will target the enemy</param>
        /// <param name="mode">What Targeting mode does the AttackTower have?</param>
        /// <returns>Enemy instance that the AttackTower will target</returns>
        [CanBeNull]
        public static Enemy TargetingMode(IEnumerable<Enemy> enemies, Tower tower, TargetingModes mode = GameConstants.defaultTargetingMode)
        {
            if (enemies == null) return null;
            switch (mode)
            {
                case TargetingModes.First:
                    return FirstTargetingMode(enemies);
                case TargetingModes.Strong:
                    return StrongTargetingMode(enemies);
                case TargetingModes.Close:
                    return CloseTargetingMode(enemies, tower);
                case TargetingModes.Last:
                    return LastTargetingMode(enemies);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Finds the first enemy that is visible to an AttackTower.
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
        [CanBeNull]
        public static Enemy FirstTargetingMode(IEnumerable<Enemy> enemies)
        {
            IEnumerable<Enemy> enumerable = enemies as Enemy[] ?? enemies.ToArray();
            return enumerable.Any() ? enumerable.First() : null;
        }

        /// <summary>
        /// Finds the strongest enemy that is visible to an AttackTower. Finds the strongest enemy by comparing
        /// the health of the bloons plus all children that will spawn plus an extra deciding variable if necessary
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
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

        /// <summary>
        /// Finds the closest enemy that is visible to an AttackTower. Finds the closest enemy by comparing
        /// their distance with their magnitude. The lower the closer an enemy is.
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
        [CanBeNull]
        public static Enemy CloseTargetingMode(IEnumerable<Enemy> enemies, Tower tower)
        {
            Enemy closestEnemy = null;
            foreach (Enemy enemy in enemies)
            {
                if (closestEnemy is null || CalculateEnemyDistance(enemy, tower) <
                    CalculateEnemyDistance(closestEnemy, tower))
                {
                    closestEnemy = enemy;
                }
            }
            return closestEnemy;
        }
        
        /// <summary>
        /// Finds the last enemy to enter the AttackTower radius.
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
        [CanBeNull]
        public static Enemy LastTargetingMode(IEnumerable<Enemy> enemies)
        {
            IEnumerable<Enemy> enumerable = enemies as Enemy[] ?? enemies.ToArray();
            return enumerable.Any() ? enumerable.Last() : null;
        }

        #endregion

        #region ICollection

        /// <summary>
        /// Decides what enemy an AttackTower will target based on their targeting mode
        /// </summary>
        /// <param name="enemies">ICollection of enemies that are visible for a given AttackTower</param>
        /// <param name="tower">Tower that will target the enemy</param>
        /// <param name="mode">What Targeting mode does the AttackTower have?</param>
        /// <returns>Enemy instance that the AttackTower will target</returns>
        [CanBeNull]
        public static Enemy TargetingMode(ICollection<Enemy> enemies, Tower tower,
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
                    return CloseTargetingMode(enemies, tower);
                case TargetingModes.Last:
                    return LastTargetingMode(enemies);
                default:
                    return null;
            }
        }
        
        /// <summary>
        /// Finds the first enemy that is visible to an AttackTower.
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
        [CanBeNull]
        public static Enemy FirstTargetingMode(ICollection<Enemy> enemies)
        {
            ICollection<Enemy> enumerable = enemies as Enemy[] ?? enemies.ToArray();
            return enumerable.Any() ? enumerable.First() : null;
        }

        /// <summary>
        /// Finds the strongest enemy that is visible to an AttackTower. Finds the strongest enemy by comparing
        /// the health of the bloons plus all children that will spawn plus an extra deciding variable if necessary
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
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
        
        /// <summary>
        /// Finds the closest enemy that is visible to an AttackTower. Finds the closest enemy by comparing
        /// their distance with their magnitude. The lower the closer an enemy is.
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
        [CanBeNull]
        public static Enemy CloseTargetingMode(ICollection<Enemy> enemies, Tower tower)
        {
            Enemy closestEnemy = null;
            foreach (Enemy enemy in enemies)
            {
                if (closestEnemy is null || CalculateEnemyDistance(enemy, tower) <
                    CalculateEnemyDistance(closestEnemy, tower))
                {
                    closestEnemy = enemy;
                }
            }
            return closestEnemy;
        }
        
        /// <summary>
        /// Finds the last enemy to enter the AttackTower radius.
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
        [CanBeNull]
        public static Enemy LastTargetingMode(ICollection<Enemy> enemies)
        {
            ICollection<Enemy> enumerable = enemies as Enemy[] ?? enemies.ToArray();
            return enumerable.Any() ? enumerable.Last() : null;
        }
        
        #endregion
        
        #region List
        
        
        /// <summary>
        /// Decides what enemy an AttackTower will target based on their targeting mode
        /// </summary>
        /// <param name="enemies">ICollection of enemies that are visible for a given AttackTower</param>
        /// <param name="tower">Tower that will target the enemy</param>
        /// <param name="mode">What Targeting mode does the AttackTower have?</param>
        /// <returns>Enemy instance that the AttackTower will target</returns>
        [CanBeNull]
        public static Enemy TargetingMode(List<Enemy> enemies, Tower tower, TargetingModes mode = GameConstants.defaultTargetingMode)
        {
            if (enemies == null) return null;
            switch (mode)
            {
                case TargetingModes.First:
                    return FirstTargetingMode(enemies);
                case TargetingModes.Strong:
                    return StrongTargetingMode(enemies);
                case TargetingModes.Close:
                    return CloseTargetingMode(enemies, tower);
                case TargetingModes.Last:
                    return LastTargetingMode(enemies);
                default:
                    return null;
            }
        }
        
        /// <summary>
        /// Finds the first enemy that is visible to an AttackTower.
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
        [CanBeNull]
        public static Enemy FirstTargetingMode(List<Enemy> enemies)
        {
            return enemies.Count > 0 ? enemies[0] : null;
        }
        
        /// <summary>
        /// Finds the strongest enemy that is visible to an AttackTower. Finds the strongest enemy by comparing
        /// the health of the bloons plus all children that will spawn plus an extra deciding variable if necessary
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
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

        /// <summary>
        /// Finds the closest enemy that is visible to an AttackTower. Finds the closest enemy by comparing
        /// their distance with their magnitude. The lower the closer an enemy is.
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <param name="tower"></param>
        /// <returns>Enemy to target</returns>
        [CanBeNull]
        public static Enemy CloseTargetingMode(List<Enemy> enemies, Tower tower)
        {
            Enemy closestEnemy = null;
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy currentEnemy = enemies[i];
                if (CheckIfTypeIsNull(closestEnemy) ||
                    CalculateEnemyDistance(currentEnemy, tower) < CalculateEnemyDistance(closestEnemy, tower))
                {
                    closestEnemy = currentEnemy;
                }
            }
            return closestEnemy;
        }
        
        /// <summary>
        /// Finds the last enemy to enter the AttackTower radius.
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
        [CanBeNull]
        public static Enemy LastTargetingMode(List<Enemy> enemies)
        {
            // rider förslag???
            return enemies.Count > 0 ? enemies[^1] : null;
        }
        
        #endregion

        #region Array

        /// <summary>
        /// Decides what enemy an AttackTower will target based on their targeting mode
        /// </summary>
        /// <param name="enemies">ICollection of enemies that are visible for a given AttackTower</param>
        /// <param name="tower">Tower that will target the enemy</param>
        /// <param name="mode">What Targeting mode does the AttackTower have?</param>
        /// <returns>Enemy instance that the AttackTower will target</returns>
        [CanBeNull]
        public static Enemy TargetingMode(Enemy[] enemies, Tower tower, TargetingModes mode = GameConstants.defaultTargetingMode)
        {
            if (enemies == null) return null;
            switch (mode)
            {
                case TargetingModes.First:
                    return FirstTargetingMode(enemies);
                case TargetingModes.Strong:
                    return StrongTargetingMode(enemies);
                case TargetingModes.Close:
                    return CloseTargetingMode(enemies, tower);
                case TargetingModes.Last:
                    return LastTargetingMode(enemies);
                default:
                    return null;
            }
        }
        
        /// <summary>
        /// Finds the first enemy that is visible to an AttackTower.
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
        [CanBeNull]
        public static Enemy FirstTargetingMode(Enemy[] enemies)
        {
            return enemies.Length > 0 ? enemies[0] : null;
        }
        
        /// <summary>
        /// Finds the strongest enemy that is visible to an AttackTower. Finds the strongest enemy by comparing
        /// the health of the bloons plus all children that will spawn plus an extra deciding variable if necessary
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
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

        /// <summary>
        /// Finds the closest enemy that is visible to an AttackTower. Finds the closest enemy by comparing
        /// their distance with their magnitude. The lower the closer an enemy is.
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <param name="tower">Tower to check distance between</param>
        /// <returns>Enemy to target</returns>
        [CanBeNull]
        public static Enemy CloseTargetingMode(Enemy[] enemies, Tower tower)
        {
            Enemy closestEnemy = null;
            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy currentEnemy = enemies[i];
                if (CheckIfTypeIsNull(closestEnemy) ||
                    CalculateEnemyDistance(currentEnemy, tower) < CalculateEnemyDistance(closestEnemy, tower))
                {
                    closestEnemy = currentEnemy;
                }
            }
            return closestEnemy;
        }
        
        /// <summary>
        /// Finds the last enemy to enter the AttackTower radius.
        /// </summary>
        /// <param name="enemies">Enemies the tower sees</param>
        /// <returns>Enemy to target</returns>
        [CanBeNull]
        public static Enemy LastTargetingMode(Enemy[] enemies)
        {
            // rider förslag???
            return enemies.Length > 0 ? enemies[^1] : null;
        }

        #endregion

        /// <summary>
        /// Checks if a supplies type is null
        /// </summary>
        /// <param name="type">Type to check for null</param>
        /// <typeparam name="T">Specified type. Type needs to be a reference type</typeparam>
        /// <returns>True if null. False if no null</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CheckIfTypeIsNull<T>(T type) where T : class
        {
            return type is null;
        }
        
        /// <summary>
        /// C
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float CalculateEnemyDistance(Enemy enemy, Tower tower)
        {
            return (tower.gameObject.transform.position - enemy.gameObject.transform.position).magnitude;
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
                if (layerMask.HasLayer(i)) hasLayers[i] = true;
            }
        
            return hasLayers;
        }

        public static bool HasAllLayers(this LayerMask layerMask, LayerMask otherLayerMask)
        {
            Debug.Log($"function");
            LayerMask newLayerMask = layerMask & otherLayerMask;
            Debug.Log($"layerMask: {newLayerMask}");
            return newLayerMask == layerMask;
        }

        public static bool HasAllLayers(this LayerMask layerMask, params int[] exponents)
        {
            Debug.Log($"has layers");
            for (int i = 0; i < exponents.Length; i++)
            {
                if ((layerMask & (1 << exponents[i])) >> exponents[i] != 1) return false;
            }

            return true;
        }
    
        private static bool InsideBoundsCheck(int layer)
        {
            return layer < 0 || layer > 32;
        }
    
    }

    public static class FindImportantGameReferences
    {
        public static GameEvents FindGameEvent()
        {
            GameEvents gameEvent = Object.FindFirstObjectByType<GameEvents>();
            if (gameEvent is null) Logging.LogNullReferenceError(nameof(gameEvent), ErrorSeverity.Error);
            return gameEvent;
        }

        public static Camera FindMainCamera()
        {
            Camera camera = Object.FindFirstObjectByType<Camera>();
            if (camera is null) Logging.LogNullReferenceError(nameof(camera), ErrorSeverity.Error);
            return Camera.main;
        }

        /// <summary>
        /// Looks for references in gameObject -> scene and at last complains that no such reference exists in the scene
        /// Returns true if it could find reference and false if it couldn't. Function was designed so that return value
        /// can be used to display an error message or other after failed search
        /// </summary>
        /// <param name="reference">Reference to search for</param>
        /// <param name="obj">GameObject to check if it exists as a component</param>
        /// <param name="searchSceneWide"></param>
        /// <typeparam name="TReference"></typeparam>
        /// <returns></returns>
        // ref för att metoden tar en kopia av referense. Värdet kommer inte att skrivas tillbaka?
        public static bool AssignReferencesProperly<TReference>(ref TReference reference, GameObject obj, bool searchSceneWide = false) where TReference : MonoBehaviour
        {
            reference ??= obj.GetComponent<TReference>();
            if (reference is not null) return true;
            if (!searchSceneWide) return false;
            reference = Object.FindFirstObjectByType<TReference>();
            return true;
        }

        /// <summary>
        /// Looks for references in gameObject and complains if ther are no such reference exists in the gameObject
        /// Returns true if it could find reference and false if it couldn't. Function was designed so that return value
        /// can be used to display an error message or other after failed search
        /// </summary>
        /// <param name="reference">Reference to search for</param>
        /// <param name="obj">GameObject to check if it exists as a component</param>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        public static void AssignReferenceProperly<TComponent>(TComponent reference, GameObject obj)
        {
            reference ??= obj.GetComponent<TComponent>();
            if (reference is null)
            {
                Logging.LogNullReferenceError(nameof(reference), ErrorSeverity.Error, obj);
            }
        }
    }

    public static class SetupSplineAnimate
    {
        public static void SetupSpline(SplineAnimate splineAnimate, SplineContainer spline, float speed)
        {
            if (splineAnimate is null) return;
            if (spline is not null) splineAnimate.Container = spline;
            splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
            splineAnimate.MaxSpeed = speed;
            splineAnimate.Loop = SplineAnimate.LoopMode.Once;
            splineAnimate.ObjectUpAxis = SplineComponent.AlignAxis.YAxis;
            splineAnimate.ObjectForwardAxis = SplineComponent.AlignAxis.NegativeZAxis;
            splineAnimate.Alignment = SplineAnimate.AlignmentMode.None;
        }
    }
    
    public static class SceneChange
    {
        /// <summary>
        /// Loads a Unity scene via scene name. Scene name can also be index
        /// but prefer using actual scene index instead of a string representation. Number of scenes cannot be higher
        /// than the number of scenes in project
        /// </summary>
        /// <param name="sceneName">Name of scene to be loaded</param>
        public static void ChangeScene(string sceneName)
        {
            if (int.TryParse(sceneName, out int sceneIndex) && sceneIndex <= SceneManager.sceneCount)
            {
                SceneManager.LoadScene(sceneIndex);
            }   
            else SceneManager.LoadScene(sceneName);
        }
        
        /// <summary>
        /// Loads a Unity scene via scene index. Index cannot be higher than the number of scenes present in project
        /// </summary>
        /// <param name="sceneIndex"></param>
        public static void ChangeScene(int sceneIndex)
        {
            if (sceneIndex <=  SceneManager.sceneCount) SceneManager.LoadScene(sceneIndex);
        }

        /// <summary>
        /// Reloads a given scene. To be used for playing again or other.
        /// </summary>
        public static void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    
    }

    
}
