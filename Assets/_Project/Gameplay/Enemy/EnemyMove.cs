using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private Transform targetToFollow;
        [SerializeField] private Transform selfTransform;
        [SerializeField] private float moveSpeed;

        private void Update() {
            Vector3 dir = (targetToFollow.position - selfTransform.position).normalized;
            dir.z = 0f;
            selfTransform.position += moveSpeed * Time.deltaTime * dir;
        }
    }
}
