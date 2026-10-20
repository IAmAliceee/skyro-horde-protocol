#if UNITY_EDITOR
using AliceTools.Variables.Internal;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AliceTools.Variables.Editor
{
    [CustomEditor(typeof(GlobalVariables))]
    public class GlobalVariablesEditor : UnityEditor.Editor
    {
        public VisualTreeAsset visualTreeAsset;
        private GlobalVariables Root => (GlobalVariables)target;
        private SerializedProperty gvTypeReference;

        private void OnEnable()
        {
            gvTypeReference = serializedObject.FindProperty("variableType");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Global Variables", EditorStyles.boldLabel);

            for (int i = 0; i < Root.variables.Count; i++)
            {
                var variable = Root.variables[i];

                if (variable == null)
                {
                    EditorGUILayout.HelpBox($"Variable at index {i} is missing.", MessageType.Warning);
                    continue;
                }

                DrawVariable(variable);
            }

            DrawAddSection();
        }

        private void DrawVariable(GlobalVariable variable)
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    var newName = EditorGUILayout.TextField(
                        variable.name,
                        EditorStyles.boldLabel
                    );
                    if(variable.name != newName)
                    {
                        if(GlobalVariableAssetUtility.RenameVariable(Root, variable, newName))
                        {
                            variable.name = newName;
                        }
                    }

                    if (GUILayout.Button("Remove", GUILayout.Width(70)))
                    {
                        GlobalVariableAssetUtility.DeleteVariable(Root, variable);
                        return;
                    }
                }

                var variableEditor = CreateEditor(variable);
                variableEditor.OnInspectorGUI();
            }

            EditorGUILayout.Space(4);
        }

        private void DrawAddSection()
        {
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(gvTypeReference);
            serializedObject.ApplyModifiedProperties();

            if(GUILayout.Button("Add", GUILayout.Width(70)))
            {
                var type = Root.variableType.Type;
                if(type == null)
                    return;
                
                var gv = GlobalVariableAssetUtility.CreateVariable(Root, type, type.Name);
                EditorUtility.SetDirty(Root);
            }
        }
    }
}

#endif