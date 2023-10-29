using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UI_CharacterInfoPanel UI_characterInfoPanel;
    public UI_WarInfoPanel UI_warInfoPanel;
    //public RectTransform UI_settlementPanel;

    public UI_InteractCharacterPanel UI_interactCharacterPanel;
    public UI_InSettlementPanel UI_inSettlementPanel;
    public float offsetXPercentage = 0.07f; // Horizontally
    public float offsetYPercentage = -0.18f; // Vertically

    private List<RectTransform> activePanels = new List<RectTransform>();

    public bool isWarHandlerPanelActive = false;

    private GameObject obje;
    [SerializeField] private TMP_Text timeScaleText;
    [SerializeField] private TMP_Text InGameHourText;

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

    public List<GameObject> selectedObjects = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    //Player can click 1 object at same time.
    public void ClearSelectedObjects(GameObject _playerObj)
    {
        //If we have a clicked object already
        if (Instance.selectedObjects.Count == 1)
        {
            _playerObj.GetComponent<PlayerController>().clickedTarget = null;
            Instance.selectedObjects[0].GetComponent<MouseInteraction>().ringEffect.SetActive(false);
            Instance.selectedObjects[0].GetComponent<MouseInteraction>().isSelected = false;
            Instance.selectedObjects.Clear();
        }
    }

    private void Update()
    {
        AdjustPanelPos(Instance.UI_characterInfoPanel.isPanelActive, Instance.UI_characterInfoPanel.GetComponent<RectTransform>());
        //AdjustPanelPos(Instance.isWarHandlerPanelActive, Instance.UI_warHandlerPanel);
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
            Vector3 mousePos = Input.mousePosition;
            // Adjusting the panel position to not exceed the screen boundaries.
            float panelHalfWidth = _panel.rect.width * 0.5f;
            float panelHalfHeight = _panel.rect.height * 0.5f;

            float minX = panelHalfWidth;
            float maxX = Screen.width - panelHalfWidth;
            float minY = panelHalfHeight;
            float maxY = Screen.height - panelHalfHeight;

            SetPanelPosWithAnchor(panelPosition, _panel,panelHalfWidth,panelHalfHeight);

            mousePos.x = Mathf.Clamp(mousePos.x, minX, maxX);
            mousePos.y = Mathf.Clamp(mousePos.y, minY, maxY);
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
    public void ActivateCharacterInfoPanel(Character _character)
    {
        Instance.obje = _character.gameObject;
        Instance.UI_characterInfoPanel.UpdatePanel(_character);
        Instance.UI_characterInfoPanel.gameObject.SetActive(true);
        Instance.UI_characterInfoPanel.isPanelActive = true;
    }
    public void DeActivateCharacterInfoPanel()
    {
        Instance.UI_characterInfoPanel.gameObject.SetActive(false);
        Instance.UI_characterInfoPanel.isPanelActive = false;
    }
    public void UpdateWarPanel(GameObject _object, string _time, List<Character> _party1,List<Character> _party2)
    {
        Instance.obje = _object;
        Instance.timeText.text = _time;

        WarHandler _warHandler = _object.GetComponent<WarHandler>();

        Army infoArmy = _warHandler.ReturnPartyArmy(_party1);

        //Party1
        party1_nameText.text = _party1[0].characterName;
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

        infoArmy = _warHandler.ReturnPartyArmy(_party2);
        //Party2
        party2_nameText.text = _party2[0].characterName;
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
    public void ToggleInteractCharacterPanel(bool _isEnemy)
    {
        UI_interactCharacterPanel.TogglePanel(_isEnemy);
    }
    public void ToggleInteractSettlementPanel()
    {
        UI_inSettlementPanel.TogglePanel();
    }
}
