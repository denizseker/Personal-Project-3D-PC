using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCAI : MonoBehaviour
{

    private NPC NPC;


    private void Awake()
    {
        NPC = GetComponentInParent<NPC>();
    }

    private void Chase(Character _targetCharacter)
    {
        NPC.agent.SetDestination(_targetCharacter.transform.position);
        float distance = Vector3.Distance(transform.position, _targetCharacter.transform.position);
        if (distance < 5)
        {
            Catch(_targetCharacter);
        }
    }
    private void StopEveryThing()
    {
        ClearTarget();
        NPC.agent.ResetPath();
        NPC.currentState = Character.CurrentState.Patroling;
    }

    public void Catch(Character _targetCharacter)
    {
        _targetCharacter.agent.ResetPath();
        NPC.agent.ResetPath();

        NPC.currentState = Character.CurrentState.InInteraction;
        _targetCharacter.currentState = Character.CurrentState.InInteraction;
    }

    private void RunFromEnemy(Character _targetCharacter)
    {
        Vector3 dirToTargetSoldier = transform.position - _targetCharacter.transform.position;
        Vector3 runPos = transform.position + dirToTargetSoldier;
        NPC.agent.SetDestination(runPos);
        //Running npc will check remaining distance/catch only if player chasing him. Otherwise always catcher will check this.
        float distance = Vector3.Distance(transform.position, _targetCharacter.transform.position);
        if (distance < 5)
        {
            Catch(_targetCharacter);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //if character detect another character.
        if (other.tag == "DetectArea")
        {
            Character interactedCharacter = other.GetComponentInParent<Character>();
            //Targetcharacter is enemy.
            if (ClanManager.Instance.isEnemy(NPC.clan, interactedCharacter.clan))
            {
                //this army is bigger then opponent army
                if(NPC.army.armyTotalTroops >= interactedCharacter.army.armyTotalTroops)
                {
                    //if this character not fleeing, it can chase.
                    if(NPC.currentState != Character.CurrentState.Fleeing)
                    {
                        NPC.currentState = Character.CurrentState.Chasing;
                    }

                }
                //this army is smaller then opponent army
                else
                {
                    NPC.currentState = Character.CurrentState.Fleeing;
                }
                //setting this character's intractedcharacter. Both AI will do that for himself
                NPC.interactedCharacter = interactedCharacter;

                //if interactedcharacter is player AI should set our target.
                if(interactedCharacter.GetType() == typeof(Player))
                {
                    interactedCharacter.interactedCharacter = interactedCharacter;
                }
            }
            //Targetcharacter is not enemy.
            else
            {
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "DetectArea")
        {
            StopEveryThing();
        }
    }

    public void GoPatrolTown()
    {
        if (!NPC.agent.hasPath)
        {
            GameObject patrolTown = NPC.patrolTown;
            Vector3 patrolPoint = patrolTown.GetComponentInChildren<GetPatrolPoint>().GetPatrolPostition();
            NPC.agent.destination = patrolPoint;
            NPC.GetPatrolPositionForDrawing(patrolPoint,true);
            NPC.currentState = Character.CurrentState.Patroling;
        }
    }

    private void ClearTarget()
    {
        Debug.Log("Cleared target");
        NPC.interactedCharacter = null;
    }

    private void Update()
    {
        if (NPC.currentState == Character.CurrentState.Patroling)
        {
            GoPatrolTown();
        }
        else if (NPC.currentState == Character.CurrentState.Fleeing)
        {
            RunFromEnemy(NPC.interactedCharacter);
        }
        else if (NPC.currentState == Character.CurrentState.Chasing)
        {
            Chase(NPC.interactedCharacter);
        }
    }


}
