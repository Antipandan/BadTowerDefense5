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
        private readonly List<GameObject> hierarchyRepresentation = new List<GameObject>();

        public static ObjectPoolManager Instance
        {
            get => instance;
        }

        private void AddHierarchyRepresentation<TObjectToPool>(TObjectToPool objectToPool)
        {
            GameObject obj = Instantiate(new GameObject($"Pool: {objectToPool.ToString()}"), transform, true);
            hierarchyRepresentation.Add(obj);
        }

        private void TryFindObjectOfType<TObject>(out int index)
        {
            for (int i = 0; i < hierarchyRepresentation.Count; i++)
            {
                if (hierarchyRepresentation[i].GetComponent<TObject>() != null)
                {
                    index = i;
                }
            }
            index = -1;
        }

        private void MovePooledObjectIntoHierarchy<TObject>(TObject objectToPool) where TObject : Object
        {
            TryFindObjectOfType<TObject>(out int index);
            if (index >= 0)
            {
                GameObject parent = hierarchyRepresentation[index];
                if (parent is not null)
                {
                    // wtf
                    GameObject obj = (GameObject)(Object)objectToPool;
                    obj.transform.SetParent(parent.transform);
                }
            }
        }

        private void RemovePooledObjectFromHierarchy<TObject>(TObject objectToPool) where TObject : Object
        {
            GameObject obj = (GameObject)(Object)objectToPool;
            obj.transform.SetParent(null);
        }

        public void AddNewPool<TObject>(TObject objectToPool, uint size = 1) where TObject : Object
        {
            if (!pools.ContainsKey(objectToPool))
            {
                pools.Add(objectToPool, new Stack<Object>((int)size));
                AddHierarchyRepresentation(objectToPool);
                MovePooledObjectIntoHierarchy(objectToPool);
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
                GameObject obj = (GameObject)(Object)objects.Pop();
                Destroy(obj);
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
            return pools[objectPool].Peek() as TObjectPool;
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