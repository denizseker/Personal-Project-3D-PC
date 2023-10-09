using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//This script getting info for nametag.

public class GetInfoForTag : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image clanLogo;
    [SerializeField] private TMP_Text troops;
    [SerializeField] private TMP_Text state;


    private void Start()
    {
        //UpdateNameTag();
    }
    private void Update()
    {
        UpdateNameTag();
    }

    public void UpdateNameTag()
    {
        //if script in settlement
        if (GetComponentInParent<Settlement>() != null)
        {
            Settlement settlement = GetComponentInParent<Settlement>();

            nameText.text = settlement.settlementName;
            clanLogo.sprite = settlement.clan.clanLogo;
            troops.text = GetComponentInParent<Army>().armyTotalTroops.ToString();
        }
        //if script in npc
        else if (GetComponentInParent<NPC>() != null)
        {
            NPC npcManager = GetComponentInParent<NPC>();

            nameText.text = npcManager.characterName;
            clanLogo.sprite = npcManager.clan.clanLogo;
            troops.text = GetComponentInParent<Army>().armyTotalTroops.ToString();
            state.text = npcManager.currentState.ToString();
            if (npcManager.interactedCharacter != null) state.text += " " + npcManager.interactedCharacter.characterName;
        }
        //if script in player
        //else if(GetComponentInParent<PlayerManager>() != null)
        //{
        //    clanText.text = GetComponentInParent<PlayerManager>().clan.clanName;
        //    clanLogo = GetComponentInParent<PlayerManager>().clan.clanLogo;
        //}
    }
}
