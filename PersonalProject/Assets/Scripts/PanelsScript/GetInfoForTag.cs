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
        //if script at settlement
        if (GetComponentInParent<Settlement>() != null)
        {
            Settlement settlement = GetComponentInParent<Settlement>();

            nameText.text = settlement.settlementName;
            clanLogo.sprite = settlement.clan.clanLogo;
            troops.text = GetComponentInParent<Army>().armyTotalTroops.ToString();
        }
        //if script at character
        else if (GetComponentInParent<Character>() != null)
        {
            Character character = GetComponentInParent<Character>();

            nameText.text = character.characterName;
            clanLogo.sprite = character.clan.clanLogo;
            troops.text = GetComponentInParent<Army>().armyTotalTroops.ToString();
            state.text = character.currentState.ToString();
            if (character.interactedCharacter != null) state.text += " " + character.interactedCharacter.characterName;
        }
    }
}
