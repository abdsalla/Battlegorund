using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFirePickup : Pickup
{
    private IEnumerator spawner;

    void Start()
    {
        powerUpType = PowerUpType.RapidFire;
        isPermanent = false;
    }

    public override void OnPickup(GameObject target)
    {
        AIController aiCheck = target.GetComponent<AIController>();

        if (receiverPawn != null)
        {
            receiverPawn.fireRate += .3f;
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