using System;

namespace HordeProtocol.Gameplay.Interfaces
{
    public interface IDamageable
    {
        void Damage(float damage);
        void SetDestroyAction(Action action);
    }
}