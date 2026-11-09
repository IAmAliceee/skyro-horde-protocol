using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy.AbstractSO
{
    public abstract class EnemyTargetingSO : ScriptableObject
    {
        public abstract Transform FindTarget(EnemyRuntime enemy);
    }
}