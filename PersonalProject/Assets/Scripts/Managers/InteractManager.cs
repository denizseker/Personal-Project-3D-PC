using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager Instance;

    public GameObject interactedSettlement;
    public GameObject interactedCharacter;
    public GameObject player;

    private void Awake()
    {
        Instance = this;
    }

    public void TakeDataActivateCharacterInteractPanel(GameObject _characterObj, GameObject _playerObj)
    {
        Instance.interactedCharacter = _characterObj;
        Instance.player = _playerObj;
        UIManager.Instance.ToggleInteractCharacterPanel();
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

    public void SendToThePointCommand()
    {
        NPCAI _interactedCharacterAI = interactedCharacter.GetComponent<NPCAI>();
        player.GetComponent<Character>().SetCharacterState(Character.State.Free);
        _interactedCharacterAI.LeaveInteraction();
        _interactedCharacterAI.GoToRandomPoint();
    }

    //add offset for follow
    public void FollowCommand()
    {
        NPCAI _interactedCharacterAI = interactedCharacter.GetComponent<NPCAI>();
        player.GetComponent<Character>().SetCharacterState(Character.State.Free);
        _interactedCharacterAI.FollowTarget(player);
    }

    public void EndInteractionCommand()
    {
        NPCAI _interactedCharacterAI = interactedCharacter.GetComponent<NPCAI>();

        _interactedCharacterAI.LeaveInteraction();
        player.GetComponent<Character>().SetCharacterState(Character.State.Free);
    }

    public void LeaveSettlementCommand()
    {
        //player.GetComponent<PlayerController>().ClearClickedTarget();
        player.GetComponent<Character>().LeaveSettlement();
    }

}
