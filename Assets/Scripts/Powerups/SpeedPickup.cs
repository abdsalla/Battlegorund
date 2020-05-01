using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{

    void Start()
    {
        powerUpType = PowerUpType.Speed;
        isPermanent = false;
    }

    public override void OnPickup(GameObject target)
    {
        AIController aiCheck = target.GetComponent<AIController>();

        if (receiverPawn != null && aiCheck == null)
        {
            receiverPawn.moveSpeed += 3f;
            receiverPawn.reverseSpeed += 3f;
            receiverPawn.AddEffect(GetComponent<Pickup>(), duration);
            Destroy(gameObject);
        }
    }
}