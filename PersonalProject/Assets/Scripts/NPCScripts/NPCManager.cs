using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour
{
    public enum CurrentState
    {
        Idle,
        Chasing,
        Patroling,
        RunningFrom,
        InInteraction,
    }

    //Soldiers current state.
    [HideInInspector] public CurrentState currentState;

    const string IDLE = "Idle";
    const string RUN = "Run";
    Animator animator;
    NavMeshAgent agent;

    //Soldier values
    public ClanManager.ENUM_Clan enumClan; //enum clan for dropdown list at inspector.
    [HideInInspector] public Clan clan; //real clan value


    public string npcName;
    public Settlement patrolSettlement;
    [HideInInspector] public float speed = 10f;
    //NPCAI using those.
    [HideInInspector] public GameObject patrolTown;
    [HideInInspector] public Vector3 patrolPoint;
    [HideInInspector] public bool drawLineandPoint;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        patrolTown = patrolSettlement.gameObject;
        GetClanWithEnum();
        currentState = CurrentState.Patroling;
        gameObject.name = (string.Format("[{0}] [{1}]", clan.clanName, npcName));
        clan.AddMember(gameObject);
        agent.speed = speed;
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
