using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFirePickup : Pickup
{

    void Start()
    {
        powerUpType = PowerUpType.RapidFire;
        isPermanent = false;
    }

    public override void OnPickup(GameObject target)
    {
        AIController aiCheck = target.GetComponent<AIController>();

        if (receiverPawn != null && aiCheck == null)
        {
            receiverPawn.fireRate += .3f;
            receiverPawn.AddEffect(GetComponent<Pickup>(), duration);
            Destroy(gameObject);
        }
    }
}