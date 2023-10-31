using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_CharacterInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text soldierTitleText;
    [SerializeField] private TMP_Text soldierStateText;
    [SerializeField] private TMP_Text soldierClanText;
    [SerializeField] private TMP_Text soldierReputationText;
    [SerializeField] private TMP_Text soldierSpeedText;
    [SerializeField] private TMP_Text soldierTroopsText;
    [SerializeField] private TMP_Text soldierPeasentRecruitText;
    [SerializeField] private TMP_Text soldierSwordsmanText;
    [SerializeField] private TMP_Text soldierHorsemanText;
    [SerializeField] private TMP_Text soldierCavalaryText;
    [SerializeField] private TMP_Text soldierEliteCavalaryText;

    [HideInInspector] public bool isPanelActive = false;


    public void UpdatePanel(Character _character)
    {
        soldierTitleText.text = _character.characterName;
        soldierStateText.text = _character.currentState.ToString();
        soldierClanText.text = _character.clan.clanName;
        soldierReputationText.text = "TestValue";
        soldierSpeedText.text = _character.speed.ToString();
        soldierTroopsText.text = "(" + _character.army.armyTotalTroops.ToString() + ")";
        soldierPeasentRecruitText.text = _character.army.PeasentRecruit.amount.ToString();
        soldierSwordsmanText.text = _character.army.SwordsMan.amount.ToString();
        soldierHorsemanText.text = _character.army.HorseMan.amount.ToString();
        soldierCavalaryText.text = _character.army.Cavalary.amount.ToString();
        soldierEliteCavalaryText.text = _character.army.EliteCavalary.amount.ToString();
    }
}
