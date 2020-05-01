using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildPickup : Pickup
{
    public float sheildCharge;

    void Start()
    {
        powerUpType = PowerUpType.Sheild;
        isPermanent = true;
    }

    public override void OnPickup(GameObject target)
    {
        AIController aiCheck = target.GetComponent<AIController>();
        Health unitCheck = target.GetComponent<Health>();

        if (receiverPawn != null && aiCheck == null)
        {
            unitCheck._sheildAmount += sheildCharge;
            receiverPawn.AddEffect(GetComponent<Pickup>(), duration);
            Destroy(gameObject);
        }
    }
}