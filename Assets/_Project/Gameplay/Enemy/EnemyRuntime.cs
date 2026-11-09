using System;
using AliceTools.Events;
using AliceTools.MonoBehaviours;
using AliceTools.Pool;
using HordeProtocol.Gameplay.Interfaces;
using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyRuntime : EventBehaviour, IPoolDestroy, IDamageable
    {
        [Header("Config")]
        [SerializeField] private EnemyDefinition enemyType;

        [Header("Events")]
        [SerializeField] private GameEvent OnStopMovement;
        [SerializeField] private GameEvent OnResumeMovement;

        public Transform Target => target;
        public EnemyStats Stats => enemyType.stats;

        private Transform target;
        private float currentHealth;

        private void OnEnable()
        {
            RegEvent(   (OnStopMovement, () => enabled = false),
                      (OnResumeMovement, () => enabled = true)
            );

            currentHealth = Stats.MaxHealth;
        }

        private void Update()
        {
            target ??= enemyType.targeting.FindTarget(this);
            enemyType.movement.Move(this, Time.deltaTime);
            enemyType.attack.TryAttack(this, Time.deltaTime);
        }

        #region IPoolDestroy_Section

        private System.Action destroyAction;

        public void Destroy() => destroyAction?.Invoke();

        public void SetDestroyAction(Action action) => destroyAction ??= action;

        #endregion
        #region IDamageable_Section
        public void Damage(float damage) => currentHealth = enemyType.health.Damage(this, damage, currentHealth);
        #endregion
    }
}