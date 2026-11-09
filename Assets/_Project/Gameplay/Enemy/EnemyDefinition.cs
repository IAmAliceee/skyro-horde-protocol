using HordeProtocol.Gameplay.Enemy.AbstractSO;
using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy/Definition", order = -1)]
    public class EnemyDefinition : ScriptableObject
    {
        public EnemyStats stats;
        public EnemyMoveSO movement;
        public EnemyTargetingSO targeting;
        public EnemyAttackSO attack;
        public EnemyHealthSO health;
    }
}