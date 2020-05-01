using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;


    [Header("Player")]
    public GameObject player; // prefab
    public GameObject currentPlayer; // active player clone of prefab
    public int lives = 3;

    [Header("Scoring")]
    public float score = 0;
    public float pointsAwarded = 10;
    public float pointsDeducted = 5;

    [Header("AI")]
    public GameObject hunter;
    public GameObject glider;
    public GameObject sniper;
    public GameObject rammer;
    public List<GameObject> activeEnemies;
    public int allowedEnemies;

    [Header("Spawn Locations")]
    public List<GameObject> playerSpawnPoints;
    public List<Transform> enemySpawnPoints;
    [SerializeField] PlayerSpawn playerSpawn;

    [Header("Powerup Spots")]
    public List<GameObject> powerupSpots;

    [Header("UI")]
    public UIManager healthTracker;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update()
    {
        if (currentPlayer == null && lives > 0) PlayerSpawn(RandomSpawn(playerSpawnPoints));
    }

    public void PlayerSpawn(GameObject spawnPoint) // Spawns Player at the given spawn point if there is no active Player in the scene
    {
        if (!currentPlayer) currentPlayer = Instantiate(player, spawnPoint.transform.position, Quaternion.identity);
    }

    public GameObject RandomSpawn(List<GameObject> spawnPoints)
    {

        int spawnToGet = UnityEngine.Random.seed;
        spawnToGet = UnityEngine.Random.Range(0, spawnPoints.Count);
        return spawnPoints[spawnToGet];
    }
}