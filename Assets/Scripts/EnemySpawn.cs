using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float secsTilSpawn;
    public float spawnTime;
    public GameObject activeEnemy;
    public GameObject enemyToSpawn;
    public Transform spawnPoint;
    public Transform[] patrolPath;

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
            secsTilSpawn -= Time.deltaTime;
            if (secsTilSpawn <= 0)
            {
                SpawnEnemy(spawnPoint);
                secsTilSpawn = spawnTime;
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