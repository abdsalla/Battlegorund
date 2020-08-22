using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawn : MonoBehaviour
{
    [Header("Health")]
    public float healSpawnTime;
    public float healSpawnTimer;
    public GameObject activeHealth;
    public GameObject health;
    public Transform hSpawnPoint;


    void Start()
    {
        healSpawnTimer = healSpawnTime;
    }
    
    void Update()
    {
        if (activeHealth == null)
        {
            healSpawnTimer -= Time.deltaTime;

            if (healSpawnTime <= 0)
            {
                SpawnHeal();
                healSpawnTimer = healSpawnTime;
            }
        }
    }

    void SpawnHeal()
    {
        activeHealth = health;
        activeHealth  = Instantiate(activeHealth, hSpawnPoint.position, Quaternion.identity) as GameObject;
    }
}