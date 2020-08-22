using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;

    private GameManager instance;

    void Start()
    {
        instance = GameManager.Instance;
        SetSpawnPoint();
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (instance.isTwoPlayer)
        {
            instance.PlayerSpawn(instance.RandomSpawn(instance.playerSpawnPoints));
            instance.TwoPlayerSpawn(instance.RandomSpawn(instance.playerSpawnPoints));
        }
        else instance.PlayerSpawn(instance.RandomSpawn(instance.playerSpawnPoints));
    }

    public void SetSpawnPoint()
    {
        instance.playerSpawnPoints.Add(spawnPoint1);
        instance.playerSpawnPoints.Add(spawnPoint2);
    }

    public void RemoveSpawnPoint()
    {
        instance.playerSpawnPoints.Remove(spawnPoint1);
    }
}