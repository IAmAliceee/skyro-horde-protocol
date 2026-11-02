using System;
using System.Collections.Generic;
using UnityEngine;

namespace AliceTools.Pool
{
    public class RuntimePool<T> : IDisposable where T : UnityEngine.MonoBehaviour
    {
        #region References
        protected Queue<T> Pool;
        protected List<T> AllObjects;
        protected Func<UnityEngine.MonoBehaviour, UnityEngine.MonoBehaviour> instantiate;
        protected Action<UnityEngine.MonoBehaviour> destroy;
        protected T prefab;
        #endregion

        public RuntimePool(T prefab, uint initSize = 0)
            => Init(prefab, MonoBehaviour.Instantiate, MonoBehaviour.Destroy, initSize);

        public RuntimePool(T prefab, Func<UnityEngine.MonoBehaviour, MonoBehaviour> instantiateFunc, Action<UnityEngine.MonoBehaviour> destroyMethod, uint initSize = 0)
            => Init(prefab, instantiateFunc, destroyMethod, initSize);

        protected void Init(T prefab, Func<UnityEngine.MonoBehaviour, MonoBehaviour> instantiateFunc, Action<UnityEngine.MonoBehaviour> destroyMethod, uint initSize = 0)
        {
            var initSizeInt = (int)initSize;

            this.prefab = prefab;
            instantiate = instantiateFunc;
            destroy = destroyMethod;
            Pool = new(initSizeInt);
            AllObjects = new(initSizeInt);

            CreateAndEnqueue(initSizeInt);
        }

        public T Get()
        {
            if (Pool.TryDequeue(out T obj))
                return obj;

            return (InstantiateObjects(1))[0];
        }

        public void ReturnToQueue(params T[] objectsToReturn)
        {
            foreach (var obj in objectsToReturn)
            {
                obj.enabled = false;
                Pool.Enqueue(obj);
            }
        }

        public void Free(params T[] objectsToFree) => DestroyObjects(objectsToFree);

        protected void CreateAndEnqueue(int amount) => ReturnToQueue(InstantiateObjects(amount));

        protected T[] InstantiateObjects(int addedSize)
        {
            T[] objects = new T[addedSize];

            for (int i = 0; i < addedSize; i++)
            {
                objects[i] = (T)instantiate(prefab);
            }

            AllObjects.AddRange(objects);

            return objects;
        }

        protected void DestroyObjects(params T[] objects)
        {
            foreach (var o in objects)
            {
                AllObjects.Remove(o);
                destroy(o);
            }
        }

        public void Dispose()
        {
            Pool.Clear();
            DestroyObjects(AllObjects.ToArray());
        }
    }
}