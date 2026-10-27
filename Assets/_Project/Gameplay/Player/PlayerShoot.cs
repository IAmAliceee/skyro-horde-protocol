using System;
using System.Threading;
using AliceTools.Events;
using AliceTools.Extensions;
using AliceTools.Input;
using AliceTools.MonoBehaviours;
using AliceTools.Variables;
using UnityEngine;

namespace HordeProtocol.Gameplay.Player {
    public class PlayerShoot : CancellableEventBehaviour
    {
        #region References
        [SerializeField] private Rigidbody2D bulletPrefab;

        [Header("Inputs")]
        [SerializeField] private InputEvent shootEvent;

        [Header("Variables")]
        [SerializeField] private FloatVariable shootCooldown;
        [SerializeField] private FloatVariable bulletSpeed;

        [Header("Events")]
        [SerializeField] private GameEvent OnStopShooting;
        [SerializeField] private GameEvent OnResumeShooting;
        #endregion

        bool isShooting;
        protected override GameEvent OnStopCancellable => OnStopShooting;

        protected override void OnEnable() {
            base.OnEnable();
            if (!shootEvent.IsButton())
                throw new ArgumentException($"shootEvent ({shootEvent.name}) is not a button input!");

            RegEvent(shootEvent, () => isShooting = shootEvent.WasPressedThisFrame());
            RegEvent(OnResumeShooting, OnResume);

            _ = ShootDelay();
        }

        private void OnResume() {
            CreateNewCancelToken(true);
            _ = ShootDelay();
        }

        private async Awaitable ShootDelay()
        {
            using var tokenSrc = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken, cancelPause);
            var token = tokenSrc.Token;

            try {
                bool waitUntilShooting() => isShooting;
                while (true) {
                    await AwaitableExtensions.WaitUntil(waitUntilShooting, token);
                    var bulletRb = await InstantiateAsync(bulletPrefab, null, transform.position, transform.rotation).ToAwaitableAsync(token);
                    bulletRb.linearVelocity = (Vector2)bulletRb.transform.right * bulletSpeed;
                    await Awaitable.WaitForSecondsAsync(shootCooldown, token);
                }
            } catch (OperationCanceledException) { }
        }
    }
}