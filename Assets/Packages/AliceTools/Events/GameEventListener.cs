using System;
using AliceTools.MonoBehaviours;
using UnityEngine;
using UnityEngine.Events;

namespace AliceTools.Events
{
    [DefaultExecutionOrder(-50)]
    public class GameEventListener : EventBehaviour
    {
        [field: SerializeField] public GameEvent gameEvent { get; protected set; }
        [SerializeField] protected UnityEvent unityEvent;

        protected virtual void OnEnable() => RegEvent(gameEvent, unityEvent.Invoke);
    }
}