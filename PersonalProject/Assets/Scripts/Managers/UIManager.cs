using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public RectTransform UI_soldierPanel;
    public float offsetXPercentage = 0.07f; // Horizontally
    public float offsetYPercentage = -0.18f; // Vertically
    public bool isPanelActive = false;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text clanText;
    [SerializeField] private TMP_Text troopsText;
    [SerializeField] private TMP_Text peasentRecruitText;
    [SerializeField] private TMP_Text swordsManText;
    [SerializeField] private TMP_Text horseManText;
    [SerializeField] private TMP_Text cavalaryText;
    [SerializeField] private TMP_Text eliteCavalaryText;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Instance.isPanelActive)
        {
            Vector3 panelPosition = Input.mousePosition;

            //Adjusting panel position to mouse position for different resolation
            float offsetWidth = Screen.width * Instance.offsetXPercentage;
            float offsetHeight = Screen.height * Instance.offsetYPercentage;

            panelPosition.x += offsetWidth;
            panelPosition.y += offsetHeight;

            // Adjusting the panel position to not exceed the screen boundaries.
            float panelHalfWidth = Instance.UI_soldierPanel.sizeDelta.x * 0.4f;
            float panelHalfHeight = Instance.UI_soldierPanel.sizeDelta.y * 0.4f;

            float minX = panelHalfWidth;
            float maxX = Screen.width - panelHalfWidth;
            float minY = panelHalfHeight;
            float maxY = Screen.height - panelHalfHeight;

            panelPosition.x = Mathf.Clamp(panelPosition.x, minX, maxX);
            panelPosition.y = Mathf.Clamp(panelPosition.y, minY, maxY);

            Instance.UI_soldierPanel.position = panelPosition;
        }

    }


    public void UpdateInfoPanel(string _name,GameManager.Clans _clan,int _troops,int _peasentrecruit, int _swordsman, int _horseman, int _cavalary, int _elitecavalary)
    {
        Instance.titleText.text = _name;
        Instance.clanText.text = _clan.ToString();
        Instance.troopsText.text = "(" + _troops.ToString() + ")";
        Instance.peasentRecruitText.text = _peasentrecruit.ToString();
        Instance.swordsManText.text = _swordsman.ToString();
        Instance.horseManText.text = _horseman.ToString();
        Instance.cavalaryText.text = _cavalary.ToString();
        Instance.eliteCavalaryText.text = _elitecavalary.ToString();

    }


}
