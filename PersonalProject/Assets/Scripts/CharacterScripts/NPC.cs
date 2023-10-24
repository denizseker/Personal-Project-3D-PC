using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Character
{

    const string IDLE = "Idle";
    const string RUN = "Run";
    Animator animator;
    public Settlement patrolSettlement;
    //NPCAI using those.

    //[HideInInspector] public GameObject town;
    [HideInInspector] public Vector3 patrolPoint;
    [HideInInspector] public bool drawLineandPoint;
    private void Awake()
    {
        Setup();
        animator = GetComponentInChildren<Animator>();
        town = patrolSettlement.gameObject;
        SetCharacterState(State.Patroling);
    }


    private void Update()
    {
        SetAnimations();
    }

    void SetAnimations()
    {
        //if animator parent active
        if (animator.gameObject.activeSelf)
        {
            if (agent.hasPath)
            {
                animator.Play(RUN);
            }
            else
            {
                animator.Play(IDLE);
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        if (drawLineandPoint)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(patrolPoint, 2f);
            Gizmos.DrawLine(transform.position, patrolPoint);
        }
    }

    public void GetPatrolPositionForDrawing(Vector3 _patrolpoint,bool _isOpen)
    {
        patrolPoint = _patrolpoint;
        drawLineandPoint = _isOpen;
    }
}
