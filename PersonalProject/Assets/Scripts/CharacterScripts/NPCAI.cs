using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCAI : MonoBehaviour
{

    private NPC NPC;
    public NavMeshData data;
    private GameObject targetDestination;
    [SerializeField] private GameObject warHappening;

    private void Awake()
    {
        NPC = GetComponentInParent<NPC>();
    }

    private void Chase(Character _targetCharacter)
    {
        NPC.agent.SetDestination(_targetCharacter.transform.position);
        float distance = Vector3.Distance(transform.position, _targetCharacter.transform.position);
        if (distance < 7)
        {
            Catch(_targetCharacter);
        }
    }
    public void StopFleeingAndChasing()
    {
        NPC.interactedCharacter = null;
        NPC.agent.ResetPath();
        NPC.currentState = Character.CurrentState.Patroling;
    }

    public void Catch(Character _targetCharacter)
    {
        //Setting targets
        NPC.interactedCharacter = _targetCharacter;
        _targetCharacter.interactedCharacter = NPC;
        //Stopping agents
        NPC.agent.isStopped = true;
        _targetCharacter.agent.isStopped = true;
        //Resetting agents path
        _targetCharacter.agent.ResetPath();
        NPC.agent.ResetPath();
        //Setting characters states
        NPC.currentState = Character.CurrentState.InInteraction;
        _targetCharacter.currentState = Character.CurrentState.InInteraction;
        //setting off characters colliders. so warhappening object will handle collisions.
        NPC.ChangeColliderState();
        _targetCharacter.ChangeColliderState();
        //instantiating warhappening object at middle of 2 characters
        Vector3 middleOfCharacters = Vector3.Lerp(transform.position, _targetCharacter.transform.position, 0.75f);
        var warHappeningObj = Instantiate(warHappening, middleOfCharacters, transform.rotation);
        //Sending 2 character who is will be in fight.
        warHappeningObj.GetComponent<WarHandler>().StartFight(NPC,_targetCharacter);
    }

    private void RunFromEnemy(Character _targetCharacter)
    {
        Vector3 dirToTargetSoldier = transform.position - _targetCharacter.transform.position;
        Vector3 runPos = transform.position + dirToTargetSoldier;
        NPC.agent.SetDestination(runPos);
        //Running npc will check remaining distance/catch only if player chasing him. Otherwise always catcher will check this.
        float distance = Vector3.Distance(transform.position, _targetCharacter.transform.position);
        if (_targetCharacter.GetType() == typeof(Player))
        {
            if (distance < 7)
            {
                Catch(_targetCharacter);
            }
        }
            
    }
    private void OnTriggerEnter(Collider other)
    {
        //if character detect another character.
        if (other.tag == "DetectArea" && NPC.currentState != Character.CurrentState.InInteraction && NPC.currentState != Character.CurrentState.Defeated)
        {
            Character interactedCharacter = other.GetComponentInParent<Character>();
            //Targetcharacter is enemy.
            if (ClanManager.Instance.isEnemy(NPC.clan, interactedCharacter.clan) && interactedCharacter.currentState != Character.CurrentState.Defeated)
            {
                //this army is bigger then opponent army
                if(NPC.army.armyTotalTroops >= interactedCharacter.army.armyTotalTroops)
                {
                    //if this character not fleeing, it can chase.
                    if(NPC.currentState != Character.CurrentState.Fleeing)
                    {
                        NPC.currentState = Character.CurrentState.Chasing;
                    }
                    else
                    {
                        return;
                    }

                }
                //this army is smaller then opponent army
                else
                {
                    NPC.currentState = Character.CurrentState.Fleeing;
                }
                //setting this character's interactedcharacter. Both AI will do that for himself
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
        //if character detect war.
        if (other.tag == "War" && NPC.currentState != Character.CurrentState.InInteraction && NPC.currentState != Character.CurrentState.Defeated)
        {

            GameObject _warHandlerObj = other.transform.parent.gameObject;
            WarHandler _warHandler = other.transform.parent.GetComponent<WarHandler>();

            //if any of party have enemy clan
            if (_warHandler.CanJoinWar(NPC.clan))
            {
                GoToWarDestination(_warHandlerObj);
            }
            //if any of party dont have enemy clan
            else
            {
                Debug.Log("Cannot join");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if this exit any character area and have a interactedcharacter already and not in interaction with someone and not defeated
        if(other.tag == "DetectArea" && NPC.interactedCharacter != null && NPC.currentState != Character.CurrentState.InInteraction && NPC.currentState != Character.CurrentState.Defeated)
        {
            Character interactedCharacter = other.GetComponentInParent<Character>();

            //if interactedcharacter is interact with this too. (chase or fleeing) and interacted character is not defeated
            if (interactedCharacter.interactedCharacter == NPC && interactedCharacter.currentState != Character.CurrentState.Defeated)
            {
                StopFleeingAndChasing();
                interactedCharacter.GetComponentInChildren<NPCAI>().StopFleeingAndChasing();
            }
            //if interacted character not chasing this but this is fleeing.
            else if (interactedCharacter.interactedCharacter != NPC && NPC.currentState == Character.CurrentState.Fleeing)
            {
                StopFleeingAndChasing();
            }
        }
    }

    public void GoToRandomPoint()
    {
        int walkableIndex = 1 << NavMesh.GetAreaFromName("Walkable");

        // Generate a random point in a spherical area.
        Vector3 randomDirection = Random.insideUnitSphere * 900;

        // Ensure the Y position is at the same level as your characters.
        randomDirection += transform.position;

        // Use the NavMesh to find a valid point on the NavMesh.
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 900, walkableIndex))
        {
            // Use hit.position as your valid random NavMesh position.
            Vector3 randomNavMeshPosition = hit.position;
            NPC.agent.SetDestination(randomNavMeshPosition);
        }
        else
        {
            Debug.LogWarning("Could not find a valid NavMesh position within the specified distance.");
        }
    }

    public void FleeToTown()
    {
        if (!NPC.agent.hasPath)
        {
            GameObject patrolTown = NPC.town;
            Vector3 townPosition = patrolTown.transform.GetChild(5).transform.position;
            NPC.agent.destination = townPosition;
            NPC.currentState = Character.CurrentState.Defeated;
        } 
    }


    private void GoToWarDestination(GameObject _target)
    {
        if(_target != null)
        {
            targetDestination = _target;
            NPC.currentState = Character.CurrentState.GoingToWar;
            NPC.agent.SetDestination(targetDestination.transform.position);


            float distance = Vector3.Distance(transform.position, targetDestination.transform.position);
            if (distance < 7)
            {
                NPC.agent.ResetPath();
                targetDestination = null;
                NPC.currentState = Character.CurrentState.InInteraction;
                JoinWar(_target);
            }
        }
        else
        {
            NPC.currentState = Character.CurrentState.Patroling;
        }
        
    }

    public void JoinWar(GameObject _target)
    {
        WarHandler warHandler = _target.GetComponent<WarHandler>();
        NPC.agent.ResetPath();
        NPC.ChangeColliderState();
        warHandler.AddCharacterToWar(NPC);
    }

    public void GoPatrolTown()
    {
        if (!NPC.agent.hasPath)
        {
            GameObject patrolTown = NPC.town;
            Vector3 patrolPoint = patrolTown.GetComponentInChildren<GetPatrolPoint>().GetPatrolPostition();
            NPC.agent.destination = patrolPoint;
            NPC.GetPatrolPositionForDrawing(patrolPoint,true);
            NPC.currentState = Character.CurrentState.Patroling;
        }
    }

    private void LeaveSettlement()
    {
        NPC.ChangeCharacterVisibility();
        NPC.town.GetComponent<Settlement>().RemoveCharacter(NPC.gameObject);
        NPC.currentState = Character.CurrentState.Patroling;
    }


    private IEnumerator RecruitArmy()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            //Decrasing town manpower while recruiting soldier with army script
            Settlement settlement = NPC.town.GetComponent<Settlement>();
            //Trying to recruit soldier if settlement have manpower
            settlement.DecreaseManPower(NPC.army.RecruitSoldier(settlement.manPower));
            //if npc recruit enough troops
            if (NPC.army.armyTotalTroops >= NPC.army.MinArmySize)
            {
                LeaveSettlement();
                break;
            }
        }
    }

    private void Update()
    {
        //AI Logic
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
        else if (NPC.currentState == Character.CurrentState.Defeated)
        {
            FleeToTown();
        }
        else if (NPC.currentState == Character.CurrentState.InSettlement)
        {
            if (NPC.army.armyTotalTroops < 10)
            {
                StartCoroutine(RecruitArmy());
                NPC.currentState = Character.CurrentState.Recruiting;
            }
        }
        else if (NPC.currentState == Character.CurrentState.GoingToWar)
        {
            GoToWarDestination(targetDestination);
        }
    }
}
