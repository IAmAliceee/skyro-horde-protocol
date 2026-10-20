using System;
using UnityEngine;

namespace AliceTools.Variables
{
    public abstract class GlobalVariable<T> : GlobalVariable
    {
        [SerializeField] protected T _value;

        public override object ObjectValue => Value;

        public T Value
        {
            get => _value;
            set
            {
                if (IsReadOnly) return;
                _value = value;

                OnChanged?.Invoke(Value);
            }
        }

        public event Action<T> OnChanged;

        public static implicit operator T(GlobalVariable<T> gv) => gv.Value;
    }
}