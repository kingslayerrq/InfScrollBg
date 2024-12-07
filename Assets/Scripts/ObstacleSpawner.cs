using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour, IObserver
{
    public static ObstacleSpawner Instance;
    [Header("Spawner Settings")] 
    [SerializeField] private bool isSpawning = false;
    [SerializeField] private float _baseSpawnInterval = 2f; // Base time interval between spawns
    [SerializeField] private float _difficultyIncreaseRate = 0.1f;
    [SerializeField] private float _difficultyIncreaseTimeInterval = 10f;
    [SerializeField] private float _maxDifficulty = 5f; 
    [SerializeField] private float _spawnDistance = 10f; // Distance ahead of the player to spawn obstacles
    public  List<GameObject> spawnedObstacles = new List<GameObject>();
    [Header("Obstacle Settings")]
    public List<GameObject> obstacles = new List<GameObject>(); // List of obstacle prefabs
    public Transform player; 

    public float currentDifficulty = 1f; 
    private float timeSinceLastSpawn = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform not assigned to ObstacleSpawner!");
            return;
        }
        
        RunManager.Instance.AddObserver(Instance);
        
        StartCoroutine(DifficultyScaler());
    }
    
    void Update()
    {
        if (!isSpawning) return;
        
        timeSinceLastSpawn += Time.deltaTime;

        // Adjust spawn interval based on current difficulty
        float spawnInterval = _baseSpawnInterval / currentDifficulty;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnObstacle();
            timeSinceLastSpawn = 0f;
        }
    }

    public void OnNotify(string sceneName)
    {
        switch (sceneName)
        {
            case "MenuScene":
                break;
            case "GameScene":
                isSpawning = true;
                break;
            case "EndScene":
                isSpawning = false;
                // Set all children(Spawned obstacles) immobile
                spawnedObstacles.ForEach((obstacle) => obstacle.GetComponent<Entity>().shouldStop = true);
                break;
            default:
                break;
        }
    }

    void SpawnObstacle()
    {
        if (obstacles.Count == 0) return;

        // Randomly select an obstacle
        GameObject obstaclePrefab = obstacles[Random.Range(0, obstacles.Count)];

        // Spawn the obstacle ahead of the player
        Vector3 spawnPosition = new Vector3(
            player.position.x + _spawnDistance,
            -0.5f, 
            player.position.z
        );

        var obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, transform);
        spawnedObstacles.Add(obstacle);
    }

    IEnumerator DifficultyScaler()
    {
        while (currentDifficulty < _maxDifficulty)
        {
            yield return new WaitForSeconds(_difficultyIncreaseTimeInterval); // Increase difficulty every 10 seconds
            currentDifficulty += _difficultyIncreaseRate;
            currentDifficulty = Mathf.Min(currentDifficulty, _maxDifficulty);
        }
    }

    private void OnDestroy()
    {
        RunManager.Instance.RemoveObserver(Instance);
    }
}
