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
    public GameObject hunter; // enemy prefab
    public GameObject glider;
    public GameObject sniper;
    public GameObject rammer;
    public int activeEnemies;
    public int allowedEnemies;

    [Header("Spawn Locations")]
    public Vector3 playerSpawnPoint;
    public Transform[] enemySpawnPoints;

    [Header("Powerup Spots")]
    public GameObject[] powerupSpots;

    [Header("Patrol Paths")]
    public Transform[] firstWaypoints;
    public Transform[] secondWaypoints;
    public Transform[] thirdWaypoints;
    public Transform[] fourthWaypoints;
    
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
        GameObject cHunter;
        AIController hController;

        GameObject cSniper;
        AIController sController;

        GameObject cRammer;
        AIController rController;

        GameObject cGlider;
        AIController gController;

        cHunter = Instantiate(hunter, enemySpawnPoints[0]);
        hController = cHunter.GetComponent<AIController>();
        hController.currentWaypointList = 0;

        cSniper = Instantiate(sniper, enemySpawnPoints[1]);
        sController = cSniper.GetComponent<AIController>();
        sController.currentWaypointList = 1;

        cRammer = Instantiate(rammer, enemySpawnPoints[2]);
        rController = cRammer.GetComponent<AIController>();
        rController.currentWaypointList = 2;

        cGlider = Instantiate(glider, enemySpawnPoints[3]);
        gController = cGlider.GetComponent<AIController>();
        gController.currentWaypointList = 3;

        //enemy = Instantiate(aI, enemySpawnPoints[activeEnemies]);
        /*for (int i = activeEnemies; activeEnemies != allowedEnemies; i++)
        {
            enemy = Instantiate(hunter, enemySpawnPoints[i]);
            controller = enemy.GetComponent<AIController>();          
            switch (i)
            {
                case 0:
                    controller.currentWaypointList = 0;
                    break;
                case 1:
                    controller.currentWaypointList = 1;
                    break;
                case 2:
                    controller.currentWaypointList = 2;
                    break;
                case 3:
                    controller.currentWaypointList = 3;
                    break;
            }
            activeEnemies += 1;
        }*/
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