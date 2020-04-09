using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject aI; // enemy prefab
    public int activeEnemies = 0;
    public int allowedEnemies = 4;

    [Header("Spawn Locations")]
    public Vector3 playerSpawnPoint;
    public Transform[] enemySpawnPoints;


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
        PlayerSpawn();
        EnemySpawn();
    }

    void EnemySpawn() // Starting Enemy spawn only happens once
    {
        GameObject enemy;
        for (int i = activeEnemies; activeEnemies < allowedEnemies; i++)
        {
            enemy = Instantiate(aI, enemySpawnPoints[i]);
            activeEnemies += 1;
        }
    }

    void PlayerSpawn() // Spawns Player at the given spawn point if there is no active Player in the scene
    {
        if (!currentPlayer)
        {
            currentPlayer = Instantiate(player);
        }
        currentPlayer.transform.position = playerSpawnPoint;
    }
}