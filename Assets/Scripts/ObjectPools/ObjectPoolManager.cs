using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace ObjectPools
{
    public class ObjectPoolManager : MonoBehaviour
    {
        private static ObjectPoolManager instance;
        private readonly Dictionary<Object, Stack<Object>> pools = new Dictionary<Object, Stack<Object>>();

        public static ObjectPoolManager Instance
        {
            get => instance;
        }

        public void AddNewPool<TObject>(TObject objectToPool, uint size = 1) where TObject : Object
        {
            if (!pools.ContainsKey(objectToPool))
            {
                pools.Add(objectToPool, new Stack<Object>((int)size));
                
            }
            pools[objectToPool] = null;
            Debug.LogWarning($"cannot create this pool because a pool of type: {nameof(TObject)} already exists!)");
        }

        public void AddNewPool<TObject>(params TObject[] objectsToPool) where TObject : Object
        {
            if (objectsToPool.Length > 0 && pools.ContainsKey(objectsToPool[0]))
            {
                pools[objectsToPool[0]] = new Stack<Object>(objectsToPool);
            }
        }

        public void ShrinkPool<TObject>(TObject objectToPool, uint shrinkAmount) where TObject : Object
        {
            if (pools[objectToPool] is not Stack<TObject> objects) return;
            if (objects.Count < shrinkAmount) RemovePool(objectToPool);
            for (int i = 0; i < shrinkAmount; i++)
            {
                objects.Pop();
            }
        }

        public void RemovePool<TObject>(TObject objectToPool) where TObject : Object
        {
            if (!pools.Remove(objectToPool)) Debug.Log($"Pool of type: {nameof(objectToPool)} doesn't exist!");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] [CanBeNull]
        public Stack<TObjectPool> GetPool<TObjectPool>(TObjectPool objectPool) where TObjectPool : Object
        {
            // rider förslag
            if (pools[objectPool] == null) return null;
            return pools[objectPool] as Stack<TObjectPool>;
        }

        public TObjectPool GetItemFromPool<TObjectPool>(TObjectPool objectPool) where TObjectPool : Object
        {
            return pools[objectPool].Pop() as TObjectPool;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearAllPools()
        {
            pools.Clear();
        }
        
        private void Awake()
        {
            IsSingleTon();    
        }
        
        private void IsSingleTon()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }
    }
}