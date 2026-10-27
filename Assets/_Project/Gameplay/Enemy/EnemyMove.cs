using AliceTools.Events;
using AliceTools.MonoBehaviours;
using AliceTools.Variables;
using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy
{
    public class EnemyMove : EventBehaviour
    {
        #region References
        [Header("References")]
        [SerializeField] private Transform targetToFollow;
        [SerializeField] private Transform selfTransform;

        [Header("Variables")]
        [SerializeField] private FloatVariable moveSpeed;

        [Header("Events")]
        [SerializeField] private GameEvent OnStopMovement;
        [SerializeField] private GameEvent OnResumeMovement;
        #endregion

        private void Start() {
            RegEvent(   (OnStopMovement, () => enabled = false),
                      (OnResumeMovement, () => enabled = true)
            );
        }

        private void Update() {
            Vector3 dir = (targetToFollow.position - selfTransform.position).normalized;
            dir.z = 0f;
            selfTransform.position += moveSpeed * Time.deltaTime * dir;
        }
    }
}
