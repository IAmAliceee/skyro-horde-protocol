using UnityEngine;

namespace AliceTools.Variables
{
    public abstract class GlobalVariable : ScriptableObject
    {
        [field: SerializeField] public bool IsReadOnly { get; protected set; }

        public abstract object ObjectValue { get; }
    }
}