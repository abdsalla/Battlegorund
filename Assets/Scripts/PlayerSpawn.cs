using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject spawnPoint;

    private GameManager instance;

    void Start()
    {
        instance = GameManager.Instance;
        SetSpawnPoint();

        if (instance.currentPlayerOne == null) SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        instance.PlayerSpawn(instance.RandomSpawn(instance.playerSpawnPoints));
    }

    public void SetSpawnPoint()
    {
        instance.playerSpawnPoints.Add(spawnPoint);
    }

    public void RemoveSpawnPoint()
    {
        instance.playerSpawnPoints.Remove(spawnPoint);
    }
}