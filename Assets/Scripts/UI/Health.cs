using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class UnityFloatEvent : UnityEvent<float> { }

public class Health : MonoBehaviour
{
    private GameManager instance;

    [Header("UI Values")]
    public float maxHealth;
    private float _currentHealth;

    [Header("UI Visuals")]
    public Image health;

    // UI Event for health, stamina and death
    public UnityFloatEvent OnHealthChange = new UnityFloatEvent();
    public UnityEvent OnDeath = new UnityEvent();

    public float CurrentHealth // Struct for Health values
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            OnHealthChange.Invoke(_currentHealth);
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            if (_currentHealth <= 0) // if tank health reaches zero it is destroyed
            {
                OnDeath.Invoke();
            }
        }
    }

    void Start()
    {
        instance = GameManager.Instance;
        _currentHealth = maxHealth;
    }

    public void DestroySelf() // Handles death and increments/decrements of player counters
    {
        Pawn unitCheck = GetComponent<Pawn>();

        if (unitCheck != null)
        {
            if (unitCheck.isPlayer == true)
            {
                instance.lives -= 1;
                instance.score -= instance.pointsDeducted;
                Destroy(gameObject);
                //if (instance.lives <= 0) instance.Loss();
               /* StartCoroutine(instance.PlayerRespawn());
                StopCoroutine(instance.PlayerRespawn());*/
            }
            else if (unitCheck.isPlayer == false)
            {
                instance.score += instance.pointsAwarded;
                Destroy(gameObject, 2.0f);
                //if (instance.score >= 150) { instance.Victory(); }

                /*for (int i = instance.activeEnemies; i >= instance.allowedEnemies; i--)
                {
                    instance.activeEnemies -= 1;
                    if (instance.activeEnemies < instance.allowedEnemies)
                    {
                        instance.EnemyRespawn();
                        instance.activeEnemies += 1;
                    }
                }*/
            }
        }
    }
}