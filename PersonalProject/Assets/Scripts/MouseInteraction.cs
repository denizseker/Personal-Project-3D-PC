using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MouseInteraction : MonoBehaviour
{
    private Character character;
    private Settlement settlement;
    private WarHandler warHandler;
    public GameObject ringEffect;
    public CheckVisibility checkVisibility;


    private GameObject player;
    private NPCAI NPCAI;

    public bool isSelected = false;
    private void Awake()
    {
        if (GetComponent<Character>() != null) character = GetComponent<Character>();
        if (GetComponent<Settlement>() != null) settlement = GetComponent<Settlement>();
        if (GetComponent<WarHandler>() != null) warHandler = GetComponent<WarHandler>();
        if (gameObject.GetComponentInChildren<CheckVisibility>() != null) checkVisibility = GetComponentInChildren<CheckVisibility>();
        //town ise null olacak.
        NPCAI = GetComponent<NPCAI>();
        player = GameObject.FindWithTag("Player");
    }

    private void OnMouseEnter()
    {
        if (checkVisibility.isVisible)
        {
            //At character
            if (character != null)
            {
                UIManager.Instance.ActivateCharacterInfoPanel(character);
                ringEffect.SetActive(true);
            }
            //At settlement
            else if (settlement != null)
            {
                UIManager.Instance.ActivateSettlementInfoPanel(settlement);
                ringEffect.SetActive(true);
            }
            //At warhandler
            else if (warHandler != null)
            {
                UIManager.Instance.ActivateWarInfoPanel(warHandler);
                ringEffect.SetActive(true);
            }
        }
        

    }
    private void OnMouseOver()
    {
        if (checkVisibility.isVisible)
        {
            Debug.Log("Still visible");
            ringEffect.SetActive(true);
            if (UIManager.Instance.UI_warInfoPanel.isPanelActive) UIManager.Instance.UI_warInfoPanel.UpdatePanel(warHandler);
            if (UIManager.Instance.UI_settlementInfoPanel.isPanelActive) UIManager.Instance.UI_settlementInfoPanel.UpdatePanel(settlement);
            if (UIManager.Instance.UI_characterInfoPanel.isPanelActive) UIManager.Instance.UI_characterInfoPanel.UpdatePanel(character);
        }
        else
        {
            UIManager.Instance.DeActivateSettlementInfoPanel();
            UIManager.Instance.DeActivateWarInfoPanel();
            UIManager.Instance.DeActivateCharacterInfoPanel();
            if (!isSelected) ringEffect.SetActive(false);
        }

    }
    private void OnMouseExit()
    {
        UIManager.Instance.DeActivateSettlementInfoPanel();
        UIManager.Instance.DeActivateWarInfoPanel();
        UIManager.Instance.DeActivateCharacterInfoPanel();
        if (!isSelected) ringEffect.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (checkVisibility.isVisible)
        {
            //Cant click if player state is those
            if (!player.GetComponent<Character>().IsCharacterState(Character.State.InSettlement, Character.State.InInteraction, Character.State.InWar))
            {
                //Player cant click to player object, but can click to npc
                if (gameObject.tag != "Player")
                {
                    //Resetting selected object for every town/enemy click
                    UIManager.Instance.ClearSelectedObjects(player);
                    UIManager.Instance.selectedObjects.Add(gameObject);
                    player.GetComponent<PlayerController>().clickedTarget = gameObject;

                    ringEffect.SetActive(true);
                    isSelected = true;
                }

                //Clicking to town
                if (isSelected && settlement != null)
                {
                    //GameObject townDoor = settlement.gameObject.GetComponentInChildren<GetCharacterInSettlement>().gameObject;
                    player.GetComponent<PlayerController>().MoveToTarget(gameObject);
                }
                if (isSelected && NPCAI != null)
                {
                    player.GetComponent<PlayerController>().MoveToTarget(gameObject);
                }
            }
        }
            
    }

    public void OnOffCollider()
    {
        GetComponent<Collider>().enabled = !GetComponent<Collider>().enabled;
    }

}
