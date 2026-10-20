#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using AliceTools.Variables.Internal;
using UnityEditor;
using UnityEngine;

namespace AliceTools.Variables.Editor
{
    [CustomPropertyDrawer(typeof(GVTypeReference))]
    public class GVTypeReferenceDrawer : PropertyDrawer
    {
        private const string BaseType = nameof(GlobalVariable);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var typeNameProp = property.FindPropertyRelative("assemblyQualifiedName");

            var derivedTypes = TypeCache.GetTypesDerivedFrom<GlobalVariable>();
            var validTypes = new List<Type>();

            foreach (var type in derivedTypes)
            {
                if(!type.IsAbstract && !type.IsGenericType)
                    validTypes.Add(type);
            }

            validTypes.Sort((x, y) => string.Compare(x.Name, y.Name));

            var names = new string[validTypes.Count + 1];
            names[0] = "None";

            int selectedIndex = 0;

            var currentType = GVTypeReference.GetTypeFromSerializedValue(typeNameProp.stringValue);

            for (int i = 0; i < validTypes.Count; i++)
            {
                names[i + 1] = validTypes[i].Name;

                if(validTypes[i] == currentType)
                    selectedIndex = i + 1;
            }

            int newIndex = EditorGUI.Popup(position, label.text, selectedIndex, names);

            typeNameProp.stringValue = newIndex == 0
                ? string.Empty
                : validTypes[newIndex - 1].AssemblyQualifiedName;
        }
    }
}
#endif