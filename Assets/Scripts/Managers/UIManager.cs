using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager instance;

    void Start()
    {
        instance = GameManager.Instance;
        SetManager();
    }

    public void RecieveDamage(Health unitToDamage, float damageValue, bool isPlayer) // Make Health visual match the healthbar value
    {
        if (isPlayer)
        {
            if (unitToDamage.CurrentSheild > 0)
            {
                unitToDamage.CurrentSheild -= damageValue;
                unitToDamage.sheild.value = unitToDamage.CurrentSheild;
            }
            else
            {
                unitToDamage.CurrentHealth -= damageValue;
                unitToDamage.health.value = unitToDamage.CurrentHealth;
            }   
        }
        else if (!isPlayer) unitToDamage.CurrentHealth -= damageValue;
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

    public void UpdateSheild(Health toUpdate, Pawn receiver, float currentSheild)
    {
        Debug.Log("Updating UI");
        toUpdate.CurrentSheild += currentSheild;
        toUpdate.sheild.value = toUpdate.CurrentSheild;
    }

    public void SetManager()
    {
        instance.healthTracker = this;
    }
}