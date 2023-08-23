using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChaseAndCatch : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyController enemyController;
    public bool isCatched = false;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        enemyController = GetComponentInParent<EnemyController>();
    }

    private void Chase(Collider other)
    {
        //if soldier running from someone, it cant chase anyone just can run.
        if(enemyController.currentState != EnemyController.CurrentState.RunningFrom)
        {
            enemyController.currentState = EnemyController.CurrentState.Catching;
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
        enemyController.intrectedSoldierName = "";
        enemyController.GoPatrol();
        enemyController.currentState = EnemyController.CurrentState.Patroling;
    }

    public void Catch(Collider other)
    {
        NavMeshAgent targetAgent = other.GetComponentInParent<NavMeshAgent>();
        agent.ResetPath();
        enemyController.currentState = EnemyController.CurrentState.Idle;
        isCatched = true;
        targetAgent.ResetPath();
    }

    private void RunFromEnemy(Collider other)
    {
        enemyController.currentState = EnemyController.CurrentState.RunningFrom;

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
            //if detected soldier is NPC
            if (other.transform.parent.tag == "NPC" && enemyController.clan != other.transform.parent.GetComponent<EnemyController>().clan)
            {
                enemyController.intrectedSoldierName = other.transform.parent.GetComponent<EnemyController>().soldierName;

                if (!isCatched && other.GetComponentInParent<EnemyController>().troops <= enemyController.troops)
                {
                    Chase(other);
                }
                else if (!isCatched && other.GetComponentInParent<EnemyController>().troops > enemyController.troops)
                {
                    RunFromEnemy(other);
                }
                else { return; }
            }
            //if detected soldier is PLAYER
            else if (other.transform.parent.tag == "Player")
            {
                enemyController.intrectedSoldierName = other.transform.parent.GetComponent<PlayerManager>().playerName;

                if (!isCatched && other.GetComponentInParent<PlayerManager>().troops <= enemyController.troops)
                {

                    Chase(other);
                }
                else if (!isCatched && other.GetComponentInParent<PlayerManager>().troops > enemyController.troops)
                {
                    RunFromEnemy(other);
                }
                else { return; }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.parent.tag == "Player" || other.transform.parent.tag == "NPC") StopChase(other);

    }
}
