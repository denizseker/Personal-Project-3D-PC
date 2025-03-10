using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Character : MonoBehaviour , IInteractable
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
        GoingToSettlement,
    }

    //Soldiers current state.
    [HideInInspector] public State currentState;
    [HideInInspector] public State oldState;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Army army;
    [HideInInspector] public float speed;
    [HideInInspector] public GameObject town;
    public List<Character> characterParty = new List<Character>();
    public Sprite charPrev;

    public Character interactedCharacter;
    public string characterName;
    [HideInInspector] public bool isVisible = true;
    
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
        characterParty.Add(this);
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

        //update settlement char prev panel if any character leave settlement while player in that settlement
        if (UIManager.Instance.UI_inSettlementPanel.isPanelActive)
        {
            //if in this settlement
            if (InteractManager.Instance.interactedSettlement == town)
            {
                UIManager.Instance.UI_inSettlementPanel.UpdateCharPrev();
            }
        }
        //town = null;
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
            //count-1 cause dont want to disable/enable selected.png
            for (int i = 0; i < gameObject.transform.childCount-1; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            //gameObject.transform.GetChild(0).gameObject.SetActive(false);
            //gameObject.transform.GetChild(1).gameObject.SetActive(false);
            //gameObject.transform.GetChild(2).gameObject.SetActive(false);
            //gameObject.transform.GetChild(3).gameObject.SetActive(false);
            //gameObject.transform.GetChild(4).gameObject.SetActive(false);
        }
        else
        {
            gameObject.GetComponentInChildren<MouseInteraction>().OnOffCollider();
            for (int i = 0; i < gameObject.transform.childCount-1; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
            //gameObject.transform.GetChild(0).gameObject.SetActive(true);
            //gameObject.transform.GetChild(1).gameObject.SetActive(true);
            //gameObject.transform.GetChild(2).gameObject.SetActive(true);
            //gameObject.transform.GetChild(3).gameObject.SetActive(true);
            //gameObject.transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    public void Click()
    {
        if (isVisible)
        {
            if (GetType() == typeof(Player))
            {
                Debug.Log("Player");
            }
            if (GetType() == typeof(NPC))
            {
                InteractManager.Instance.SelectObject(gameObject);
                InteractManager.Instance.player.GetComponent<PlayerController>().MoveToTarget(gameObject);
            }
        }
        
    }

    public void MouseEnter()
    {
        if (isVisible)
        {
            UIManager.Instance.ActivateCharacterInfoPanel(this);
        }
    }

    public void MouseOver()
    {
        if (isVisible)
        {
            if (UIManager.Instance.UI_characterInfoPanel.isPanelActive) UIManager.Instance.UI_characterInfoPanel.UpdatePanel(this);
        }    
    }

    public void MouseExit()
    {
        UIManager.Instance.DeActivateCharacterInfoPanel();
    }
}