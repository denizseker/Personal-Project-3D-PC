using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public enum State
    {
        Idle,
        Chasing,
        Patroling,
        Fleeing,
        InInteraction,
        Free,
        Defeated,
        InSettlement,
        Recruiting,
        GoingToWar,
        InWar,
        Following,
    }

    //Soldiers current state.
    [HideInInspector] public State currentState;
    [HideInInspector] public State oldState;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Army army;
    [HideInInspector] public float speed;
    [HideInInspector] public GameObject town;
    public Character interactedCharacter;
    public string characterName;
    
    //Soldier values
    public ClanManager.ENUM_Clan enumClan; //enum clan for dropdown list at inspector.
    [HideInInspector] public Clan clan; //real clan value

    public void Setup()
    {
        army = GetComponent<Army>();
        agent = GetComponent<NavMeshAgent>();
        GetClanWithEnum();
        gameObject.name = (string.Format("[{0}] [{1}]", clan.clanName, characterName));
        clan.AddMember(gameObject);
        speed = agent.speed;
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
        else if (enumClan == ClanManager.ENUM_Clan.NONE) clan = ClanManager.Instance.None;
    }

    //Setting colliders to opposite
    public void ChangeColliderState()
    {
        gameObject.GetComponentInChildren<DetectArea>().OnOffCollider();
        gameObject.GetComponentInChildren<InteractArea>().OnOffCollider();
        gameObject.GetComponentInChildren<MouseInteraction>().OnOffCollider();
    }

    public void EnterSettlement()
    {
        SetCharacterState(State.InSettlement);
        OnOffCharacterComponentForTown(false);
    }

    public void SetCharacterState(State _State)
    {
        oldState = currentState;
        currentState = _State;
    }


    public void SendCharacterStateToInteractedCharacter()
    {
        if(interactedCharacter.interactedCharacter == this)
        {
            Debug.Log("YES");
        }
    }

    public bool IsCharacterState(params State[] state)
    {
        bool isMatch = false;

        foreach (State CurrentState in state)
        {
            if(CurrentState == currentState)
            {
                isMatch = true;
                return isMatch;
            }
        }
        return isMatch;
    }

    public void LeaveSettlement()
    {
        OnOffCharacterComponentForTown(true);
        town.GetComponent<Settlement>().RemoveCharacter(gameObject);
        town = null;
        SetCharacterState(State.Patroling);
    }

    public void ResetTarget()
    {
        interactedCharacter = null;
    }

    //This function make ghost that character but important scripts (NPCAI,NPC,PLAYER) is still working while ghost.
    public void OnOffCharacterComponentForTown(bool isActive)
    {
        if (!isActive)
        {
            gameObject.GetComponentInChildren<MouseInteraction>().OnOffCollider();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            gameObject.GetComponentInChildren<MouseInteraction>().OnOffCollider();
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

}