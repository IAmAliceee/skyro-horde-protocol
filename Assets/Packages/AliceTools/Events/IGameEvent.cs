using System;

namespace AliceTools.Events
{
    public interface IGameEvent : IDisposable
    {
        void Invoke();
        void Sub(Action listener);
        void Unsub(Action listener);
    }
}