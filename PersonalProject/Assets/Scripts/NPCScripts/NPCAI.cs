using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private NPCManager npcManager;
    private Army army;
    public bool isCatched = false;
    public bool isChasing = false;
    public bool isRunning = false;

    public GameObject targetSoldier;
    private Army targetSoldierArmy;
    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        npcManager = GetComponentInParent<NPCManager>();
        army = GetComponentInParent<Army>();
    }

    private void Chase(GameObject targetSoldier)
    {
        npcManager.currentState = NPCManager.CurrentState.Catching;
        agent.SetDestination(targetSoldier.transform.position);
        float distance = Vector3.Distance(transform.position, targetSoldier.transform.position);
        if (distance < 5)
        {
            Catch(targetSoldier);
        }
    }
    private void StopChase(GameObject targetSoldier)
    {
        agent.ResetPath();
        npcManager.intrectedSoldierName = "";
        npcManager.GoPatrol();
        npcManager.currentState = NPCManager.CurrentState.Patroling;
        isRunning = false;
        isChasing = false;
        isCatched = false;
    }

    public void Catch(GameObject targetSoldier)
    {
        Debug.Log("Catch");
        NPCAI targetAI = targetSoldier.GetComponent<NPCAI>();
        NavMeshAgent targetAgent = targetSoldier.GetComponent<NavMeshAgent>();

        targetAgent.ResetPath();
        agent.ResetPath();

        npcManager.currentState = NPCManager.CurrentState.Idle;
        isCatched = true;
        isChasing = false;
        isRunning = false;
    }

    private void RunFromEnemy(GameObject targetSoldier)
    {
        npcManager.currentState = NPCManager.CurrentState.RunningFrom;
        Vector3 dirToTargetSoldier = transform.position - targetSoldier.transform.position;
        Vector3 runPos = transform.position + dirToTargetSoldier;
        agent.SetDestination(runPos);
        float distance = Vector3.Distance(transform.position, targetSoldier.transform.position);
        if (distance < 5)
        {
            Debug.Log("RunCatch");
            Catch(targetSoldier);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //if soldiers detect an other soldier.
        if (other.tag == "DetectArea" && !isRunning && !isChasing && !isCatched)
        {
            //if detected soldier is NPC
            if (other.transform.parent.gameObject.tag == "NPC" && npcManager.clan != other.GetComponentInParent<NPCManager>().clan)
            {
                targetSoldier = other.transform.parent.gameObject;
                targetSoldierArmy = targetSoldier.GetComponent<Army>();
                npcManager.intrectedSoldierName = targetSoldier.GetComponent<NPCManager>().npcName;

                if (targetSoldierArmy.armyTotalTroops <= army.armyTotalTroops)
                {
                    isChasing = true;
                    //Chase(targetSoldier);
                }
                else if (targetSoldierArmy.armyTotalTroops > army.armyTotalTroops)
                {
                    isRunning = true;
                    //RunFromEnemy(targetSoldier);
                }
                else { return; }
            }
            //if detected soldier is PLAYER
            else if (other.transform.parent.gameObject.tag == "Player")
            {
                targetSoldier = other.transform.parent.gameObject;
                targetSoldierArmy = targetSoldier.GetComponent<Army>();
                npcManager.intrectedSoldierName = targetSoldier.GetComponent<PlayerManager>().playerName;

                if (!isCatched && targetSoldierArmy.armyTotalTroops <= army.armyTotalTroops)
                {
                    isChasing = true;
                    //Chase(targetSoldier);
                }
                else if (!isCatched && targetSoldierArmy.armyTotalTroops > army.armyTotalTroops)
                {
                    isRunning = true;
                    //RunFromEnemy(targetSoldier);
                }
                else { return; }
            }
        }
    }

    private void Update()
    {
        if (isRunning && !isCatched)
        {
            RunFromEnemy(targetSoldier);
        }
        else if (isChasing && !isCatched)
        {
            Chase(targetSoldier);
        }

        if (Input.GetKeyDown("v"))
        {
            Debug.Log(ClanManager.Instance.isEnemy(npcManager.clan,targetSoldier.GetComponent<NPCManager>().clan));
        }

    }

    //private void OnTriggerStay(Collider other)
    //{
    //    //if soldiers detect an other soldier.
    //    if (other.tag == "DetectArea")
    //    {
    //        GameObject targetSoldier = other.transform.parent.gameObject;
    //        Army targetSoldierArmy = targetSoldier.GetComponent<Army>();

    //        //if detected soldier is NPC
    //        if (targetSoldier.tag == "NPC" && npcManager.clan != targetSoldier.GetComponent<NPCManager>().clan)
    //        {
    //            npcManager.intrectedSoldierName = targetSoldier.GetComponent<NPCManager>().npcName;

    //            if (!isCatched && targetSoldierArmy.armyTotalTroops <= army.armyTotalTroops)
    //            {
    //                Chase(targetSoldier);
    //            }
    //            else if (!isCatched && targetSoldierArmy.armyTotalTroops > army.armyTotalTroops)
    //            {
    //                RunFromEnemy(targetSoldier);
    //            }
    //            else { return; }
    //        }
    //        //if detected soldier is PLAYER
    //        else if (targetSoldier.tag == "Player")
    //        {
    //            npcManager.intrectedSoldierName = targetSoldier.GetComponent<PlayerManager>().playerName;

    //            if (!isCatched && targetSoldierArmy.armyTotalTroops <= army.armyTotalTroops)
    //            {

    //                Chase(targetSoldier);
    //            }
    //            else if (!isCatched && targetSoldierArmy.armyTotalTroops > army.armyTotalTroops)
    //            {
    //                RunFromEnemy(targetSoldier);
    //            }
    //            else { return; }
    //        }
    //    }
    //}


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DetectArea")
        {
            //Debug.Log(other.GetComponent<NPCAI>().targetSoldier);

            if (other.GetComponent<NPCAI>().targetSoldier != transform.parent.gameObject)
            {
                StopChase(targetSoldier);
            }

            if(!isRunning && !isChasing && !isCatched)
            {
                //base soldier object
                GameObject targetSoldier = other.transform.parent.gameObject;

                if (targetSoldier.tag == "NPC")
                {
                    if (npcManager.clan != other.transform.parent.GetComponent<NPCManager>().clan)
                    {
                        StopChase(targetSoldier);
                    }
                }
                else if (targetSoldier.tag == "Player")
                {
                    if (npcManager.clan != other.transform.parent.GetComponent<PlayerManager>().clan)
                    {
                        StopChase(targetSoldier);
                    }
                }
            }
            
        }
    }
}
