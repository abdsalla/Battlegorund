using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{

    private GameManager instance;
    public GameObject heal;

    private IEnumerator spawner;

    void Start()
    {
        instance = GameManager.Instance;
        isPermanent = true;
    }

    public override void OnPickup(GameObject target)
    {
        Health healthToAffect = target.GetComponent<Health>();

        if (receiverPawn != null)
        {
            if (healthToAffect.CurrentHealth == healthToAffect.maxHealth) return;

            if (healthToAffect.CurrentHealth < healthToAffect.maxHealth) // if the unit isn't at full health
            {
                instance.healthTracker.HealDamage(healthToAffect, 25, false);
                generator.isActive = false;
                spawner = generator.Timer(10);
                generator.pickupNum = pNum;
                StartCoroutine(spawner);
                generator.ReplacePowerUp();
                generator.activePowerUps[spotNum] = generator.newPower;
            }
            Destroy(gameObject);
        }
    }

   /* public IEnumerator Heal()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(heal);
        yield return new WaitForSeconds(heal.length);
        Destroy(gameObject);
    }*/
}