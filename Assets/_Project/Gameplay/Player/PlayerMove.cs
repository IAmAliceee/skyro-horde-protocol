using UnityEngine;
using UnityEngine.InputSystem;

namespace HordeProtocol.Gameplay.Player {
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveActionRef;
        
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform playerT;

        private InputAction moveAction => moveActionRef.action;

        private Vector3 lastMoveInput = Vector3.zero;


        private void OnEnable() {
            moveAction.Enable();

            moveAction.performed += OnMoveAction;
            moveAction.canceled += OnMoveAction;
        }

        private void OnDisable() {
            moveAction.performed -= OnMoveAction;
            moveAction.canceled -= OnMoveAction;

            moveAction.Disable();
        }

        private void Update() {
            playerT.position += moveSpeed * Time.deltaTime * lastMoveInput;
        }

        private void OnMoveAction(InputAction.CallbackContext ctx) => lastMoveInput = ctx.ReadValue<Vector2>();
    }
}