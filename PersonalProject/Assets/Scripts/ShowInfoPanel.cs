using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfoPanel : MonoBehaviour
{
    private Army army;
    private PlayerManager playerManager;
    private NPCManager npcManager;
    private Settlement settlement;
    private void Awake()
    {
        if(GetComponent<Army>() != null) army = GetComponent<Army>();
        if(GetComponent<PlayerManager>() != null) playerManager = GetComponent<PlayerManager>();
        if(GetComponent<NPCManager>() != null) npcManager = GetComponent<NPCManager>();
        if (GetComponent<Settlement>() != null) settlement = GetComponent<Settlement>();

    }

    private void OnMouseEnter()
    {
        //Mouse over on player
        if(playerManager != null)
        {
            UIManager.Instance.UpdateInfoPanel(playerManager.playerName, playerManager.clan, army.GetArmySize(), army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount);
        }
        //Mouse over on npc
        else if (npcManager != null)
        {
            UIManager.Instance.UpdateInfoPanel(npcManager.npcName, npcManager.clan, army.GetArmySize(), army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount);
        }
        //Mouse over on settlement
        else if (settlement != null)
        {
            UIManager.Instance.UpdateInfoPanel(settlement.settlementName, settlement.clan, army.GetArmySize(), army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount);
        }
        UIManager.Instance.UI_soldierPanel.gameObject.SetActive(true);
        UIManager.Instance.isPanelActive = true;


    }



    private void OnMouseExit()
    {
        UIManager.Instance.UI_soldierPanel.gameObject.SetActive(false);
        UIManager.Instance.isPanelActive = false;
    }
}
