using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildPickup : Pickup
{
    public float sheildCharge = 25;

    [SerializeField] GameManager instance;
    [SerializeField] UIManager ui;
    [SerializeField] Health unitCheck;

    private IEnumerator spawner;

    void Start()
    {
        instance = GameManager.Instance;
        ui = instance.healthTracker;
        powerUpType = PowerUpType.Sheild;
        isPermanent = true;
    }

    public override void OnPickup(GameObject target)
    {
        unitCheck = target.GetComponent<Health>();

        Debug.Log("Collided");

        if (receiverPawn)
        {
            Debug.Log("Has Pawn");
            unitCheck.CurrentSheild += sheildCharge;
            ui.UpdateSheild(unitCheck, receiverPawn, sheildCharge);
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