using System;
using System.Collections.Generic;
using AliceTools.Events;
using UnityEngine;

namespace AliceTools.MonoBehaviours
{
    public abstract class EventBehaviour : MonoBehaviour
    {
        private Dictionary<Action, List<IGameEvent>> registeredEvents = new(); 

        protected virtual void OnDisable() => UnregAction();

        protected void RegEvent(IGameEvent gameEvent, Action action)
        {
            if (registeredEvents.TryGetValue(action, out var events))
            {
                if (events.Contains(gameEvent)) return;
                events.Add(gameEvent);
            }
            else
            {
                registeredEvents.Add(action, new(1) { gameEvent });
            }

            gameEvent.Sub(action);
        }

        protected void UnregEvent(IGameEvent gameEvent, Action action)
        {
            if(!registeredEvents.TryGetValue(action, out var events)) return;
            if(!events.Contains(gameEvent)) return;

            gameEvent.Unsub(action);
            if(events.Count <= 1)
                registeredEvents.Remove(action);
            else
                events.Remove(gameEvent);
        }

        protected void RegEvent(params (IGameEvent, Action)[] eventActionPair)
        {
            foreach (var (gameEvent, action) in eventActionPair)
            {
                RegEvent(gameEvent, action);
            }
        }

        protected void UnregEvent(params (IGameEvent, Action)[] eventActionPair)
        {
            foreach (var (gameEvent, action) in eventActionPair)
            {
                UnregEvent(gameEvent, action);
            }
        }

        protected void RegEvent(IGameEvent gameEvent, params Action[] actions)
        {
            foreach(var action in actions)
            {
                RegEvent(gameEvent, action);
            }
        }

        protected void UnregEvent(IGameEvent gameEvent, params Action[] actions)
        {
            foreach (var action in actions)
            {
                UnregEvent(gameEvent, action);
            }
        }

        protected void RegEvent(Action action, params IGameEvent[] gameEvents)
        {
            foreach(var gameEvent in gameEvents)
            {
                RegEvent(gameEvent, action);
            }
        }

        protected void UnregAction(Action action)
        {
            if(!registeredEvents.TryGetValue(action, out var events)) return;

            foreach(var gameEvent in events)
            {
                gameEvent.Unsub(action);
            }
            registeredEvents.Remove(action);
        } 

        protected void UnregAction()
        {
            foreach(var action in registeredEvents.Keys)
            {
                foreach(var gameEvent in registeredEvents[action])
                {
                    gameEvent.Unsub(action);
                }
            }
            registeredEvents.Clear();
        }
    }
}