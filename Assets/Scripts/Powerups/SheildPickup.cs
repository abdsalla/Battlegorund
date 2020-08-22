using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildPickup : Pickup
{
    public float sheildCharge = 25;

    [SerializeField] private GameManager instance;

    private IEnumerator spawner;

    void Start()
    {
        instance = GameManager.Instance;
        powerUpType = PowerUpType.Sheild;
        isPermanent = true;
    }

    public override void OnPickup(GameObject target)
    {
        Health unitCheck = target.GetComponent<Health>();

        if (receiverPawn != null)
        {
            Debug.Log("Pawn exists");
            unitCheck.CurrentSheild += sheildCharge;
            instance.healthTracker.UpdateSheild(unitCheck, receiverPawn, sheildCharge);
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