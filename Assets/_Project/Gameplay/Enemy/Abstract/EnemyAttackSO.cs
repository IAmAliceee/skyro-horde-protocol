using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy.AbstractSO
{
    public abstract class EnemyAttackSO : ScriptableObject
    {
        public abstract void TryAttack(EnemyRuntime enemy, float dt);
    }
}