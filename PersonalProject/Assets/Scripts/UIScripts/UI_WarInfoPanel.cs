using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_WarInfoPanel : MonoBehaviour
{
    [Header("War Panel")]
    [SerializeField] private TMP_Text timeText;
    //Values for warpanel
    [Header("Party1")]
    [SerializeField] private TMP_Text party1_nameText;
    [SerializeField] private TMP_Text party1_totalLiveText;
    [SerializeField] private TMP_Text party1_totalDeathText;
    [SerializeField] private TMP_Text party1_peasentLiveText;
    [SerializeField] private TMP_Text party1_peasentDeathText;
    [SerializeField] private TMP_Text party1_swordsManLiveText;
    [SerializeField] private TMP_Text party1_swordsManDeathText;
    [SerializeField] private TMP_Text party1_horseManLiveText;
    [SerializeField] private TMP_Text party1_horseManDeathText;
    [SerializeField] private TMP_Text party1_cavalaryLiveText;
    [SerializeField] private TMP_Text party1_cavalaryDeathText;
    [SerializeField] private TMP_Text party1_eliteCavalaryLiveText;
    [SerializeField] private TMP_Text party1_eliteCavalaryDeathText;
    [SerializeField] private TMP_Text party1_participantText;
    [Header("Party2")]
    [SerializeField] private TMP_Text party2_nameText;
    [SerializeField] private TMP_Text party2_totalLiveText;
    [SerializeField] private TMP_Text party2_totalDeathText;
    [SerializeField] private TMP_Text party2_peasentLiveText;
    [SerializeField] private TMP_Text party2_peasentDeathText;
    [SerializeField] private TMP_Text party2_swordsManLiveText;
    [SerializeField] private TMP_Text party2_swordsManDeathText;
    [SerializeField] private TMP_Text party2_horseManLiveText;
    [SerializeField] private TMP_Text party2_horseManDeathText;
    [SerializeField] private TMP_Text party2_cavalaryLiveText;
    [SerializeField] private TMP_Text party2_cavalaryDeathText;
    [SerializeField] private TMP_Text party2_eliteCavalaryLiveText;
    [SerializeField] private TMP_Text party2_eliteCavalaryDeathText;
    [SerializeField] private TMP_Text party2_participantText;

    [HideInInspector] public bool isPanelActive = false;


    public void UpdatePanel(WarHandler _warHandler)
    {
        timeText.text = _warHandler.pastTimeString;

        //Party1
        Army infoArmy = _warHandler.ReturnPartyArmy(_warHandler.party1);
        party1_nameText.text = _warHandler.party1[0].characterName;
        party1_totalLiveText.text = infoArmy.armyTotalTroops.ToString();
        party1_totalDeathText.text = "0";
        party1_peasentLiveText.text = infoArmy.PeasentRecruit.amount.ToString();
        party1_peasentDeathText.text = "0";
        party1_swordsManLiveText.text = infoArmy.SwordsMan.amount.ToString();
        party1_swordsManDeathText.text = "0";
        party1_horseManLiveText.text = infoArmy.HorseMan.amount.ToString();
        party1_horseManDeathText.text = "0";
        party1_cavalaryLiveText.text = infoArmy.Cavalary.amount.ToString();
        party1_cavalaryDeathText.text = "0";
        party1_eliteCavalaryLiveText.text = infoArmy.EliteCavalary.amount.ToString();
        party1_eliteCavalaryDeathText.text = "0";
        party1_participantText.text = "";
        //Party2
        infoArmy = _warHandler.ReturnPartyArmy(_warHandler.party2);
        party2_nameText.text = _warHandler.party2[0].characterName;
        party2_totalLiveText.text = infoArmy.armyTotalTroops.ToString();
        party2_totalDeathText.text = "0";
        party2_peasentLiveText.text = infoArmy.PeasentRecruit.amount.ToString();
        party2_peasentDeathText.text = "0";
        party2_swordsManLiveText.text = infoArmy.SwordsMan.amount.ToString();
        party2_swordsManDeathText.text = "0";
        party2_horseManLiveText.text = infoArmy.HorseMan.amount.ToString();
        party2_horseManDeathText.text = "0";
        party2_cavalaryLiveText.text = infoArmy.Cavalary.amount.ToString();
        party2_cavalaryDeathText.text = "0";
        party2_eliteCavalaryLiveText.text = infoArmy.EliteCavalary.amount.ToString();
        party2_eliteCavalaryDeathText.text = "0";
        party2_participantText.text = "";
    }
}
