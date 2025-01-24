using System;
using UnityEngine;
using Random = UnityEngine.Random;

enum WaveState
{
    Spawning,
    Waiting,
    Counting
}

enum SpawnMode
{
    Wave,
    Constant
}

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private Wave[] waves;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float searchCountdown = 1f;
    [SerializeField] private WaveState state = WaveState.Counting;
    [SerializeField] private SpawnMode spawnMode = SpawnMode.Wave;
    
    [Header("Spawn Warning")]
    [SerializeField] private bool showSpawnWarning = true;
    [SerializeField] private float spawnWarningTime = 1.5f;
    [SerializeField] private GameObject spawnWarningPrefab;
    
    [Header("Spawn Area")]
    [SerializeField] private Vector2 spawnArea = new Vector2(20, 20);
    
    [Header("Debug")]
    [SerializeField] private bool debug = false;

    private float _waveCountdown;
    private int _waveIndex;
    
    private void Start()
    {
        _waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == WaveState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (_waveCountdown <= 0)
        {
            if (state != WaveState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[_waveIndex]));
            }
        }
        else
        {
            _waveCountdown -= Time.deltaTime;
        }
    }

    private void WaveCompleted()
    {
        state = WaveState.Counting;
        _waveCountdown = timeBetweenWaves;

        if (_waveIndex + 1 > waves.Length - 1)
        {
            _waveIndex = 0;
        }
        else
        {
            _waveIndex++;
        }
    }

    private bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }

        return true;
    }

    private System.Collections.IEnumerator SpawnWave(Wave wave)
    {
        state = WaveState.Spawning;

        for (int i = 0; i < wave.count; i++)
        {
            if (showSpawnWarning)
            {
                StartCoroutine(SpawnEnemy());
            }
            else
            {
                SpawnEnemyInstant(wave.enemy);
            }
            yield return new WaitForSeconds(1f / wave.rate);
        }

        state = WaveState.Waiting;
    }

    private System.Collections.IEnumerator SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x)/2, Random.Range(-spawnArea.y, spawnArea.y)/2, 0);
        GameObject spawnWarningObject = Instantiate(spawnWarningPrefab, spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(spawnWarningTime);
        Instantiate(waves[_waveIndex].enemy, spawnPosition, Quaternion.identity);
        Destroy(spawnWarningObject);
    }
    
    private void SpawnEnemyInstant(GameObject enemy)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x)/2, Random.Range(-spawnArea.y, spawnArea.y)/2, 0);
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (!debug) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}

[Serializable]
public class Wave
{
    public GameObject enemy;
    public int count;
    public float rate;
}
