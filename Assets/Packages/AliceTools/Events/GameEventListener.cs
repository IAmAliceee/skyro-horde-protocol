using System;
using AliceTools.MonoBehaviours;
using UnityEngine;
using UnityEngine.Events;

namespace AliceTools.Events
{
    public class GameEventListener : EventBehaviour
    {
        [field: SerializeField] public GameEvent gameEvent { get; protected set; }
        [SerializeField] protected UnityEvent unityEvent;

        protected virtual void OnEnable() => RegEvent(gameEvent, unityEvent.Invoke);
    }
}