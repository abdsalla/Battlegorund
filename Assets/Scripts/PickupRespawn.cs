using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRespawn : MonoBehaviour
{
    [SerializeField] GameObject pickup;
    [SerializeField] GameObject activePickup;
    [SerializeField] float secsTilSpawn;
    [SerializeField] float timeLeft;

    private GameManager instance;


    void Start()
    {
        activePickup = Instantiate(pickup, transform.position, Quaternion.identity);
        instance = GameManager.Instance;
        instance.powerupSpots.Add(activePickup);
    }

    void Update()
    {
        if (pickup == null)
        {
            instance.powerupSpots.Remove(activePickup);
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                activePickup = Instantiate(pickup, transform.position, Quaternion.identity);
                instance.powerupSpots.Add(activePickup);
                timeLeft = secsTilSpawn;
            }
        }
    }
}