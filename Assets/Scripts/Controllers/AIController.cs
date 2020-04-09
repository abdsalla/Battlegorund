using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pawn))]

public class AIController : MonoBehaviour
{
    private Pawn pawn;

    public Health health;

    void Start()
    {
        pawn = GetComponent<Pawn>();
        health = pawn.GetComponent<Health>();
        pawn.isPlayer = false;
    }

    void Update()
    {
        pawn.Shoot();
    }
}