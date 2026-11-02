using AliceTools.Variables;
using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private FloatVariable maxHealth;
        private float health;

        private void OnEnable()
        {
            health = maxHealth;
        }

        public void Damage(float damage)
        {
            health -= damage;

            if (health <= 0f)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}