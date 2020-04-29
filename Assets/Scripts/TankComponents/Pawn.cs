using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [Header("Movement")]
    public float rotateSpeed;
    public float reverseSpeed;
    public float moveSpeed;

    [Header("Firing")]
    public GameObject shellPrefab;
    public Transform firePoint;
    public float fireRate = .5f;
    public float roundSpeed = 100f;
    [SerializeField] float shotCooldown;

    [Header("AI Values")]
    [SerializeField] AIController controller;

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

    public void MoveTo(Transform currentTarget)
    {      
        if (Vector3.Distance(transform.position, currentTarget.position) >= controller.stoppingDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, .095f);
        }
    }

    public void MoveForward()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    public void RotateTo(Transform currentTarget)
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, currentTarget.position, .02f, 8.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.LookAt(currentTarget);
    }

    public bool RotateTowards(Vector3 target, float speed)
    {
        Vector3 vectorToTarget = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);

        if (targetRotation == transform.rotation) return false;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
        return true;
    }

    public void Rotate(float speed)
    {
        Vector3 rotateVector;
        rotateVector = Vector3.up * speed * Time.deltaTime;
        transform.Rotate(rotateVector, Space.Self);
    }
}