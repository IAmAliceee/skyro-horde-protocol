using AliceTools.Variables;
using HordeProtocol.Gameplay.Enemy;
using UnityEngine;

namespace HordeProtocol.Gameplay.Player
{
    public class BulletCollision : MonoBehaviour
    {
        [SerializeField] private StringVariable enemyTag;
        [SerializeField] private FloatVariable bulletDamage;
        [SerializeField] private BoolVariable shouldDestroyItself;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.tag != enemyTag || !col.collider.TryGetComponent<IDamageable>(out var enemyHealth))
                return;

            enemyHealth.Damage(bulletDamage);
            if(shouldDestroyItself) Destroy(gameObject);
        }
    }
}