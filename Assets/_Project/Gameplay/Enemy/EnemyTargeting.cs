using HordeProtocol.Gameplay.Enemy.AbstractSO;
using HordeProtocol.Gameplay.Player;
using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy/Targeting/Default")]
    public class EnemyTargeting : EnemyTargetingSO
    {
        public override Transform FindTarget(EnemyRuntime enemy) => PlayerMove.Instance.transform;
    }
}
