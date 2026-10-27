using System.Threading;
using AliceTools.Events;

namespace AliceTools.MonoBehaviours {
    public abstract class CancellableEventBehaviour : EventBehaviour
    {
        protected abstract GameEvent OnStopCancellable { get; }

        protected CancellationTokenSource cancelPauseSource { get; private set; }
        protected CancellationToken cancelPause => cancelPauseSource.Token;

        protected virtual void OnEnable() => CreateNewCancelToken();

        protected void CreateNewCancelToken(bool allowDisposing = false) {
            if (cancelPauseSource != null) {
                if (allowDisposing) {
                    DisposeOfPauseToken();
                } else {
                    return;
                }
            }

            cancelPauseSource = new();
            RegEvent(OnStopCancellable, cancelPauseSource.Cancel);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            DisposeOfPauseToken();
        }

        protected virtual void OnDestroy() => DisposeOfPauseToken();

        private void DisposeOfPauseToken()
        {
            if(cancelPauseSource == null) return;
            cancelPauseSource.Cancel();
            cancelPauseSource.Dispose();
            cancelPauseSource = null;
        }
    }
}