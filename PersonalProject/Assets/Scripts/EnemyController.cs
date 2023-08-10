using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum CurrentState
    {
        Idle,
        Catching,
        Patroling,
        RunningFromEnemy,
    }

    //Soldiers current state.
    public CurrentState currentState = CurrentState.Idle;

    //This is contain settlements patrol radius.
    [SerializeField] private GetPatrolPoint patrolArea;


    const string IDLE = "Idle";
    const string RUN = "Run";
    Animator animator;
    NavMeshAgent agent;

    //Soldier values
    public string clan;
    public string soldierName;
    public int troops;

    //chaseandcatch
    private ChaseAndCatch chaseAndCatch;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        chaseAndCatch = GetComponentInChildren<ChaseAndCatch>();
    }
    private void Update()
    {
        SetAnimations();
        GoPatrol();
        
    }

    void SetAnimations()
    {
        if (agent.hasPath)
        {
            animator.Play(RUN);
        }
        else
        {
            currentState = CurrentState.Idle;
            animator.Play(IDLE);
        }
    }

    //Enemy AI Soldier movement
    public void GoPatrol()
    {
        //If agent dont have path (patroling,catching,runningfromus) we are giving it patrol job.
        if (!agent.hasPath && !chaseAndCatch.isCatched)
        {
            Vector3 patrolPoint = patrolArea.GetPatrolPostition();
            agent.destination = patrolPoint;
            currentState = CurrentState.Patroling;
        }
    }


}
