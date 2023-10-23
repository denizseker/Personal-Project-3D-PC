using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager Instance;

    private GameObject interactedTown;
    private GameObject interactedCharacter;
    private GameObject player;

    private void Awake()
    {
        Instance = this;
    }

    public void TakeDataActivateInteractPanel(GameObject _characterObj, GameObject _player)
    {
        Instance.interactedCharacter = _characterObj;
        Instance.player = _player;
        UIManager.Instance.ToggleInteractCharacterPanel();
    }

    public void EnterWarCommand()
    {
        Debug.Log("War");
        Instance.interactedCharacter.GetComponent<NPCAI>().SpawnWarHandler(player.GetComponent<Character>());
    }

    public void SendToThePointCommand()
    {
        NPCAI _interactedCharacterAI = interactedCharacter.GetComponent<NPCAI>();
        player.GetComponent<Character>().currentState = Character.CurrentState.Free;
        _interactedCharacterAI.LeaveInteraction();
        _interactedCharacterAI.GoToRandomPoint();
    }

    //add offset for follow
    public void FollowCommand()
    {
        NPCAI _interactedCharacterAI = interactedCharacter.GetComponent<NPCAI>();
        player.GetComponent<Character>().currentState = Character.CurrentState.Free;
        _interactedCharacterAI.FollowTarget(player);
    }

    public void EndInteractionCommand()
    {
        NPCAI _interactedCharacterAI = interactedCharacter.GetComponent<NPCAI>();

        _interactedCharacterAI.LeaveInteraction();
        player.GetComponent<Character>().currentState = Character.CurrentState.Free;
    }
}
