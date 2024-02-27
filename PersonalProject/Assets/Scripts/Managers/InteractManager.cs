using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager Instance;

    public GameObject interactedSettlement;
    public GameObject interactedCharacter;
    public GameObject player;

    public List<GameObject> selectedObjects = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        Instance.player = GameObject.FindWithTag("Player");
    }

    public void SelectObject(GameObject _selectedObject)
    {
        ClearSelectedObjects();
        Instance.selectedObjects.Add(_selectedObject);
        Instance.player.GetComponent<PlayerController>().clickedTarget = _selectedObject;
        _selectedObject.GetComponent<MouseInteraction>().ringEffect.SetActive(true);
        _selectedObject.GetComponent<MouseInteraction>().isSelected = true;
    }

    public void ClearSelectedObjects()
    {
        //If we have a clicked object already
        if (Instance.selectedObjects.Count > 0)
        {
            player.GetComponent<PlayerController>().clickedTarget = null;
            Instance.selectedObjects[0].GetComponent<MouseInteraction>().ringEffect.SetActive(false);
            Instance.selectedObjects[0].GetComponent<MouseInteraction>().isSelected = false;
            Instance.selectedObjects.Clear();
        }
    }

    public void TakeDataActivateCharacterInteractPanel(GameObject _characterObj, GameObject _playerObj)
    {
        Instance.interactedCharacter = _characterObj;
        Instance.player = _playerObj;

        Character charNpc = _characterObj.GetComponent<Character>();
        Character charPlayer = _playerObj.GetComponent<Character>();

        if (ClanManager.Instance.IsEnemy(charNpc.clan, charPlayer.clan))
        {
            UIManager.Instance.ToggleInteractCharacterPanel(true);
        }
        else
        {
            UIManager.Instance.ToggleInteractCharacterPanel(false);
        }  
    }
    public void TakeDataActivateSettlementInteractPanel(GameObject _townObj, GameObject _playerObj)
    {
        Instance.interactedSettlement = _townObj;
        Instance.player = _playerObj;
        UIManager.Instance.ToggleInteractSettlementPanel();
    }
    public void EnterWarCommand()
    {
        Instance.interactedCharacter.GetComponent<NPCAI>().SpawnWarHandler(player.GetComponent<Character>());
    }

    public void SendToTownCommand()
    {
        NPCAI _interactedCharacterAI = interactedCharacter.GetComponent<NPCAI>();
        _interactedCharacterAI.GoToClosestAllyTown();
        player.GetComponent<Character>().SetCharacterState(Character.State.Free);
    }

    public void SendToThePointCommand()
    {
        NPCAI _interactedCharacterAI = interactedCharacter.GetComponent<NPCAI>();
        player.GetComponent<Character>().SetCharacterState(Character.State.Free);
        _interactedCharacterAI.LeaveInteraction();
        _interactedCharacterAI.GoToRandomPoint();
    }

    //TODO: add offset for follow
    public void FollowCommand()
    {
        NPCAI _interactedCharacterAI = interactedCharacter.GetComponent<NPCAI>();
        player.GetComponent<Character>().SetCharacterState(Character.State.Free);
        _interactedCharacterAI.FollowTarget(player);
    }

    public void LeaveInteractionCommand()
    {
        NPCAI _interactedCharacterAI = interactedCharacter.GetComponent<NPCAI>();

        //If npc is not in settlement
        if (!_interactedCharacterAI.NPC.IsCharacterState(Character.State.InSettlement))
        {
            _interactedCharacterAI.LeaveInteraction();
            player.GetComponent<Character>().SetCharacterState(Character.State.Free);
        }
        
    }

    public void LeaveSettlementCommand()
    {
        //player.GetComponent<PlayerController>().ClearClickedTarget();
        player.GetComponent<Character>().LeaveSettlement();
    }

}
