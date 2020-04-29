using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pawn))]

public class AIController : MonoBehaviour
{
    [SerializeField] FiniteStateMachine fsm;
    private GameManager instance;
    private float exitTime = 0;
    private Pawn pawn;

    public int currentWaypointList;
    public int currentWaypoint;
    public float aggroRadius = 5.0f;
    public float firingRange = 3.0f;
    public float stoppingDist = .3f;
    public float avoidanceTime = 2.0f;
    public Health health;


    void Start()
    {
        instance = GameManager.Instance;
        pawn = GetComponent<Pawn>();
        health = pawn.GetComponent<Health>();
        fsm.avoidanceStage = AIState.AvoidanceStage.None;
        pawn.isPlayer = false;
    }

    void Update()
    {
        Debug.Log("Enemy CurrentHealth: " + health.CurrentHealth);
        Debug.Log("AvoidanceStage: " + CanMove());
    }

    bool CanMove() // Is there an obstacle blocking our tank's path?
    {
        RaycastHit hit;
        Health unitCheck;
        GameObject obstacle;

        if (fsm.currentState == AIState.State.Retreat) return true;

        if (Physics.Raycast(transform.position, transform.forward, out hit, aggroRadius)) // Object in front
        {
            unitCheck = hit.collider.gameObject.GetComponent<Health>();
            obstacle = hit.collider.gameObject;

            if (unitCheck == null && obstacle.tag == "Obstacle")
            {
                fsm.avoidanceStage = AIState.AvoidanceStage.Rotate;
                return false;
            } // Avoid
        }
        fsm.avoidanceStage = AIState.AvoidanceStage.None;
        return true; // Is a tank proceed
    }

    public void AvoidObstacle()
    {
        if (fsm.currentState == AIState.State.Retreat) return;

        if (fsm.avoidanceStage == AIState.AvoidanceStage.Rotate)
        {
            Debug.Log("Starting Avoidance");
            pawn.Rotate(pawn.rotateSpeed);

            if (CanMove())
            {
                fsm.avoidanceStage = AIState.AvoidanceStage.Move;
                exitTime = avoidanceTime;
            }
            else if (!CanMove())
            {
                Debug.Log("Avoiding");
                pawn.Rotate(pawn.rotateSpeed);
                pawn.MoveForward();
            }
        }

        if (fsm.avoidanceStage == AIState.AvoidanceStage.Move)
        {
            exitTime -= Time.deltaTime;
            // proceed
            if (exitTime <= 0.0f) fsm.avoidanceStage = AIState.AvoidanceStage.None;
            else
            {
                Debug.Log("Continuing Avoidance");
                fsm.avoidanceStage = AIState.AvoidanceStage.Rotate;
            }
        } 
    }

    public void Patrol()
    {
        Transform[] listInUse; // List of patrol paths

        switch (currentWaypointList) // assigns path based on list number
        {
            case 0:
                listInUse = instance.firstWaypoints;
                fsm.target = listInUse[currentWaypoint];
                pawn.MoveTo(listInUse[currentWaypoint]);
                pawn.RotateTo(listInUse[currentWaypoint]);
                PointTransition(listInUse);
                break;
            case 1:
                listInUse = instance.secondWaypoints;
                fsm.target = listInUse[currentWaypoint];
                pawn.MoveTo(listInUse[currentWaypoint]);
                pawn.RotateTo(listInUse[currentWaypoint]);
                PointTransition(listInUse);
                break;
            case 2:
                listInUse = instance.thirdWaypoints;
                fsm.target = listInUse[currentWaypoint];
                pawn.MoveTo(listInUse[currentWaypoint]);
                pawn.RotateTo(listInUse[currentWaypoint]);
                PointTransition(listInUse);
                break;
            case 3:
                listInUse = instance.fourthWaypoints;
                fsm.target = listInUse[currentWaypoint];
                pawn.MoveTo(listInUse[currentWaypoint]);
                pawn.RotateTo(listInUse[currentWaypoint]);
                PointTransition(listInUse);
                break;
        }
    }

    void PointTransition(Transform[] currentList) // If we are close to the waypoint,
    {
        if (Vector3.SqrMagnitude(currentList[currentWaypoint].position - transform.position) < stoppingDist)
        {
            if (currentWaypoint < currentList.Length - 1) currentWaypoint++;
            else currentWaypoint = 0;
        }
    }

    public bool RotateTowards(Transform toFace)
    {
        Vector3 vectorToTarget;
        vectorToTarget = toFace.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, pawn.rotateSpeed * Time.deltaTime);

        if (targetRotation == transform.rotation) return false;

        return true;
    }

    public void FindHeal()
    {
        Debug.Log("Finding Heal");
        fsm.target = null;
        if (fsm.target == null && instance.powerupSpots != null) fsm.target = instance.powerupSpots[Random.Range(0, instance.powerupSpots.Length)].transform;
    }

    public void GetHeal()
    {       
        fsm.avoidanceStage = AIState.AvoidanceStage.None;
        pawn.MoveTo(fsm.target);
        pawn.RotateTo(fsm.target);
    }

    public void Attack()
    {
        pawn.RotateTo(fsm.target);

        if (CanMove())
        {
            fsm.avoidanceStage = AIState.AvoidanceStage.Move;
            if (fsm.personality != AIState.Personalities.Rammer)
            {
                pawn.MoveTo(fsm.target);
                pawn.Shoot();
            }
            else pawn.MoveTo(fsm.target);
        }    
        else fsm.avoidanceStage = AIState.AvoidanceStage.Rotate;
    }

    public void Chase()
    {
        if (CanMove())
        {
            fsm.avoidanceStage = AIState.AvoidanceStage.Move;
            pawn.RotateTo(fsm.target);
            pawn.MoveTo(fsm.target);
        }
        else fsm.avoidanceStage = AIState.AvoidanceStage.Rotate;
    }

    void OnCollisionStay(Collision collision)
    {
        TankData playerCheck = collision.gameObject.GetComponent<TankData>();
        Health otherHealth = collision.gameObject.GetComponent<Health>();

        if (fsm.personality == AIState.Personalities.Rammer && otherHealth != null && playerCheck != null) instance.healthTracker.RecieveDamage(otherHealth, 5, true);
        else if (fsm.personality == AIState.Personalities.Rammer && otherHealth != null && playerCheck == null) instance.healthTracker.RecieveDamage(otherHealth, 5, true);
    }
}