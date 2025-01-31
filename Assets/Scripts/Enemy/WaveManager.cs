using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [System.Serializable]
    public class WaveAction
    {
        public EnemyBase prefab;
        public int spawnCount;
    }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public float duration = 10;
        public List<WaveAction> actions;
    }
    
    public class WaveManager : MonoBehaviour
    {
        [Header("Waves Settings")]
        [SerializeField] private float timeDifficultyFactor = 0.1f;
        [SerializeField] private float enemyDifficultyFactor = 0.1f;
        [SerializeField] private Vector2 spawnArea = new(20, 20);
        [SerializeField] private float enemySpawnDelay = 1.0f;
        [SerializeField] private List<Wave> waves;
        
        [Header("Spawn Warning")]
        [SerializeField] private bool showSpawnWarning = true;
        [SerializeField] private float spawnWarningTime = 1.5f;
        [SerializeField] private GameObject spawnWarningPrefab;
    
        [Header("Debug")]
        [SerializeField] private bool debug = false;
        
        public Wave CurrentWave => _currentWave;
        
        private float _currentDelayMultiplier = 1.0f;
        private float _currentDifficultyMultiplier = 1.0f;
        private Wave _currentWave;
        private int _currentWaveIndex = 0;
        
        private float _currentEnemySpawnDelay;

        private void Start()
        {
            _currentEnemySpawnDelay = enemySpawnDelay;
            StartCoroutine(SpawnLoop());
        }
        
        IEnumerator SpawnLoop()
        {
            yield return new WaitForSeconds(5f);
            while(true)
            {
                foreach(Wave currentWave in waves)
                {
                    _currentWave = currentWave;
                    _currentWaveIndex++;
                    foreach(WaveAction action in currentWave.actions)
                    {
                        if (action.prefab != null && action.spawnCount > 0)
                        {
                            for(int i = 0; i < action.spawnCount; i++)
                            {
                                Vector3 spawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), 0);
                                GameObject enemyPrefab = action.prefab.gameObject;
                                if (showSpawnWarning)
                                {
                                    StartCoroutine(SpawnEnemyAfterWarning(spawnPosition, enemyPrefab));
                                } 
                                else
                                {
                                    SpawnEnemy(enemyPrefab, spawnPosition);
                                }
                                yield return new WaitForSeconds(_currentEnemySpawnDelay);
                            }
                        }
                    }
                    yield return new WaitForSeconds(_currentWave.duration);
                }
                _currentDelayMultiplier *= timeDifficultyFactor;
                _currentEnemySpawnDelay *= timeDifficultyFactor;
                _currentDifficultyMultiplier *= 1 + enemyDifficultyFactor;
                yield return null;
            }
            yield return null;
        }
        
        private IEnumerator SpawnEnemyAfterWarning(Vector3 spawnPosition, GameObject enemyPrefab)
        {
            GameObject spawnWarningObject = Instantiate(spawnWarningPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnWarningTime);
            Destroy(spawnWarningObject);
            SpawnEnemy(enemyPrefab, spawnPosition);
        }

        private void SpawnEnemy(GameObject enemyPrefab, Vector3 spawnPosition)
        {
            EnemyBase enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity).GetComponent<EnemyBase>();
            enemy.Stats.IncreaseStats(_currentDifficultyMultiplier * GameManager.Instance.GameDifficultyFactor);
        }

        #region Debug

        private void OnDrawGizmos()
        {
            if (!debug) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(spawnArea.x * 2, spawnArea.y * 2, 0));
        }

        #endregion
    }
}