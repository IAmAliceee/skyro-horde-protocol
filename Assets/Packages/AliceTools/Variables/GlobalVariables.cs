using System.Collections.Generic;
using AliceTools.Variables.Internal;
using UnityEngine;

namespace AliceTools.Variables
{
    [CreateAssetMenu(fileName = "Variables", menuName = "Alice/Variables")]
    public class GlobalVariables : ScriptableObject
    {
        public GVTypeReference variableType;
        [SerializeReference] public List<GlobalVariable> variables = new();

        public T Get<T>(string name) where T : GlobalVariable
        {
            foreach (var variable in variables)
            {
                if(variable != null && variable.name == name && variable is T typed)
                    return typed;
            }

            return null;
        }
    }
}