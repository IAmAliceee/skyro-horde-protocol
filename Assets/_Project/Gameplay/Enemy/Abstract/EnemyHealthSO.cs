using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy.AbstractSO
{
    public abstract class EnemyHealthSO : ScriptableObject
    {
        public abstract float Damage(EnemyRuntime enemy, float damage, float health);
    }
}