//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class CanvasController : MonoBehaviour
//{
//    public RectTransform UI_soldierPanel;
//    public RectTransform UI_nameTag;
//    private float offsetXPercentage = 0.07f; // Horizontally
//    private float offsetYPercentage = -0.18f; // Vertically
//    private bool isInfoPanelActive = false;
//    private bool isNameTagActive = true;

//    [SerializeField] private TMP_Text titleText;
//    [SerializeField] private TMP_Text clanText;
//    [SerializeField] private TMP_Text troopsText;
//    [SerializeField] private TMP_Text peasentRecruitText;
//    [SerializeField] private TMP_Text swordsManText;
//    [SerializeField] private TMP_Text horseManText;
//    [SerializeField] private TMP_Text cavalaryText;
//    [SerializeField] private TMP_Text eliteCavalaryText;

//    private Army army;
//    private PlayerManager playerManager;
//    private NPCManager npcManager;
//    private Settlement settlement;

//    private void Awake()
//    {
//        if (GetComponent<Army>() != null) army = GetComponent<Army>();
//        if (GetComponent<PlayerManager>() != null) playerManager = GetComponent<PlayerManager>();
//        if (GetComponent<NPCManager>() != null) npcManager = GetComponent<NPCManager>();
//        if (GetComponent<Settlement>() != null) settlement = GetComponent<Settlement>();
//    }


//    private void OnMouseEnter()
//    {
//        CheckTypeAndUpdate();
//        UI_soldierPanel.gameObject.SetActive(true);
//        isInfoPanelActive = true;
//    }

//    private void OnMouseOver()
//    {
//        if (!isInfoPanelActive)
//        {
//            CheckTypeAndUpdate();
//            UI_soldierPanel.gameObject.SetActive(true);
//            isInfoPanelActive = true;
//        }
        
//    }
//    private void OnMouseExit()
//    {
//        UI_soldierPanel.gameObject.SetActive(false);
//        isInfoPanelActive = false;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (isInfoPanelActive)
//        {
//            Vector3 panelPosition = Camera.main.WorldToScreenPoint(transform.position);

//            //Adjusting panel position to object position for different resolation
//            float offsetWidth = Screen.width * offsetXPercentage;
//            float offsetHeight = Screen.height * offsetYPercentage;

//            panelPosition.x += offsetWidth;
//            panelPosition.y += offsetHeight;

//            // Adjusting the panel position to not exceed the screen boundaries.
//            float panelHalfWidth = UI_soldierPanel.sizeDelta.x * 0.4f;
//            float panelHalfHeight = UI_soldierPanel.sizeDelta.y * 0.4f;

//            float minX = panelHalfWidth;
//            float maxX = Screen.width - panelHalfWidth;
//            float minY = panelHalfHeight;
//            float maxY = Screen.height - panelHalfHeight;

//            panelPosition.x = Mathf.Clamp(panelPosition.x, minX, maxX);
//            panelPosition.y = Mathf.Clamp(panelPosition.y, minY, maxY);
//            UI_soldierPanel.position = panelPosition;
//        }

//    }

//    private void CheckTypeAndUpdate()
//    {
//        //Mouse over on player
//        if (playerManager != null)
//        {
//            UpdateInfoPanel(playerManager.playerName, playerManager.clan, army.GetArmySize(), army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount);
//        }
//        //Mouse over on npc
//        else if (npcManager != null)
//        {
//            UpdateInfoPanel(npcManager.npcName, npcManager.clan, army.GetArmySize(), army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount);
//        }
//        //Mouse over on settlement
//        else if (settlement != null)
//        {
//            UpdateInfoPanel(settlement.settlementName, settlement.clan, army.GetArmySize(), army.PeasentRecruit.amount, army.SwordsMan.amount, army.HorseMan.amount, army.Cavalary.amount, army.EliteCavalary.amount);
//        }
//    }


//    public void UpdateInfoPanel(string _name, GameManager.Clans _clan, int _troops, int _peasentrecruit, int _swordsman, int _horseman, int _cavalary, int _elitecavalary)
//    {
//        titleText.text = _name;
//        clanText.text = _clan.ToString();
//        troopsText.text = "(" + _troops.ToString() + ")";
//        peasentRecruitText.text = _peasentrecruit.ToString();
//        swordsManText.text = _swordsman.ToString();
//        horseManText.text = _horseman.ToString();
//        cavalaryText.text = _cavalary.ToString();
//        eliteCavalaryText.text = _elitecavalary.ToString();
//    }

//}
