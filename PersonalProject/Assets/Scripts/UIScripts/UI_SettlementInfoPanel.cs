using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_SettlementInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text settlementTitleText;
    [SerializeField] private TMP_Text settlementClanText;
    [SerializeField] private TMP_Text settlementLordText;
    [SerializeField] private TMP_Text settlementProsperityText;
    [SerializeField] private TMP_Text settlementMilitaText;
    [SerializeField] private TMP_Text settlementVillagesText;
    [SerializeField] private TMP_Text settlementDefendersText;
    [SerializeField] private TMP_Text settlementDefendersOfTownText;
    [SerializeField] private TMP_Text settlementMilitaOfTownText;

    [HideInInspector] public bool isPanelActive = false;


    public void UpdatePanel(Settlement _settlement)
    {
        settlementTitleText.text = _settlement.settlementName;
        settlementClanText.text = _settlement.clan.clanName;
        settlementLordText.text = "Sir Test";
        settlementProsperityText.text = "TestValue";
        settlementMilitaText.text = _settlement.manPower.ToString();
        settlementVillagesText.text = "TestVillage1";
        settlementDefendersText.text = (_settlement.army.armyTotalTroops + _settlement.manPower).ToString();
        settlementDefendersOfTownText.text = _settlement.army.armyTotalTroops.ToString();
        settlementMilitaOfTownText.text = _settlement.manPower.ToString();
    }
}
