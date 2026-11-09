using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy/Stats/Default")]
    public class EnemyStats : ScriptableObject
    {
        public float MaxHealth;
        public float MoveSpeed;
        public float Damage;
        public float AttackCooldown;
    }
}