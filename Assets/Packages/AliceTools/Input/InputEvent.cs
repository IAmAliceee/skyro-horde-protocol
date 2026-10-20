using System;
using System.Collections.Generic;
using AliceTools.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AliceTools.Input
{
    [CreateAssetMenu(fileName = "InputEvent", menuName = "Alice/Input Event")]
    public class InputEvent : ScriptableObject, IGameEvent
    {
        protected Dictionary<Action<InputAction.CallbackContext>, InputActionType> registeredInputs = new();
        protected Dictionary<Action, Action<InputAction.CallbackContext>> callbacklessActions = new();
        [SerializeField] protected InputActionReference inputAction;

        public void Register()
        {
            inputAction.action.Enable();
        }
        
        public void Dispose()
        {
            foreach (var action in registeredInputs.Keys)
            {
                Unsub(action);
            }

            registeredInputs.Clear();
            inputAction.action.Disable();
        }

        public enum InputActionType
        {
            Started,
            Canceled,
            Performed
        }

        public void Sub(Action<InputAction.CallbackContext> action, InputActionType type = InputActionType.Started)
        {
            if(registeredInputs.ContainsKey(action)) return;
            ModifySub(action, type, add: true);
            registeredInputs[action] = type;
        }

        public void Unsub(Action<InputAction.CallbackContext> action)
        {
            if (!registeredInputs.TryGetValue(action, out var type)) return;
            ModifySub(action, type, add: false);
            registeredInputs.Remove(action);
        }

        protected void ModifySub(Action<InputAction.CallbackContext> action, InputActionType type, bool add)
        {
            switch (type)
            {
                case InputActionType.Started:
                    if (add)
                        inputAction.action.started += action;
                    else
                        inputAction.action.started -= action;
                    break;

                case InputActionType.Canceled:
                    if (add)
                        inputAction.action.canceled += action;
                    else
                        inputAction.action.canceled -= action;
                    break;

                case InputActionType.Performed:
                    if (add)
                        inputAction.action.performed += action;
                    else
                        inputAction.action.performed -= action;
                    break;
            }
        }

        public T ReadValue<T>() where T : struct => inputAction.action.ReadValue<T>();

        public void Invoke()
        {
            throw new NotImplementedException("Input Events should not be Invoke-d");
        }

        public void Sub(Action listener) => Sub(listener, InputActionType.Performed);

        public void Sub(Action listener, InputActionType type)
        {
            Action<InputAction.CallbackContext> newContextListener = (_) => listener();
            callbacklessActions.Add(listener, newContextListener);
            Sub(newContextListener, type);
        }

        public void Unsub(Action listener)
        {
            if(!callbacklessActions.TryGetValue(listener, out var contextListener)) return;
            Unsub(contextListener);
            callbacklessActions.Remove(listener);
        }

        public void Clear()
        {
            foreach(var inputAction in registeredInputs.Keys)
            {
                Unsub(inputAction);
            }
            registeredInputs.Clear();
            callbacklessActions.Clear();
        }
    }
}