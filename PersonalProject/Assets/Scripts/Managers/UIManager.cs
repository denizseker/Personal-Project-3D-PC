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
    public UI_SettlementInfoPanel UI_settlementInfoPanel;
    public UI_InteractCharacterPanel UI_interactCharacterPanel;
    public UI_InSettlementPanel UI_inSettlementPanel;
    //public float offsetXPercentage = 0.07f; // Horizontally
    //public float offsetYPercentage = -0.18f; // Vertically

    private List<RectTransform> activePanels = new List<RectTransform>();

    private GameObject obje;
    [SerializeField] private TMP_Text timeScaleText;
    [SerializeField] private TMP_Text InGameHourText;


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
        AdjustPanelPos(Instance.UI_warInfoPanel.isPanelActive, Instance.UI_warInfoPanel.GetComponent<RectTransform>());
        AdjustPanelPos(Instance.UI_settlementInfoPanel.isPanelActive, Instance.UI_settlementInfoPanel.GetComponent<RectTransform>());
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


    public void UpdateTimeScaleText()
    {
        timeScaleText.text = Time.timeScale + "x";
    }
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
    public void ActivateWarInfoPanel(WarHandler _warHandler)
    {
        Instance.obje = _warHandler.gameObject;
        Instance.UI_warInfoPanel.UpdatePanel(_warHandler);
        Instance.UI_warInfoPanel.gameObject.SetActive(true);
        Instance.UI_warInfoPanel.isPanelActive = true;

    }
    public void DeActivateWarInfoPanel()
    {
        Instance.UI_warInfoPanel.gameObject.SetActive(false);
        Instance.UI_warInfoPanel.isPanelActive = false;
    }
    public void ActivateSettlementInfoPanel(Settlement _settlement)
    {
        Instance.obje = _settlement.gameObject;
        Instance.UI_settlementInfoPanel.UpdatePanel(_settlement);
        Instance.UI_settlementInfoPanel.gameObject.SetActive(true);
        Instance.UI_settlementInfoPanel.isPanelActive = true;
    }
    public void DeActivateSettlementInfoPanel()
    {
        Instance.UI_settlementInfoPanel.gameObject.SetActive(false);
        Instance.UI_settlementInfoPanel.isPanelActive = false;
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
