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
    public float _sheildAmount;

    [Header("UI Visuals")]
    public Slider health;
    public Slider sheild;

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
        _sheildAmount = 0;
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

        if (unitCheck != null)
        {
            if (unitCheck.isPlayer == true)
            {
                instance.lives -= 1;
                instance.score -= instance.pointsDeducted;
                Destroy(gameObject);
            }
            else if (unitCheck.isPlayer == false)
            {
                instance.score += instance.pointsAwarded;
                Destroy(gameObject, 2.0f);
            }
        }
    }
}