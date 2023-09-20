using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        NPCAI = GetComponentInChildren<NPCAI>();
        player = GameObject.FindWithTag("Player");

    }

    private void OnMouseEnter()
    {
        //Sending infos to the right UI panel.

        //Mouse over on player
        if (playerManager != null)
        {
            UIManager.Instance.UpdateInfoPanel(playerManager.characterName, playerManager.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject,10f);
            //Activate info panel
            UIManager.Instance.UI_soldierPanel.gameObject.SetActive(true);
            UIManager.Instance.isInfoPanelActive = true;
            ringEffect.SetActive(true);
        }
        //Mouse over on npc
        else if (npcManager != null)
        {
            UIManager.Instance.UpdateInfoPanel(npcManager.characterName, npcManager.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject,npcManager.speed);
            //Activate info panel
            UIManager.Instance.UI_soldierPanel.gameObject.SetActive(true);
            UIManager.Instance.isInfoPanelActive = true;
            ringEffect.SetActive(true);
        }
        //Mouse over on settlement
        else if (settlement != null)
        {
            UIManager.Instance.UpdateInfoPanel(settlement.settlementName, settlement.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject,0f);
            //Activate info panel
            UIManager.Instance.UI_soldierPanel.gameObject.SetActive(true);
            UIManager.Instance.isInfoPanelActive = true;
            ringEffect.SetActive(true);
        }
        else if (warHandler != null)
        {
            UIManager.Instance.UpdateWarPanel(gameObject,warHandler.pastTimeString,warHandler.character1,warHandler.character2);
            //Activate war panel
            UIManager.Instance.UI_warHandlerPanel.gameObject.SetActive(true);
            UIManager.Instance.isWarHandlerPanelActive = true;
            ringEffect.SetActive(true);
        }

        
    }

    private void OnMouseOver()
    {
        ringEffect.SetActive(true);
        if(UIManager.Instance.isWarHandlerPanelActive) UIManager.Instance.UpdateWarPanel(gameObject, warHandler.pastTimeString,warHandler.character1,warHandler.character2);
    }

    private void OnMouseExit()
    {
      UIManager.Instance.UI_warHandlerPanel.gameObject.SetActive(false);
      UIManager.Instance.isWarHandlerPanelActive = false;
      UIManager.Instance.UI_soldierPanel.gameObject.SetActive(false);
      UIManager.Instance.isInfoPanelActive = false;
      if (!isSelected) ringEffect.SetActive(false);
    }


    //Clicking to enemy or town
    private void OnMouseDown()
    {
        //Resetting selected object for every town/enemy click
        UIManager.Instance.ClearSelectedObjects();
        UIManager.Instance.selectedObjects.Add(gameObject);

        ringEffect.SetActive(true);
        isSelected = true;
        //Clicking to town
        if (isSelected && GetComponentInChildren<NPCAI>() == null)
        {
            NavMeshAgent playerAgent = player.GetComponent<NavMeshAgent>();
            Collider col = GetComponentInParent<Collider>();
            playerAgent.destination = col.ClosestPoint(playerAgent.transform.position);
        }
    }

    private void Update()
    {
        //If clicked object is enemy we are updating destination for follow.
        if (GetComponentInChildren<NPCAI>() != null)
        {
            if (isSelected)
            {
                player.GetComponent<NavMeshAgent>().SetDestination(transform.position);
            }
        }
    }
}
