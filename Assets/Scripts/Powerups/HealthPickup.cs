using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public GameObject healthPack; // health Item

    private GameManager instance;

    void Start()
    {
        instance = GameManager.Instance;
    }

    public override void OnPickup(GameObject target)
    {
        Health healthToAffect = target.GetComponent<Health>();
        AIController aiCheck = target.GetComponent<AIController>();

        if (receiverPawn != null)
        {
            if (healthToAffect.CurrentHealth < healthToAffect.maxHealth && aiCheck != null) // if the unit isn't at full health
            {
                instance.healthTracker.HealDamage(healthToAffect, 25, false);
            }
            else if (healthToAffect.CurrentHealth < healthToAffect.maxHealth && aiCheck == null) // if the unit isn't at full health
            {
                instance.healthTracker.HealDamage(healthToAffect, 25, true);
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