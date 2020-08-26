using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pawn))]

public class TankData : MonoBehaviour
{
    private Pawn pawn;

    public int playerNum;
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

    void InputConvertMove() // Forward and Back
    {
        if (playerNum == 1)
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
        else if (playerNum == 2)
        {
            string moveDir;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                moveDir = "Accelerate";
                pawn.Move(moveDir);
                moveDir = null;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                moveDir = "Reverse";
                pawn.Move(moveDir);
                moveDir = null;
            }
        }
    }

    void InputConvertRotate() // Turning
    {
        if (playerNum == 1)
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
        else if (playerNum == 2)
        {
            string rotateDir;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotateDir = "CounterClockwise";
                pawn.Turn(rotateDir);
                rotateDir = null;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rotateDir = "Clockwise";
                pawn.Turn(rotateDir);
                rotateDir = null;
            }
        }
    }

    void Fire()
    {
        if (playerNum == 1)
        {
            if (Input.GetButtonDown("Fire"))
            {
                pawn.owner = 1;
                pawn.Shoot();
            }
        }
        else if (playerNum == 2)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                pawn.owner = 2;
                pawn.Shoot();
            }
        }
    }
}