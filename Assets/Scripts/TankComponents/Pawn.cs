using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{  
    [Header("Movement")]
    [SerializeField] float rotateSpeed;
    [SerializeField] float reverseSpeed;
    [SerializeField] float moveSpeed;

    [Header("Firing")]
    [SerializeField] GameObject shellPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] private float fireRate = .5f;
    [SerializeField] private float roundSpeed = 100f;
    [SerializeField] private float shotCooldown;

    public bool isPlayer;


    void Start()
    {
        shotCooldown = 0;
    }

    void Update()
    {
        if (shotCooldown < fireRate) shotCooldown += Time.deltaTime;
    }

    public void Move(string mDir)
    {
        if (mDir == "Accelerate") { transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); }
        else if (mDir == "Reverse") { transform.Translate(Vector3.back * reverseSpeed * Time.deltaTime); }
    }

    public void Turn(string rDir)
    {
        if (rDir == "Clockwise") { transform.Rotate(0.0f, rotateSpeed, 0.0f); }
        else if (rDir == "CounterClockwise") { transform.Rotate(0.0f, -rotateSpeed, 0.0f); }
    }

    public void Shoot()
    {
        if (shotCooldown >= fireRate)
        {
            GameObject bullet = Instantiate(shellPrefab, firePoint.position, firePoint.rotation) as GameObject;
            BulletData bulletData = bullet.GetComponent<BulletData>();
            shotCooldown = 0;
        }
    }
}