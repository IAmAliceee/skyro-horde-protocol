#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace AliceTools.Variables.Internal
{
    public static class GlobalVariableAssetUtility
    {
        public static GlobalVariable CreateVariable(
            GlobalVariables root,
            System.Type concreteType,
            string name)
        {
            if (!typeof(GlobalVariable).IsAssignableFrom(concreteType))
            {
                Debug.LogError($"{concreteType.Name} is not a GlobalVariable.");
                return null;
            }

            if(VariableExists(root, name))
            {
                Debug.LogError($"'{name}' already exists!");
                return null;
            }

            var variable = (GlobalVariable)ScriptableObject.CreateInstance(concreteType);
            variable.name = name;

            var so = new SerializedObject(variable);
            var monoScriptProperty = so.FindProperty("m_Script");
            if (monoScriptProperty != null && monoScriptProperty.propertyType == SerializedPropertyType.ObjectReference) {
                var monoScript = MonoScript.FromScriptableObject(variable);
                monoScriptProperty.objectReferenceValue = monoScript;
                so.ApplyModifiedProperties();
            }

            AssetDatabase.AddObjectToAsset(variable, root);
            root.variables.Add(variable);

            EditorUtility.SetDirty(variable);
            EditorUtility.SetDirty(root);
            AssetDatabase.SaveAssets();

            return variable;
        }

        public static bool VariableExists(GlobalVariables root, string name)
        {
            foreach (var item in root.variables)
            {
                if (item.name == name)
                    return true;
            }

            return false;
        }

        public static void DeleteVariable(GlobalVariables root, GlobalVariable variable)
        {
            root.variables.Remove(variable);
            Undo.DestroyObjectImmediate(variable);

            EditorUtility.SetDirty(root);
            AssetDatabase.SaveAssets();
        }

        public static bool RenameVariable(GlobalVariables root, GlobalVariable variable, string newName)
        {
            if(root == null)
            {
                Debug.LogError("Cannot rename variable: root is null.");
                return false;
            }

            if (variable == null)
            {
                Debug.LogError("Cannot rename variable: variable is null.");
                return false;
            }

            if (!root.variables.Contains(variable))
            {
                Debug.LogError("Cannot rename variable: it does not belong to this root asset.");
                return false;
            }

            newName = newName.Trim();

            if(string.IsNullOrEmpty(newName))
            {
                Debug.LogWarning("Variable name cannot be empty.");
                return false;
            }

            if(variable.name == newName)
                return true;

            if(VariableExists(root, newName))
            {
                Debug.LogError($"'{newName}' already exists!");
                return false;
            }

            Undo.RecordObject(variable, "Rename Global Variable");

            variable.name = newName;

            EditorUtility.SetDirty(variable);
            EditorUtility.SetDirty(root);
            AssetDatabase.SaveAssets();

            return true;
        }
    }
}
#endif