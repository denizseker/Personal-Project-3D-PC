using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour
{
    public enum CurrentState
    {
        Idle,
        Catching,
        Patroling,
        RunningFrom,
    }

    //Soldiers current state.
    [HideInInspector] public CurrentState currentState = CurrentState.Idle;

    const string IDLE = "Idle";
    const string RUN = "Run";
    Animator animator;
    NavMeshAgent agent;

    //Soldier values
    public ClanManager.ENUM_Clan enumClan; //enum clan for dropdown list at inspector.
    [HideInInspector] public Clan clan; //real clan value


    public string npcName;
    public int troops;

    //NPC AI
    private NPCAI Npc_AI;

    public Settlement settlement;
    private GameObject patrolTown;
    public string intrectedSoldierName;
    private Vector3 patrolPoint;
    private bool drawLineandPoint;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Npc_AI = GetComponentInChildren<NPCAI>();
        patrolTown = settlement.gameObject;
        GetClanWithEnum();
    }

    private void GetClanWithEnum() 
    {
        if (enumClan == ClanManager.ENUM_Clan.APHALUX) clan = ClanManager.Instance.Aphalux;
        else if (enumClan == ClanManager.ENUM_Clan.DARTRONG) clan = ClanManager.Instance.Dartrong;
        else if (enumClan == ClanManager.ENUM_Clan.SHUNEM) clan = ClanManager.Instance.Shunem;
        else if (enumClan == ClanManager.ENUM_Clan.SOLVENNA) clan = ClanManager.Instance.Solvenna;
        else if (enumClan == ClanManager.ENUM_Clan.VALANDOR) clan = ClanManager.Instance.Valandor;
        else if (enumClan == ClanManager.ENUM_Clan.WUTANG) clan = ClanManager.Instance.Wutang;
        else if (enumClan == ClanManager.ENUM_Clan.BARBARIAN) clan = ClanManager.Instance.Barbarian;
    }


    private void Update()
    {
        SetAnimations();
        //if AI have town for patroling.
        if(patrolTown != null)
        {
            GoPatrol();
        }
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

    public void GoPatrol()
    {
        if (!agent.hasPath && !Npc_AI.isCatched)
        {
            patrolPoint = patrolTown.GetComponentInChildren<GetPatrolPoint>().GetPatrolPostition();
            agent.destination = patrolPoint;
            drawLineandPoint = true;
            currentState = CurrentState.Patroling;
        }
    }



    private void OnDrawGizmos()
    {
        if (patrolPoint != null && drawLineandPoint)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(patrolPoint, 2f);
            Gizmos.DrawLine(transform.position, patrolPoint);
        }
        
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
