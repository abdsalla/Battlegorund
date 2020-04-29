using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Pawn))]

public class FiniteStateMachine : AIState
{
    private AIController ai;
    private Health health;
    private float dangerZone;

    void Start()
    {
        ai = GetComponent<AIController>();
        health = GetComponent<Health>();
    }

    void Update()
    {
       Behavior();
       Personality();
    }

    public override void Behavior()
    {
        switch (currentState)
        {
            case State.Patrol:
                if (avoidanceStage != AvoidanceStage.None) { ai.AvoidObstacle(); }
                else { ai.Patrol(); }

                if (CheckHealth() <= dangerZone)
                {
                    ai.FindHeal();
                    currentState = State.Retreat;
                }
                else if (IsInChaseRange()) currentState = State.Chase;
                break;
            case State.Chase:
                if (avoidanceStage != AvoidanceStage.None) { ai.AvoidObstacle(); }
                else { ai.Chase(); }

                if (CheckHealth() <= dangerZone)
                {
                    ai.FindHeal();
                    currentState = State.Retreat;
                }
                else if (IsInAttackRange()) currentState = State.Attack;
                break;
            case State.Attack:
                if (avoidanceStage != AvoidanceStage.None) { ai.AvoidObstacle(); }
                else { ai.Attack(); }

                if (CheckHealth() <= dangerZone)
                {
                    ai.FindHeal();
                    currentState = State.Retreat;
                }
                else if (target == null)
                {
                    ai.currentWaypoint = Random.Range(0, 3);
                    currentState = State.Patrol;
                }
                break;
            case State.Retreat:
                avoidanceStage = AvoidanceStage.None;              
                if (CheckHealth() > dangerZone) currentState = State.Patrol;
                ai.GetHeal();
                break;
        }
    }

    float CheckHealth()
    {
        float healthPercent = (health.CurrentHealth / health.maxHealth) * 100;
        dangerZone = health.maxHealth / 4;

        if (healthPercent <= dangerZone)
        {
            return healthPercent;
        }
        return healthPercent;
    }

    bool IsInChaseRange() // Is this obstacle another tank?
    {
        RaycastHit hit;
        Health unitCheck;
        GameObject objInRange;

        if (Physics.Raycast(pawn.firePoint.transform.position, transform.forward, out hit, ai.aggroRadius))
        {
            objInRange = hit.collider.gameObject;
            unitCheck = objInRange.GetComponent<Health>();

            if (unitCheck == null) return false; // No health component, not a tank
            else // It is a tank
            {
                // In Chase Range
                target = objInRange.transform;
                return true;
            }
        }
        return false; // Not within AggroRadius
    }

    bool IsInAttackRange()
    {
        RaycastHit hit;
        Health unitCheck;
        GameObject objInRange;


        if (Physics.Raycast(pawn.firePoint.transform.position, transform.forward, out hit, ai.firingRange))
        {
            objInRange = hit.collider.gameObject;
            unitCheck = objInRange.GetComponent<Health>();

            if (unitCheck == null) return false; // No health component, not a tank
            else // It is a tank
            {
                // In Attack Range
                target = objInRange.transform;
                return true;
            }
        }
        return false;
    }

    void Personality()
    {
        switch (personality)
        {
            case Personalities.Hunter:
                Hunter();
                break;
            case Personalities.Glider:
                Glider();
                break;
            case Personalities.Sniper:
                Sniper();
                break;
            case Personalities.Rammer:
                Collider();
                break;
        }
    }

    void Hunter()
    {
        pawn.fireRate = .25f;
    }

    void Glider()
    {
        pawn.moveSpeed = 10;
        pawn.rotateSpeed = 1200;
        pawn.reverseSpeed = 8;
    }

    void Sniper()
    {
        ai.aggroRadius = 8;
        ai.firingRange = 5;
        ai.stoppingDist = .7f;
    }

    void Collider()
    {
        pawn.shellPrefab = null;
        ai.stoppingDist = 0.0f;
    }
}