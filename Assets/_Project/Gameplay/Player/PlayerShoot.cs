using System;
using System.Threading;
using AliceTools.Events;
using AliceTools.Extensions;
using AliceTools.Input;
using AliceTools.MonoBehaviours;
using AliceTools.Pool;
using AliceTools.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private Camera cam;
        private RuntimePool<Rigidbody2D> bulletPool;

        protected override void OnEnable() {
            base.OnEnable();
            if (!shootEvent.IsButton())
                throw new ArgumentException($"shootEvent ({shootEvent.name}) is not a button input!");

            RegEvent(shootEvent, () => isShooting = shootEvent.WasPressedThisFrame());
            RegEvent(OnResumeShooting, OnResume);

            cam = Camera.main;
            bulletPool = new(bulletPrefab, 6);
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

                    Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
                    mouseScreenPos.z = Mathf.Abs(cam.transform.position.z);

                    var mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);
                    mouseWorldPos.z = 0f;

                    var bulletDir = ((Vector2)mouseWorldPos - (Vector2)transform.position).normalized;

                    var bulletRb = bulletPool.Get();
                    bulletRb.transform.position = transform.position;
                    bulletRb.gameObject.SetActive(true);

                    bulletRb.linearVelocity = bulletDir * bulletSpeed;

                    await Awaitable.WaitForSecondsAsync(shootCooldown, token);
                }
            } catch (OperationCanceledException) { }
        }
    }
}