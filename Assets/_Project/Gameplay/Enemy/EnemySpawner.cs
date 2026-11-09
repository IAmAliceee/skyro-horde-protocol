using System.Collections;
using AliceTools.Pool;
using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float spawnDelay;
        [SerializeField, Min(0f)] private int spawnCount;
        [SerializeField] private EnemyRuntime enemyPrefab;

        private RuntimePool<EnemyRuntime> enemies;

        private int spawnCountCurrent;

        private void OnDestroy() => enemies.Dispose();

        private void Start() => StartCoroutine(StartIE());

        private IEnumerator StartIE()
        {
            enemies = new(enemyPrefab);

            while (true)
            {
                var enemy = enemies.Get();
                enemy.enabled = true;
                enemy.transform.position = transform.position;
                spawnCountCurrent++;
                if(spawnCount > 0 && spawnCountCurrent >= spawnCount) break;
                yield return new WaitForSeconds(spawnDelay);
            }

        }
    }
}