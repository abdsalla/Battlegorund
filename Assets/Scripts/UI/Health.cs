using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class UnityFloatEvent : UnityEvent<float> { }

public class Health : MonoBehaviour
{
    private GameManager instance;
    private AIController ai;
    private FiniteStateMachine fsm;

    [Header("UI Values")]
    public float maxHealth;
    public float maxSheild;
    [SerializeField] float _currentHealth;   
    [SerializeField] float _currentSheild;

    [Header("UI Visuals")]
    public Slider health;
    public Slider sheild;

    // UI Event for health, stamina and death
    public UnityFloatEvent OnHealthChange = new UnityFloatEvent();
    public UnityFloatEvent OnSheildChange = new UnityFloatEvent();
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

    public float CurrentSheild // Struct for Sheild values
    {
        get { return _currentSheild; }
        set
        {
            _currentSheild = value;
            OnSheildChange.Invoke(_currentSheild);
            _currentSheild = Mathf.Clamp(_currentSheild, 0, maxSheild);
        }
    }

    void Start()
    {
        instance = GameManager.Instance;
        _currentHealth = maxHealth;
    }

    void Update()
    {
        ai = GetComponent<AIController>();
        fsm = GetComponent<FiniteStateMachine>();
        if (ai != null && CurrentHealth < 25)
        {
            Debug.Log("Health Below 25");
            fsm.currentState = AIState.State.Retreat;
        }

    }

    public void DestroySelf() // Handles death and increments/decrements of player counters
    {
        Pawn unitCheck = GetComponent<Pawn>();
        TankData playerCheck = GetComponent<TankData>();

        if (unitCheck != null)
        {
            if (unitCheck.isPlayer == true)
            {
                if (playerCheck.playerNum == 1)
                {
                    instance.lives1 -= 1;
                    instance.score1 -= instance.pointsDeducted;
                }
                else if (playerCheck.playerNum == 2)
                {
                    instance.lives2 -= 1;
                    instance.score2 -= instance.pointsDeducted;
                }
                Destroy(gameObject);
            }
            else if (unitCheck.isPlayer == false)
            {
                if (playerCheck.playerNum == 1)
                {
                    instance.score1 += instance.pointsAwarded;
                }
                else if (playerCheck.playerNum == 2)
                {
                    instance.score2 += instance.pointsAwarded;
                }
                Destroy(gameObject, 2.0f);
            }
        }
    }
}