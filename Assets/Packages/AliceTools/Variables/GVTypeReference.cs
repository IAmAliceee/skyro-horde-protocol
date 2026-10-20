using System;
using UnityEngine;

namespace AliceTools.Variables.Internal
{
    [Serializable]
    public class GVTypeReference
    {
        [SerializeField] private string assemblyQualifiedName;

        public Type Type
        {
            get => GetTypeFromSerializedValue(assemblyQualifiedName);

            set => assemblyQualifiedName = value?.AssemblyQualifiedName;
        }

        public bool IsValid => Type != null;

        public static Type GetTypeFromSerializedValue(string asmQualifName)
        {
            return string.IsNullOrEmpty(asmQualifName)
                ? null
                : Type.GetType(asmQualifName);
        }
    }
}