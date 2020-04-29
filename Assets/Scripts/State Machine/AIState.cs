using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    public Transform target;

    public enum AvoidanceStage { None, Rotate, Move };
    public enum Personalities { Hunter, Glider, Sniper, Rammer };
    public enum State { Patrol, Chase, Attack, Retreat };
    public AvoidanceStage avoidanceStage;
    public Personalities personality;
    public State currentState;

    protected Pawn pawn => GetComponent<Pawn>();

    public abstract void Behavior();
}