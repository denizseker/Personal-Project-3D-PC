using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseInteraction : MonoBehaviour
{
    private Army army;
    private PlayerManager playerManager;
    private NPCManager npcManager;
    private Settlement settlement;
    public GameObject ringEffect;


    private GameObject player;
    private NPCAI NPCAI;

    public bool isSelected = false;
    private void Awake()
    {
        if (GetComponent<Army>() != null) army = GetComponent<Army>();
        if (GetComponent<PlayerManager>() != null) playerManager = GetComponent<PlayerManager>();
        if (GetComponent<NPCManager>() != null) npcManager = GetComponent<NPCManager>();
        if (GetComponent<Settlement>() != null) settlement = GetComponent<Settlement>();
        //town ise null olacak.
        NPCAI = GetComponentInChildren<NPCAI>();
        player = GameObject.FindWithTag("Player");

    }

    private void OnMouseEnter()
    {
        //Sending infos to the UI panel.

        //Mouse over on player
        if (playerManager != null)
        {
            UIManager.Instance.UpdateInfoPanel(playerManager.playerName, playerManager.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject,npcManager.speed);
        }
        //Mouse over on npc
        else if (npcManager != null)
        {
            UIManager.Instance.UpdateInfoPanel(npcManager.npcName, npcManager.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject,10f);
        }
        //Mouse over on settlement
        else if (settlement != null)
        {
            UIManager.Instance.UpdateInfoPanel(settlement.settlementName, settlement.clan.clanName, army.armyTotalTroops, army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount, gameObject,0f);
        }

        //Activate info panel
        UIManager.Instance.UI_soldierPanel.gameObject.SetActive(true);
        UIManager.Instance.isPanelActive = true;
        ringEffect.SetActive(true);
    }

    private void OnMouseOver()
    {
        ringEffect.SetActive(true);
    }

    private void OnMouseExit()
    {
        //deactivate info panel
        UIManager.Instance.UI_soldierPanel.gameObject.SetActive(false);
        UIManager.Instance.isPanelActive = false;
        if(!isSelected) ringEffect.SetActive(false);

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
