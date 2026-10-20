using System;
using UnityEngine;

namespace AliceTools.Events
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Alice/Game Event", order = 0)]
    public class GameEvent : ScriptableObject, IGameEvent
    {
        protected event Action OnEventRaised;

        public virtual void Invoke() => OnEventRaised?.Invoke();

        public virtual void Sub(Action listener) => OnEventRaised += listener;
        public virtual void Unsub(Action listener) => OnEventRaised -= listener;
        public virtual void Dispose() => OnEventRaised = null;
    }
}