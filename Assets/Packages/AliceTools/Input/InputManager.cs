using UnityEngine;

namespace AliceTools.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputEvent[] inputs;

        private void OnEnable()
        {
            foreach (var input in inputs)
            {
                input.Register();
            }
        }

        private void OnDisable()
        {
            foreach (var input in inputs)
            {
                input.Dispose();
            }
        }
    }
}