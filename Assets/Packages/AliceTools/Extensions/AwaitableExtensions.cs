using System;
using System.Threading;
using UnityEngine;

namespace AliceTools.Extensions
{
    public static class AwaitableExtensions
    {
        public static async Awaitable WaitUntil(Func<bool> condition, CancellationToken cancellationToken) {
            while (!condition()) {
                cancellationToken.ThrowIfCancellationRequested();
                await Awaitable.NextFrameAsync();
            }
        }

        public static async Awaitable<T> ToAwaitableAsync<T>(this AsyncInstantiateOperation<T> operation, CancellationToken cancellationToken = default) where T : UnityEngine.Object {
            var completion = new AwaitableCompletionSource<T>();

            void OnCompleted(AsyncOperation _) => completion.SetResult(operation.Result[0]);

            operation.completed += OnCompleted;

            try
            {
                return await completion.Awaitable;
            } finally {
                operation.completed -= OnCompleted;
            }
        }
    }
}