using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public RectTransform UI_soldierPanel;
    public RectTransform UI_warHandlerPanel;
    public RectTransform UI_settlementPanel;
    public float offsetXPercentage = 0.07f; // Horizontally
    public float offsetYPercentage = -0.18f; // Vertically

    public bool isSoldierPanelActive = false;
    public bool isWarHandlerPanelActive = false;
    public bool isSettlementPanelActive = false;

    private GameObject obje;
    [SerializeField] private TMP_Text timeScaleText;
    [SerializeField] private TMP_Text InGameHourText;
    
    [Header("Soldier Panel")]
    //Values for infopanel
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text clanText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text troopsText;
    [SerializeField] private TMP_Text peasentRecruitText;
    [SerializeField] private TMP_Text swordsManText;
    [SerializeField] private TMP_Text horseManText;
    [SerializeField] private TMP_Text cavalaryText;
    [SerializeField] private TMP_Text eliteCavalaryText;

    //[Header("Settlement Panel")]
    ////Values for settlementpanel
    //[SerializeField] private TMP_Text titleText;
    //[SerializeField] private TMP_Text clanText;
    //[SerializeField] private TMP_Text speedText;
    //[SerializeField] private TMP_Text troopsText;
    //[SerializeField] private TMP_Text peasentRecruitText;
    //[SerializeField] private TMP_Text swordsManText;
    //[SerializeField] private TMP_Text horseManText;
    //[SerializeField] private TMP_Text cavalaryText;
    //[SerializeField] private TMP_Text eliteCavalaryText;

    [Header("War Panel")]
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

    [SerializeField] private TMP_Text timeText;





    [HideInInspector] public List<GameObject> selectedObjects = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    //Player can click 1 object at same time.
    public void ClearSelectedObjects()
    {
        //If we have a clicked object already
        if (Instance.selectedObjects.Count == 1)
        {
            Instance.selectedObjects[0].GetComponent<MouseInteraction>().ringEffect.SetActive(false);
            Instance.selectedObjects[0].GetComponent<MouseInteraction>().isSelected = false;
            Instance.selectedObjects.Clear();
        }
    }

    private void Update()
    {
        AdjustPanelPos(Instance.isSoldierPanelActive,Instance.UI_soldierPanel);
        AdjustPanelPos(Instance.isWarHandlerPanelActive, Instance.UI_warHandlerPanel);
    }

    //Setting panel pos with new anchored pos 
    private void SetPanelPosWithAnchor(Vector3 _targetPos,RectTransform _panel,float _panelHalfWidth,float _panelHalfHeight)
    {
        int mousePosIndex = MouseInWhichPartOfScreen();

        switch (mousePosIndex)
        {
            //Top Left
            case 1:
                _panel.position = new Vector3(_targetPos.x + _panelHalfWidth, _targetPos.y - _panelHalfHeight, 0);
                break;
            //Top Right
            case 2:
                _panel.position = new Vector3(_targetPos.x - _panelHalfWidth, _targetPos.y - _panelHalfHeight, 0);
                break;
            //Bottom Left
            case 3:
                _panel.position = new Vector3(_targetPos.x + _panelHalfWidth, _targetPos.y + _panelHalfHeight, 0);
                break;
            //Bottom right
            case 4:
                _panel.position = new Vector3(_targetPos.x - _panelHalfWidth, _targetPos.y + _panelHalfHeight, 0);
                break;
            //Out of screen
            case 5:
                Debug.Log("Out of screen");
                break;
            default:
                print("Out of value");
                break;
        }
    }

    private int MouseInWhichPartOfScreen()
    {
        Vector3 mousePos = Input.mousePosition;

        //Left top
        if (mousePos.x <= Screen.width / 2 && mousePos.y >= Screen.height / 2)
        {
            //Debug.Log("Sol üst");
            return 1;
        }
        //Right top
        else if (mousePos.x >= Screen.width / 2 && mousePos.y >= Screen.height / 2)
        {
            //Debug.Log("Sað üst");
            return 2;
        }
        //Left bottom
        else if (mousePos.x <= Screen.width / 2 && mousePos.y <= Screen.height/2)
        {
            //Debug.Log("Sol alt");
            return 3;
        }
        //Right bottom
        else if (mousePos.x >= Screen.width / 2 && mousePos.y <= Screen.height / 2)
        {
            //Debug.Log("Sað alt");
            return 4;
        }
        //out of screen
        else
        {
            return 5;
        }
    }

    public void AdjustPanelPos(bool _isPanelActive,RectTransform _panel)
    {
        //Panel attached to Object.
        if (_isPanelActive)
        {
            Vector3 panelPosition = Camera.main.WorldToScreenPoint(Instance.obje.transform.position);

            //Vector3 panelPosition = Camera.main.WorldToScreenPoint(Input.mousePosition);

            Vector3 mousePos = Input.mousePosition;
            ////Adjusting panel position to mouse position for different resolation
            //float offsetWidth = Screen.width * Instance.offsetXPercentage;
            //float offsetHeight = Screen.height * Instance.offsetYPercentage;

            //panelPosition.x += offsetWidth;
            //panelPosition.y += offsetHeight;

            // Adjusting the panel position to not exceed the screen boundaries.
            float panelHalfWidth = _panel.rect.width * 0.5f;
            float panelHalfHeight = _panel.rect.height * 0.5f;

            float minX = panelHalfWidth;
            float maxX = Screen.width - panelHalfWidth;
            float minY = panelHalfHeight;
            float maxY = Screen.height - panelHalfHeight;

            //float normalizedDistance = Mathf.Clamp01(panelPosition.z / 500);
            //Debug.Log(new Vector3(_panel.localScale.x * normalizedDistance, _panel.localScale.y * normalizedDistance, _panel.localScale.z * normalizedDistance));
            //_panel.localScale = (_panel.localScale.x * normalizedDistance) * Vector3.one;

            //panelPosition = new Vector3(panelPosition.x + panelHalfWidth, panelPosition.y - panelHalfHeight, 0);

            SetPanelPosWithAnchor(panelPosition, _panel,panelHalfWidth,panelHalfHeight);

            mousePos.x = Mathf.Clamp(mousePos.x, minX, maxX);
            mousePos.y = Mathf.Clamp(mousePos.y, minY, maxY);

            //_panel.position = new Vector3(mousePos.x,mousePos.y,0);
        }
    }


    //UI bottom panel timescale update
    public void UpdateTimeScaleText()
    {
        timeScaleText.text = Time.timeScale + "x";
    }
    //UI Bottom panel date update
    public void UpdateDateText()
    {
        if(TimeManager.Instance.InGameHour < 10)
        {
            Instance.InGameHourText.text = "0" + TimeManager.Instance.InGameHour + ":00";
        }  
        else
        {
            Instance.InGameHourText.text = TimeManager.Instance.InGameHour + ":00";
        }
            
        TimeManager.Instance.DateText.text = TimeManager.Instance.currentSeason + "  " + TimeManager.Instance.InGameDay + ",  " + TimeManager.Instance.InGameYear;
    }
    public void UpdateSoldierPanel(string _name, string _clan, int _troops, int _peasentrecruit, int _swordsman, int _horseman, int _cavalary, int _elitecavalary, GameObject _object, float _speed)
    {
        Instance.obje = _object;
        Instance.titleText.text = _name;
        Instance.clanText.text = _clan;
        Instance.speedText.text = _speed.ToString();
        Instance.troopsText.text = "(" + _troops.ToString() + ")";
        Instance.peasentRecruitText.text = _peasentrecruit.ToString();
        Instance.swordsManText.text = _swordsman.ToString();
        Instance.horseManText.text = _horseman.ToString();
        Instance.cavalaryText.text = _cavalary.ToString();
        Instance.eliteCavalaryText.text = _elitecavalary.ToString();
    }
    public void UpdateWarPanel(GameObject _object, string _time, Character _character1,Character _character2)
    {
        Instance.obje = _object;
        Instance.timeText.text = _time;

        //Party1
        party1_nameText.text = _character1.characterName;
        party1_totalLiveText.text = _character1.army.armyTotalTroops.ToString();
        party1_totalDeathText.text = "0";
        party1_peasentLiveText.text = _character1.army.PeasentRecruit.amount.ToString();
        party1_peasentDeathText.text = "0";
        party1_swordsManLiveText.text = _character1.army.SwordsMan.amount.ToString();
        party1_swordsManDeathText.text = "0";
        party1_horseManLiveText.text = _character1.army.HorseMan.amount.ToString();
        party1_horseManDeathText.text = "0";
        party1_cavalaryLiveText.text = _character1.army.Cavalary.amount.ToString();
        party1_cavalaryDeathText.text = "0";
        party1_eliteCavalaryLiveText.text = _character1.army.EliteCavalary.amount.ToString();
        party1_eliteCavalaryDeathText.text = "0";
        party1_participantText.text = "";
        //Party2
        party2_nameText.text = _character2.characterName;
        party2_totalLiveText.text = _character2.army.armyTotalTroops.ToString();
        party2_totalDeathText.text = "0";
        party2_peasentLiveText.text = _character2.army.PeasentRecruit.amount.ToString();
        party2_peasentDeathText.text = "0";
        party2_swordsManLiveText.text = _character2.army.SwordsMan.amount.ToString();
        party2_swordsManDeathText.text = "0";
        party2_horseManLiveText.text = _character2.army.HorseMan.amount.ToString();
        party2_horseManDeathText.text = "0";
        party2_cavalaryLiveText.text = _character2.army.Cavalary.amount.ToString();
        party2_cavalaryDeathText.text = "0";
        party2_eliteCavalaryLiveText.text = _character2.army.EliteCavalary.amount.ToString();
        party2_eliteCavalaryDeathText.text = "0";
        party2_participantText.text = "";
    }
    public void UpdateSettlementPanel()
    {

    }
}
