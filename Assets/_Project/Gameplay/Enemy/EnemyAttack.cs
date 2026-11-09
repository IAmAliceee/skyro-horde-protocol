using HordeProtocol.Gameplay.Enemy.AbstractSO;
using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy/Attack/Default")]
    public class EnemyAttack : EnemyAttackSO
    {
        public override void TryAttack(EnemyRuntime enemy, float dt)
        {
            // TODO: Implement player health
        }
    }
}
