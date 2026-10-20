using UnityEngine;

namespace AliceTools.MonoBehaviours {
    [DefaultExecutionOrder(-50)]
    public abstract class EventSingleton<T> : EventBehaviour where T : EventSingleton<T>
    {
        public static T Instance { get; protected set; }

        protected virtual bool IsNotDestroyedOnLoad => false;

        protected virtual void Awake() {
            if (!Instance) {
                Instance = this as T;
                if (IsNotDestroyedOnLoad && Application.isPlaying)
                    DontDestroyOnLoad(gameObject);

                OnSingletonInitialized();
            } else {
                if (Application.isPlaying) {
                    Destroy(gameObject);
                } else {
                    DestroyImmediate(gameObject);
                }
            }
        }

        protected virtual void OnDestroy() {
            if(Instance == this as T)
                DestroyInstance();
        }

        public static void DestroyInstance() {
            if (!Instance) return;

            Instance.OnSingletonDestroyed();
            Instance = default;
        }

        protected virtual void OnSingletonInitialized() { }
        protected virtual void OnSingletonDestroyed() { }
    }
}