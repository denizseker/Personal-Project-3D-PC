using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MouseInteraction : MonoBehaviour
{
    private Army army;
    private Player playerManager;
    private NPC npcManager;
    private Settlement settlement;
    private WarHandler warHandler;
    public GameObject ringEffect;


    private GameObject player;
    private NPCAI NPCAI;

    public bool isSelected = false;
    private void Awake()
    {
        if (GetComponent<Army>() != null) army = GetComponent<Army>();
        if (GetComponent<Player>() != null) playerManager = GetComponent<Player>();
        if (GetComponent<NPC>() != null) npcManager = GetComponent<NPC>();
        if (GetComponent<Settlement>() != null) settlement = GetComponent<Settlement>();
        if (GetComponent<WarHandler>() != null) warHandler = GetComponent<WarHandler>();
        //town ise null olacak.
        NPCAI = GetComponent<NPCAI>();
        player = GameObject.FindWithTag("Player");
    }

    //public void InteractAreaOnMouseEnter()
    //{
    //    //Sending infos to the right UI panel.
    //    //Mouse over on player
    //    if (playerManager != null)
    //    {
    //        UIManager.Instance.UpdateSoldierPanel(playerManager.characterName, playerManager.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject, 10f);
    //        //Activate info panel
    //        UIManager.Instance.UI_soldierPanel.gameObject.SetActive(true);
    //        UIManager.Instance.isSoldierPanelActive = true;
    //        ringEffect.SetActive(true);
    //    }
    //    //Mouse over on npc
    //    else if (npcManager != null)
    //    {
    //        UIManager.Instance.UpdateSoldierPanel(npcManager.characterName, npcManager.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject, npcManager.speed);
    //        //Activate info panel
    //        UIManager.Instance.UI_soldierPanel.gameObject.SetActive(true);
    //        UIManager.Instance.isSoldierPanelActive = true;
    //        ringEffect.SetActive(true);
    //    }
    //    //Mouse over on settlement
    //    else if (settlement != null)
    //    {
    //        UIManager.Instance.UpdateSoldierPanel(settlement.settlementName, settlement.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject, 0f);
    //        //Activate info panel
    //        UIManager.Instance.UI_soldierPanel.gameObject.SetActive(true);
    //        UIManager.Instance.isSoldierPanelActive = true;
    //        ringEffect.SetActive(true);
    //    }
    //    else if (warHandler != null)
    //    {
    //        UIManager.Instance.UpdateWarPanel(gameObject, warHandler.pastTimeString, warHandler.party1, warHandler.party2);
    //        //Activate war panel
    //        UIManager.Instance.UI_warHandlerPanel.gameObject.SetActive(true);
    //        UIManager.Instance.isWarHandlerPanelActive = true;
    //        ringEffect.SetActive(true);
    //    }
    //}
    //public void InteractAreaOnMouseExit()
    //{
    //    UIManager.Instance.UI_warHandlerPanel.gameObject.SetActive(false);
    //    UIManager.Instance.isWarHandlerPanelActive = false;
    //    UIManager.Instance.UI_soldierPanel.gameObject.SetActive(false);
    //    UIManager.Instance.isSoldierPanelActive = false;
    //    if (!isSelected) ringEffect.SetActive(false);
    //}
    //public void InteractAreaOnMouseOver()
    //{
    //    ringEffect.SetActive(true);
    //    if (UIManager.Instance.isWarHandlerPanelActive) UIManager.Instance.UpdateWarPanel(gameObject, warHandler.pastTimeString, warHandler.party1, warHandler.party2);
    //}
    //public void InteractAreaOnMouseDown()
    //{
    //    //Resetting selected object for every town/enemy click
    //    UIManager.Instance.ClearSelectedObjects();
    //    UIManager.Instance.selectedObjects.Add(gameObject);

    //    ringEffect.SetActive(true);
    //    isSelected = true;
    //    //Clicking to town
    //    if (isSelected && settlement != null)
    //    {
    //        NavMeshAgent playerAgent = player.GetComponent<NavMeshAgent>();
    //        Vector3 townPosition = settlement.gameObject.GetComponentInChildren<GetCharacterInSettlement>().transform.position;
    //        playerAgent.destination = townPosition;
    //    }
    //}

    private void OnMouseEnter()
    {
        //Sending infos to the right UI panel.
        //Mouse over on player
        if (playerManager != null)
        {
            UIManager.Instance.UpdateSoldierPanel(playerManager.characterName, playerManager.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject, 10f);
            //Activate info panel
            UIManager.Instance.UI_soldierPanel.gameObject.SetActive(true);
            UIManager.Instance.isSoldierPanelActive = true;
            ringEffect.SetActive(true);
        }
        //Mouse over on npc
        else if (npcManager != null)
        {
            UIManager.Instance.UpdateSoldierPanel(npcManager.characterName, npcManager.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject, npcManager.speed);
            //Activate info panel
            UIManager.Instance.UI_soldierPanel.gameObject.SetActive(true);
            UIManager.Instance.isSoldierPanelActive = true;
            ringEffect.SetActive(true);
        }
        //Mouse over on settlement
        else if (settlement != null)
        {
            UIManager.Instance.UpdateSoldierPanel(settlement.settlementName, settlement.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject, 0f);
            //Activate info panel
            UIManager.Instance.UI_soldierPanel.gameObject.SetActive(true);
            UIManager.Instance.isSoldierPanelActive = true;
            ringEffect.SetActive(true);
        }
        else if (warHandler != null)
        {
            UIManager.Instance.UpdateWarPanel(gameObject, warHandler.pastTimeString, warHandler.party1, warHandler.party2);
            //Activate war panel
            UIManager.Instance.UI_warHandlerPanel.gameObject.SetActive(true);
            UIManager.Instance.isWarHandlerPanelActive = true;
            ringEffect.SetActive(true);
        }

    }
    private void OnMouseOver()
    {
        ringEffect.SetActive(true);
        if (UIManager.Instance.isWarHandlerPanelActive) UIManager.Instance.UpdateWarPanel(gameObject, warHandler.pastTimeString, warHandler.party1, warHandler.party2);
    }
    private void OnMouseExit()
    {
        UIManager.Instance.UI_warHandlerPanel.gameObject.SetActive(false);
        UIManager.Instance.isWarHandlerPanelActive = false;
        UIManager.Instance.UI_soldierPanel.gameObject.SetActive(false);
        UIManager.Instance.isSoldierPanelActive = false;
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
