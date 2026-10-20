#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;

namespace AliceTools.Events.Editor {
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : UnityEditor.Editor
    {
        public GameEvent Root => (GameEvent)target;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var invokeBtn = new Button {
                text = "Invoke Event",
                name = "invoke-button"
            };

            invokeBtn.clicked += OnInvokeButtonClicked;
            root.Add(invokeBtn);
            
            return root;
        }

        private void OnInvokeButtonClicked() => Root.Invoke();
    }
}

#endif