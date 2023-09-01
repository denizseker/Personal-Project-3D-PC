using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private NPCManager npcManager;
    private Army army;

    public GameObject targetSoldier;
    private Army targetSoldierArmy;
    private NPCManager targetSoldierNPCManager;
    private PlayerManager targetSoldierPlayerManager;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        npcManager = GetComponentInParent<NPCManager>();
        army = GetComponentInParent<Army>();
    }

    private void Chase(GameObject targetSoldier)
    {
        agent.SetDestination(targetSoldier.transform.position);
        float distance = Vector3.Distance(transform.position, targetSoldier.transform.position);
        if (distance < 5)
        {
            Catch(targetSoldier);
        }
    }
    private void StopEveryThing()
    {
        ClearTarget();
        agent.ResetPath();
        npcManager.GoPatrol();
        npcManager.currentState = NPCManager.CurrentState.Patroling;
    }

    public void Catch(GameObject targetSoldier)
    {
        Debug.Log("Catch");
        NavMeshAgent targetAgent = targetSoldier.GetComponent<NavMeshAgent>();

        targetAgent.ResetPath();
        agent.ResetPath();

        npcManager.currentState = NPCManager.CurrentState.InInteraction;
        if (targetSoldierNPCManager != null) targetSoldierNPCManager.currentState = NPCManager.CurrentState.InInteraction;
        if (targetSoldierPlayerManager != null) targetSoldierPlayerManager.currentState = NPCManager.CurrentState.InInteraction;
    }

    private void RunFromEnemy(GameObject targetSoldier)
    {
        Vector3 dirToTargetSoldier = transform.position - targetSoldier.transform.position;
        Vector3 runPos = transform.position + dirToTargetSoldier;
        agent.SetDestination(runPos);
        //Running npc will check remaining distance/catch only if player chasing him. Other wise always catcher will check this.
        if(targetSoldierPlayerManager != null)
        {
            float distance = Vector3.Distance(transform.position, targetSoldier.transform.position);
            if (distance < 5)
            {
                Catch(targetSoldier);
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //if soldiers detect an other soldier. But this soldier must be idling or patroling.
        if (other.tag == "DetectArea" && (npcManager.currentState == NPCManager.CurrentState.Idle || npcManager.currentState == NPCManager.CurrentState.Patroling))
        {

            Debug.Log("Saw someone");
            //setting target variables
            targetSoldier = other.transform.parent.gameObject;
            targetSoldierArmy = targetSoldier.GetComponent<Army>();
            if(targetSoldier.GetComponent<NPCManager>() != null) targetSoldierNPCManager = targetSoldier.GetComponent<NPCManager>();
            if(targetSoldier.GetComponent<PlayerManager>() != null) targetSoldierPlayerManager = targetSoldier.GetComponent<PlayerManager>();

            //if detected soldier is NPC and enemy
            if (targetSoldier.tag == "NPC" && ClanManager.Instance.isEnemy(npcManager.clan,targetSoldierNPCManager.clan))
            {
                Debug.Log("Its enemy NPC");
                if (targetSoldierArmy.armyTotalTroops <= army.armyTotalTroops)
                {
                    Debug.Log("Im chasing");
                    npcManager.currentState = NPCManager.CurrentState.Chasing;
                    //Chase(targetSoldier);
                }
                else if (targetSoldierArmy.armyTotalTroops > army.armyTotalTroops)
                {
                    Debug.Log("Im running");
                    npcManager.currentState = NPCManager.CurrentState.RunningFrom;
                    //RunFromEnemy(targetSoldier);
                }
                else 
                {
                    ClearTarget();
                }
            }
            //if detected soldier is PLAYER and enemy
            else if (targetSoldier.tag == "Player" && ClanManager.Instance.isEnemy(npcManager.clan, targetSoldierPlayerManager.clan))
            {
                //if player is weak
                if (targetSoldierArmy.armyTotalTroops <= army.armyTotalTroops)
                {
                    npcManager.currentState = NPCManager.CurrentState.Chasing;
                    targetSoldierPlayerManager.targetSoldier = transform.parent.gameObject;
                    //Chase(targetSoldier);
                }
                //if npc is weak
                else if (targetSoldierArmy.armyTotalTroops > army.armyTotalTroops)
                {
                    npcManager.currentState = NPCManager.CurrentState.RunningFrom;
                    targetSoldierPlayerManager.targetSoldier = transform.parent.gameObject;
                    //RunFromEnemy(targetSoldier);
                }
                else 
                {
                    ClearTarget();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if any soldier exit from detectarea this must be not at interaction.
        if (other.tag == "DetectArea" && npcManager.currentState != NPCManager.CurrentState.InInteraction)
        {
            Debug.Log("Exited");

            GameObject targetSoldier = other.transform.parent.gameObject;

            bool isNpc = false;

            if (targetSoldier.GetComponent<NPCManager>() != null)
            {
                Debug.Log("Its npc");
                isNpc = true;
            }

            //if npc and its not chasing this.
            if (isNpc && targetSoldier.GetComponentInChildren<NPCAI>().targetSoldier != transform.parent.gameObject)
            {
                StopEveryThing();
            }
            //if player
            else if (!isNpc && targetSoldier.GetComponentInChildren<PlayerManager>().targetSoldier != transform.parent.gameObject)
            {
                StopEveryThing();
            }
        }
    }

    private void ClearTarget()
    {
        Debug.Log("Cleared target");
        targetSoldier = null;
        targetSoldierArmy = null;
        targetSoldierNPCManager = null;
        targetSoldierPlayerManager = null;
    }

    private void Update()
    {
        if(npcManager.currentState == NPCManager.CurrentState.Patroling)
        {

        }
        else if (npcManager.currentState == NPCManager.CurrentState.RunningFrom)
        {
            RunFromEnemy(targetSoldier);
        }
        else if (npcManager.currentState == NPCManager.CurrentState.Chasing)
        {
            Chase(targetSoldier);
        }
    }

    
}
