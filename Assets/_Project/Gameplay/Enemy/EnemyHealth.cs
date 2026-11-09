using HordeProtocol.Gameplay.Enemy.AbstractSO;
using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy/Health/Default")]
    public class EnemyHealth : EnemyHealthSO
    {
        public override float Damage(EnemyRuntime enemy, float damage, float health)
        {
            float newHp = health - damage;

            if (newHp <= 0f)
                enemy.Destroy();

            return newHp;
        }
    }
}