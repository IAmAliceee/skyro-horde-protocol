using AliceTools.Events;
using AliceTools.Input;
using AliceTools.MonoBehaviours;
using AliceTools.Variables;
using UnityEngine;

namespace HordeProtocol.Gameplay.Player {
    public class PlayerMove : EventSingleton<PlayerMove>
    {
        #region References
        [Header("Variables")]
        [SerializeField] private FloatVariable moveSpeed;

        [Header("Unity References")]
        [SerializeField] private Transform playerT;

        [Header("Inputs")]
        [SerializeField] private InputEvent moveEvent;

        [Header("Events")]
        [SerializeField] private GameEvent OnStopMovement;
        [SerializeField] private GameEvent OnResumeMovement;
        #endregion

        private Vector3 lastMoveInput = Vector3.zero;

        private void OnEnable() {
            RegEvent(        (moveEvent, () => lastMoveInput = moveEvent.ReadValue<Vector2>()),
                        (OnStopMovement, () => enabled = false),
                      (OnResumeMovement, () => enabled = true)
            );
        }

        private void Update() {
            playerT.position += moveSpeed * Time.deltaTime * lastMoveInput;
        }
    }
}