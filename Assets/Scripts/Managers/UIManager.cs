using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager instance;

    void Start()
    {
        instance = GameManager.Instance;
    }

    public void RecieveDamage(Health unitToDamage, float damageValue, bool isPlayer) // Make Health visual match the healthbar value
    {
        if (isPlayer)
        {
            unitToDamage.CurrentHealth -= damageValue;
            unitToDamage.health.value = unitToDamage.CurrentHealth;
        }
        else if (!isPlayer)
        {
            unitToDamage.CurrentHealth -= damageValue;
        }
    }

    public void HealDamage(Health unitToHeal, float healValue, bool isPlayer)
    {
        if (isPlayer)
        {
            unitToHeal.CurrentHealth += healValue;
            unitToHeal.health.value = unitToHeal.CurrentHealth;
        }
        else if (!isPlayer) unitToHeal.CurrentHealth += healValue;
    }
}