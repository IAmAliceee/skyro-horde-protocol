using System;

namespace AliceTools.Pool
{
    public interface IPoolDestroy
    {
        void SetDestroyAction(Action action);
        void Destroy();
    }
}