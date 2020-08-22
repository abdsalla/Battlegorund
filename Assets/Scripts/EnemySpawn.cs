using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Enemies")]
    public float enemySpawnTime;
    public float enemySpawnTimer;
    public GameObject activeEnemy;
    public GameObject enemyToSpawn;
    public Transform spawnPoint;
    public Transform[] patrolPath;

    [Header("PowerUps")]
    public float powerUpSpawnTime;
    public float powerUpSpawnTimer;
    public GameObject activePowerUp;
    public GameObject rapidFire;
    public GameObject shield;
    public GameObject speed;
    

    private GameManager instance;

    void Start()
    {
        instance = GameManager.Instance;
        SpawnEnemy(spawnPoint);
    }

    void Update()
    {
        if (activeEnemy == null)
        {
            enemySpawnTimer -= Time.deltaTime;
            if (enemySpawnTimer <= 0)
            {
                SpawnEnemy(spawnPoint);
                enemySpawnTimer = enemySpawnTime;
            }
        }
    }

    public void SpawnEnemy(Transform location) // Grab enemy spawn location and create enemy
    {
        AIController aIController;

        activeEnemy = Instantiate(enemyToSpawn, location.position, Quaternion.identity) as GameObject;
        aIController = activeEnemy.GetComponent<AIController>();
        aIController.listInUse = patrolPath;
        instance.activeEnemies.Add(activeEnemy);
    }
     
    public void RemoveEnemy() // Remove enenmy from list
    {    
        instance.activeEnemies.Remove(activeEnemy); 
    }
}