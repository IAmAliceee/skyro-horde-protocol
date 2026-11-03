using AliceTools.Pool;
using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float spawnDelay;
        [SerializeField] private EnemyMove enemyPrefab;

        private RuntimePool<EnemyMove> enemies;

        private async void Start()
        {
            enemies = new(enemyPrefab);

            while (true)
            {
                var enemy = enemies.Get();
                enemy.enabled = true;
                enemy.transform.position = transform.position;

                await Awaitable.WaitForSecondsAsync(spawnDelay, destroyCancellationToken);
            }

        }
    }
}