using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AliceTools.Pool
{
    public class RuntimePool<T> : IDisposable where T : UnityEngine.Object
    {
        #region References
        protected Queue<T> Pool;
        protected List<T> AllObjects;
        protected Func<UnityEngine.Object, UnityEngine.Object> instantiate;
        protected Action<UnityEngine.Object> destroy;
        protected T prefab;
        protected int currentSpawnIndex = 0;
        #endregion

        public RuntimePool(T prefab, uint initSize = 0)
            => Init(prefab, MonoBehaviour.Instantiate, MonoBehaviour.Destroy, initSize);

        public RuntimePool(T prefab, Func<UnityEngine.Object, UnityEngine.Object> instantiateFunc, Action<UnityEngine.Object> destroyMethod, uint initSize = 0)
            => Init(prefab, instantiateFunc, destroyMethod, initSize);

        protected void Init(T prefab, Func<UnityEngine.Object, UnityEngine.Object> instantiateFunc, Action<UnityEngine.Object> destroyMethod, uint initSize = 0)
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
                DisableObject(obj);
                Pool.Enqueue(obj);
            }
        }

        protected void DisableObject(T obj)
        {
            if (obj is IDisposable disp)
                disp.Dispose();
            else if (obj is Component comp)
                comp.gameObject.SetActive(false);
        }

        public void Free(params T[] objectsToFree) => DestroyObjects(objectsToFree);

        protected void CreateAndEnqueue(int amount) => ReturnToQueue(InstantiateObjects(amount));

        protected T[] InstantiateObjects(int addedSize)
        {
            T[] objects = new T[addedSize];

            for (int i = 0; i < addedSize; i++)
            {
                var obj = (T)instantiate(prefab);
                objects[i] = obj;
                if (obj is Component comp && comp.TryGetComponent<IPoolDestroy>(out var poolDestroy))
                    poolDestroy.SetDestroyAction(() => ReturnToQueue(obj));
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