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

    [HideInInspector] public GameObject patrolTown;
    [HideInInspector] public Vector3 patrolPoint;
    [HideInInspector] public bool drawLineandPoint;
    private void Awake()
    {
        Setup();
        animator = GetComponent<Animator>();
        patrolTown = patrolSettlement.gameObject;
        currentState = CurrentState.Patroling;
    }


    private void Update()
    {
        SetAnimations();

    }

    void SetAnimations()
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

    ////Enemy AI Soldier movement
    //public void GetPatrolTown()
    //{
    //    //If agent dont have path (patroling,catching,runningfromus) we are giving it patrol job.
    //    if (!agent.hasPath && !Npc_AI.isCatched)
    //    {
    //        //Checking all available town.
    //        for (int i = 0; i < GameManager.Instance.Settlements.Count; i++)
    //        {
    //            Debug.Log(gameObject.name + " looking for town.");

    //            //Looking for a town owned by ally clan.
    //            if (GameManager.Instance.Settlements[i].GetComponent<Settlement>().RullerClan == clan)
    //            {
    //                if (!GameManager.Instance.Settlements[i].GetComponent<Settlement>().isHavePatrol)
    //                {

    //                    patrolTown = GameManager.Instance.Settlements[i];
    //                    patrolTown.GetComponent<Settlement>().isHavePatrol = true;
    //                    Debug.Log(patrolTown.name + " patrol is " + gameObject.name);
    //                    return;
    //                }
    //                else
    //                {
    //                    //Debug.Log("Have town but someone patroling already.");
    //                }
    //            }
    //            else
    //            {
    //                //Debug.Log("Dont have clan");
    //            }
    //        }
    //    }
    //}


}
