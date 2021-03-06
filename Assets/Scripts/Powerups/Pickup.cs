﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] Vector3 spinRotation;
    [SerializeField] protected Pawn receiverPawn;
    public MapGenerator generator;
    public Room spawnedAt;
    public int spotNum;
    public int pNum;
    public bool isPermanent;
    public float duration;

    public enum PowerUpType { Sheild, RapidFire, Speed }
    public PowerUpType powerUpType;


    public virtual void Update()
    {
        Spin();
    }

    public abstract void OnPickup(GameObject target);

    void Spin() { transform.Rotate(spinRotation * Time.deltaTime); }

    void OnTriggerEnter(Collider other)
    {
        receiverPawn = other.gameObject.GetComponent<Pawn>();
        OnPickup(other.gameObject);
        Debug.Log("Passed through pickup");
    }
}