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


    private GameObject player;
    private NPCAI NPCAI;

    public bool isSelected = false;
    private void Awake()
    {
        if (GetComponent<Character>() != null) character = GetComponent<Character>();
        if (GetComponent<Settlement>() != null) settlement = GetComponent<Settlement>();
        if (GetComponent<WarHandler>() != null) warHandler = GetComponent<WarHandler>();
        //town ise null olacak.
        NPCAI = GetComponent<NPCAI>();
        player = GameObject.FindWithTag("Player");
    }

    private void OnMouseEnter()
    {
        //Mouse over on character
        if (character != null)
        {
            //Activate info panel
            UIManager.Instance.ActivateCharacterInfoPanel(character);
            ringEffect.SetActive(true);
        }
        //Mouse over on settlement
        else if (settlement != null)
        {
            //UIManager.Instance.UpdateSoldierPanel(settlement.settlementName, settlement.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject, 0f);
            ////Activate info panel
            //UIManager.Instance.UI_soldierPanel.gameObject.SetActive(true);
            //UIManager.Instance.isSoldierInfoPanelActive = true;
            //ringEffect.SetActive(true);
        }
        else if (warHandler != null)
        {
            //Activate war panel
            UIManager.Instance.ActivateWarInfoPanel(warHandler);
            ringEffect.SetActive(true);
        }

    }
    private void OnMouseOver()
    {
        ringEffect.SetActive(true);
        if (UIManager.Instance.UI_warInfoPanel.isPanelActive) UIManager.Instance.UI_warInfoPanel.UpdatePanel(warHandler);
    }
    private void OnMouseExit()
    {
        //UIManager.Instance.UI_warHandlerPanel.gameObject.SetActive(false);
        UIManager.Instance.DeActivateWarInfoPanel();
        UIManager.Instance.DeActivateCharacterInfoPanel();
        if (!isSelected) ringEffect.SetActive(false);

    }
    private void OnMouseDown()
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

    public void OnOffCollider()
    {
        GetComponent<Collider>().enabled = !GetComponent<Collider>().enabled;
    }

}
