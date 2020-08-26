using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    [SerializeField] float damageDone;
    [SerializeField] float travelSpeed;
    [SerializeField] float lifespan = 1.5f;

    public int owner;

    private GameManager instance;
    private UIManager healthTracker;

    void Awake()
    {
        instance = GameManager.Instance;
        healthTracker = instance.healthTracker;
    }

    void Start() { Destroy(gameObject, lifespan); }

    void Update()
    {
        transform.position += transform.forward * travelSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Pawn otherPawn = other.GetComponent<Pawn>();
        Health otherHealth = other.GetComponent<Health>();

        if (otherHealth != null && otherPawn != null)
        {

            otherHealth.lastShooter = owner;
            healthTracker.RecieveDamage(otherHealth, damageDone, otherPawn.isPlayer);
        }
        Destroy(gameObject);
    }
}