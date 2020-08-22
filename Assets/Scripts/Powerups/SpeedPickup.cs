using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{

    private IEnumerator spawner;

    void Start()
    {
        powerUpType = PowerUpType.Speed;
        isPermanent = false;
    }

    public override void OnPickup(GameObject target)
    {

        if (receiverPawn != null)
        {
            receiverPawn.moveSpeed += 3f;
            receiverPawn.reverseSpeed += 3f;
            generator.isActive = false;
            spawner = generator.Timer(10);
            generator.pickupNum = pNum;
            StartCoroutine(spawner);
            generator.ReplacePowerUp();
            generator.activePowerUps[spotNum] = generator.newPower;
            Destroy(gameObject);
        }
    }
}