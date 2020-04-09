using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pawn))]

public class TankData : MonoBehaviour
{
    private Pawn pawn;

    public Health health;

    void Start()
    {
        pawn = GetComponent<Pawn>();
        health = pawn.GetComponent<Health>();
        pawn.isPlayer = true;
    }

    void Update()
    {
        InputConvertMove();
        InputConvertRotate();
        Fire();
    }

    void InputConvertMove()
    {
        string moveDir;
        if (Input.GetKey(KeyCode.W))
        {
            moveDir = "Accelerate";
            pawn.Move(moveDir);
            moveDir = null;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDir = "Reverse";
            pawn.Move(moveDir);
            moveDir = null;
        }
    }

    void InputConvertRotate()
    {
        string rotateDir;
        if (Input.GetKey(KeyCode.A))
        {
            rotateDir = "CounterClockwise";
            pawn.Turn(rotateDir);
            rotateDir = null;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotateDir = "Clockwise";
            pawn.Turn(rotateDir);
            rotateDir = null;
        }
    }

    void Fire() { if (Input.GetButtonDown("Fire")) pawn.Shoot(); }
}