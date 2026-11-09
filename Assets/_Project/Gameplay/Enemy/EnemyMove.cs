using HordeProtocol.Gameplay.Enemy.AbstractSO;
using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy/Move/Default")]
    public class EnemyMove : EnemyMoveSO
    {
        public override void Move(EnemyRuntime enemy, float dt)
        {
            if (!enemy.Target) return;
            Vector3 dir = (enemy.Target.position - enemy.transform.position).normalized;
            dir.z = 0f;
            enemy.transform.position += enemy.Stats.MoveSpeed * dt * dir;
        }
    }
}
