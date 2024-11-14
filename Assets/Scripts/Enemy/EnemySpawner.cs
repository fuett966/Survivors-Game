using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IPaused
{
    [System.Serializable]
    public struct EnemyType
    {
        public GameObject prefab;
        public float spawnChance;
    }


    [SerializeField] private EnemyType[] enemyTypes;
    [SerializeField] private float initialSpawnRate = 2f;
    [SerializeField] private float spawnRateReduction = 0.1f;
    [SerializeField] private float minSpawnRate = 0.5f;
    [SerializeField] private float spawnDistance = 20f;
    private float currentSpawnRate;
    private float spawnTimer = 0f;
    private Transform player;

    public bool IsPaused { get; set; }

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        float _timeToSpawn = currentSpawnRate;
        while (true)
        {
            //yield return new WaitForSeconds(currentSpawnRate);
            yield return new WaitUntil(() => { return !IsPaused;});

            if (_timeToSpawn >= 0)
            {
                _timeToSpawn -= Time.deltaTime;
                continue;
            }
            else
            {
                _timeToSpawn = currentSpawnRate;
            }
            
            if (spawnTimer >= 10f)
            {
                spawnTimer = 0f;
                if (currentSpawnRate > minSpawnRate)
                {
                    currentSpawnRate -= spawnRateReduction;
                }
            }

            SpawnEnemy();
            spawnTimer += currentSpawnRate;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetSpawnPosition();
        GameObject enemyPrefab = GetRandomEnemyPrefab();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        float randomValue = Random.value;

        if (randomValue < 0.25f)
        {
            spawnPosition = new Vector3(player.position.x + spawnDistance, player.position.y, player.position.z + Random.Range(-spawnDistance, spawnDistance));
        }
        else if (randomValue < 0.5f)
        {
            spawnPosition = new Vector3(player.position.x - spawnDistance, player.position.y, player.position.z + Random.Range(-spawnDistance, spawnDistance));
        }
        else if (randomValue < 0.75f)
        {
            spawnPosition = new Vector3(player.position.x + Random.Range(-spawnDistance, spawnDistance), player.position.y, player.position.z + spawnDistance);
        }
        else
        {
            spawnPosition = new Vector3(player.position.x + Random.Range(-spawnDistance, spawnDistance), player.position.y, player.position.z - spawnDistance);
        }

        return spawnPosition;
    }

    GameObject GetRandomEnemyPrefab()
    {
        float totalChance = 0f;

        foreach (var enemyType in enemyTypes)
        {
            totalChance += enemyType.spawnChance;
        }

        float randomValue = Random.value * totalChance;
        float cumulativeChance = 0f;

        foreach (var enemyType in enemyTypes)
        {
            cumulativeChance += enemyType.spawnChance;
            if (randomValue < cumulativeChance)
            {
                return enemyType.prefab;
            }
        }

        return enemyTypes[0].prefab;
    }

    public void OnPause()
    {
        IsPaused = true;
    }

    public void OnResume()
    {
        IsPaused = false;
    }

    void OnEnable()
    {
        PauseManager.OnGamePaused += OnPause;
        PauseManager.OnGameResumed += OnResume;

    }

    void OnDisable()
    {
        PauseManager.OnGamePaused -= OnPause;
        PauseManager.OnGameResumed -= OnResume;
    }
}
