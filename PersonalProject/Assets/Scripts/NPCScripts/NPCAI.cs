using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private NPCManager npcManager;
    public bool isCatched = false;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        npcManager = GetComponentInParent<NPCManager>();
    }

    private void Chase(Collider other)
    {
        //if soldier running from someone, it cant chase anyone just can run.
        if(npcManager.currentState != NPCManager.CurrentState.RunningFrom)
        {
            npcManager.currentState = NPCManager.CurrentState.Catching;
            agent.SetDestination(other.transform.position);
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance < 5)
            {
                Catch(other);
            }
        }
        
    }
    private void StopChase(Collider other)
    {
        isCatched = false;
        agent.ResetPath();
        npcManager.intrectedSoldierName = "";
        npcManager.GoPatrol();
        npcManager.currentState = NPCManager.CurrentState.Patroling;
    }

    public void Catch(Collider other)
    {
        NavMeshAgent targetAgent = other.GetComponentInParent<NavMeshAgent>();
        agent.ResetPath();
        npcManager.currentState = NPCManager.CurrentState.Idle;
        isCatched = true;
        targetAgent.ResetPath();
    }

    private void RunFromEnemy(Collider other)
    {
        npcManager.currentState = NPCManager.CurrentState.RunningFrom;

        Vector3 dirToPlayer = transform.position - other.transform.position;
        Vector3 runPos = transform.position + dirToPlayer;
        agent.SetDestination(runPos);
        float distance = Vector3.Distance(transform.position, other.transform.position);
        if (distance < 5)
        {
            Catch(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if soldiers detect an other soldier.
        if (other.tag == "DetectArea")
        {
            NPCManager targetSoldier = other.transform.parent.GetComponent<NPCManager>();


            //if detected soldier is NPC
            if (other.transform.parent.tag == "NPC" && npcManager.clan != targetSoldier.clan)
            {
                npcManager.intrectedSoldierName = targetSoldier.npcName;

                if (!isCatched && targetSoldier.troops <= npcManager.troops)
                {
                    Chase(other);
                }
                else if (!isCatched && targetSoldier.troops > npcManager.troops)
                {
                    RunFromEnemy(other);
                }
                else { return; }
            }
            //if detected soldier is PLAYER
            else if (other.transform.parent.tag == "Player")
            {
                PlayerManager Player = other.transform.parent.GetComponent<PlayerManager>();

                npcManager.intrectedSoldierName = Player.playerName;

                if (!isCatched && Player.troops <= npcManager.troops)
                {

                    Chase(other);
                }
                else if (!isCatched && Player.troops > npcManager.troops)
                {
                    RunFromEnemy(other);
                }
                else { return; }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if((other.transform.parent.tag == "Player" || other.transform.parent.tag == "NPC" && npcManager.clan != other.transform.parent.GetComponent<NPCManager>().clan)) StopChase(other);

    }
}
