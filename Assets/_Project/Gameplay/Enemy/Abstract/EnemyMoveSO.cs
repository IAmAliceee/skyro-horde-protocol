using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy.AbstractSO
{
    public abstract class EnemyMoveSO : ScriptableObject
    {
        public abstract void Move(EnemyRuntime enemy, float dt);
    }
}