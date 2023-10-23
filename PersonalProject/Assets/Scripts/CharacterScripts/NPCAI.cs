using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCAI : MonoBehaviour
{
    private float timer;
    private int interval = 10;
    private NPC NPC;
    public NavMeshData data;
    private GameObject targetDestination;
    [SerializeField] private GameObject warHappening;

    private void Awake()
    {
        NPC = GetComponent<NPC>();
    }

    private void Chase(Character _targetCharacter)
    {
        NPC.agent.SetDestination(_targetCharacter.transform.position);
    }
    public void StopAgent()
    {
        NPC.agent.velocity = Vector3.zero;
        NPC.agent.isStopped = true;
        NPC.agent.ResetPath();
    }
    public void StopAndReset()
    {
        NPC.interactedCharacter = null;
        NPC.agent.velocity = Vector3.zero;
        NPC.agent.isStopped = true;
        NPC.agent.ResetPath();
    }

    public void Catch(Character _targetCharacter)
    {
        Debug.Log("Catch");
        //Setting targets
        NPC.interactedCharacter = _targetCharacter;
        _targetCharacter.interactedCharacter = NPC;
        //Stopping agents
        //setting their velocity to zero for instant stop without sliding.
        NPC.agent.velocity = Vector3.zero;
        _targetCharacter.agent.velocity = Vector3.zero;
        NPC.agent.isStopped = true;
        _targetCharacter.agent.isStopped = true;
        //Resetting agents path
        _targetCharacter.agent.ResetPath();
        NPC.agent.ResetPath();
        //Setting characters states
        NPC.currentState = Character.CurrentState.InInteraction;
        _targetCharacter.currentState = Character.CurrentState.InInteraction;
        SpawnWarHandler(_targetCharacter);
    }
    public void SpawnWarHandler(Character _targetCharacter)
    {
        //setting off characters colliders. so warhappening object will handle collisions.
        NPC.ChangeColliderState();
        _targetCharacter.ChangeColliderState();
        //instantiating warhappening object at middle of 2 characters
        Vector3 middleOfCharacters = Vector3.Lerp(transform.position, _targetCharacter.transform.position, 0.5f);
        var warHappeningObj = Instantiate(warHappening, middleOfCharacters, transform.rotation);
        //Rotating character to warhappeningobj so they will look each other
        NPC.gameObject.transform.LookAt(warHappeningObj.transform);
        _targetCharacter.gameObject.transform.LookAt(warHappeningObj.transform);
        //Sending 2 character who is will be in fight.
        warHappeningObj.GetComponent<WarHandler>().StartFight(NPC, _targetCharacter);
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
            //if (distance < 7)
            //{
            //    Catch(_targetCharacter);
            //}
        }
            
    }
    //private void OnTriggerEnter(Collider other)
    //{

    //    //if character detect another character.
    //    if (other.tag == "DetectArea" && NPC.currentState != Character.CurrentState.InInteraction && NPC.currentState != Character.CurrentState.Defeated)
    //    {
    //        Character interactedCharacter = other.GetComponentInParent<Character>();
    //        //Targetcharacter is enemy.
    //        if (ClanManager.Instance.isEnemy(NPC.clan, interactedCharacter.clan) && interactedCharacter.currentState != Character.CurrentState.Defeated)
    //        {
    //            //this army is bigger then opponent army
    //            if(NPC.army.armyTotalTroops >= interactedCharacter.army.armyTotalTroops)
    //            {
    //                //if this character not fleeing, it can chase.
    //                if(NPC.currentState != Character.CurrentState.Fleeing)
    //                {
    //                    NPC.currentState = Character.CurrentState.Chasing;
    //                }
    //                else
    //                {
    //                    return;
    //                }

    //            }
    //            //this army is smaller then opponent army
    //            else
    //            {
    //                NPC.currentState = Character.CurrentState.Fleeing;
    //            }
    //            //setting this character's interactedcharacter. Both AI will do that for himself
    //            NPC.interactedCharacter = interactedCharacter;

    //            //if interactedcharacter is player AI should set our target.
    //            if(interactedCharacter.GetType() == typeof(Player))
    //            {
    //                interactedCharacter.interactedCharacter = interactedCharacter;
    //            }
    //        }
    //        //Targetcharacter is not enemy.
    //        else
    //        {
                
    //        }
    //    }
    //    //if character detect war.
    //    if (other.tag == "War" && NPC.currentState != Character.CurrentState.InInteraction && NPC.currentState != Character.CurrentState.Defeated)
    //    {

    //        GameObject _warHandlerObj = other.transform.parent.gameObject;
    //        WarHandler _warHandler = other.transform.parent.GetComponent<WarHandler>();

    //        //if any of party have enemy clan
    //        if (_warHandler.CanJoinWar(NPC.clan))
    //        {
    //            GoToWarDestination(_warHandlerObj);
    //        }
    //        //if any of party dont have enemy clan
    //        else
    //        {
    //            Debug.Log("Cannot join");
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    //if this exit any character area and have a interactedcharacter already and not in interaction with someone and not defeated
    //    if(other.tag == "DetectArea" && NPC.interactedCharacter != null && NPC.currentState != Character.CurrentState.InInteraction && NPC.currentState != Character.CurrentState.Defeated)
    //    {
    //        Character interactedCharacter = other.GetComponentInParent<Character>();

    //        //if interactedcharacter is interact with this too. (chase or fleeing) and interacted character is not defeated
    //        if (interactedCharacter.interactedCharacter == NPC && interactedCharacter.currentState != Character.CurrentState.Defeated)
    //        {
    //            StopFleeingAndChasing();
    //            interactedCharacter.GetComponent<NPCAI>().StopFleeingAndChasing();
    //        }
    //        //if interacted character not chasing this but this is fleeing.
    //        else if (interactedCharacter.interactedCharacter != NPC && NPC.currentState == Character.CurrentState.Fleeing)
    //        {
    //            StopFleeingAndChasing();
    //        }
    //    }
    //}

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
            Vector3 townPosition = patrolTown.GetComponentInChildren<GetCharacterInSettlement>().transform.position;
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

    private bool CheckCharacterType(Character _character)
    {
        if (_character.GetType() == typeof(Player))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void InteractAreaOnTriggerEnter(Collider other)
    {
        if(other.tag == "InteractArea")
        {
            Character _interactedCharacter = other.GetComponentInParent<Character>();
            bool isPlayer = CheckCharacterType(_interactedCharacter);

            //if characters are not defeated already.
            if(NPC.currentState != Character.CurrentState.Defeated || _interactedCharacter.currentState != Character.CurrentState.Defeated)
            {
                //Debug.Log("Not defeated");
                //If interactedcharacter/clickedcharacter same as this character when interact area triggered
                if (_interactedCharacter.interactedCharacter == NPC || (isPlayer && other.GetComponentInParent<Player>().clickedTarget == NPC.gameObject))
                {
                    //if interacted character is npc, chaser handle situation.
                    if (!isPlayer && NPC.currentState == Character.CurrentState.Chasing)
                    {
                        Catch(_interactedCharacter);
                    }
                    //interactedcharacter is player or this npc not chasing.
                    else
                    {
                        //NPC interact with player
                        if (isPlayer)
                        {
                            Player _player = other.GetComponentInParent<Player>();

                            StopAgent();
                            _player.StopAgent();
                            NPC.currentState = Character.CurrentState.InInteraction;
                            _player.currentState = Character.CurrentState.InInteraction;
                            InteractManager.Instance.TakeDataActivateInteractPanel(NPC.gameObject, _player.gameObject);
                            _player.ClearClickedTarget();
                            //Debug.Log("Interact");
                        }
                    }
                }
            }
        }
    }
    public void InteractAreaOnTriggerExit(Collider other)
    {
        if (other.tag == "InteractArea")
        {
            if(GetComponentInParent<Character>().interactedCharacter == NPC)
            {
                Debug.Log("Exit");
            }
        }
    }
    public void DetectAreaOnTriggerEnter(Collider other)
    {
        //if character detect another character.
        if (other.tag == "DetectArea" && NPC.currentState != Character.CurrentState.InInteraction && NPC.currentState != Character.CurrentState.Defeated)
        {
            
            Character interactedCharacter = other.GetComponentInParent<Character>();
            //Targetcharacter is enemy.
            if (ClanManager.Instance.isEnemy(NPC.clan, interactedCharacter.clan) && interactedCharacter.currentState != Character.CurrentState.Defeated)
            {
                //this army is bigger then opponent army
                if (NPC.army.armyTotalTroops >= interactedCharacter.army.armyTotalTroops)
                {
                    //if this character not fleeing, it can chase.
                    if (NPC.currentState != Character.CurrentState.Fleeing)
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

                //if interactedcharacter is player, AI should set our target.
                //if (interactedCharacter.GetType() == typeof(Player))
                //{
                //    interactedCharacter.interactedCharacter = NPC;
                //}
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
    public void DetectAreaOnTriggerExit(Collider other)
    {
        //if this exit any character area and have a interactedcharacter already and not in interaction with someone and not defeated
        if (other.tag == "DetectArea" && NPC.interactedCharacter != null && NPC.currentState != Character.CurrentState.InInteraction && NPC.currentState != Character.CurrentState.Defeated)
        {
            Character interactedCharacter = other.GetComponentInParent<Character>();

            //if interactedcharacter is interact with this too. (chase or fleeing) and interacted character is not defeated
            if (interactedCharacter.interactedCharacter == NPC && interactedCharacter.currentState != Character.CurrentState.Defeated)
            {
                StopAndReset();
                NPC.currentState = Character.CurrentState.Patroling;
            }
            //if interacted character not chasing this but this is fleeing.
            else if (interactedCharacter.interactedCharacter != NPC && NPC.currentState == Character.CurrentState.Fleeing)
            {
                StopAndReset();
                NPC.currentState = Character.CurrentState.Patroling;
            }

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
        NPC.OnOffCharacterComponentForTown(true);
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

    private void AILogic()
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

    private void Update()
    {
        //AI checking logic every x frame
        if (timer % interval == 0 && NPC.currentState != Character.CurrentState.InInteraction)
        {
            AILogic();
        }
        timer++;
    }
}
