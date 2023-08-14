using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControllerSoldier : MonoBehaviour
{
    //This values are need when we calculating scale of canvas for easy readable infos.
    private float minScale = 1f;
    private float maxScale = 7f;
    private float minDistance = 50f;
    private float maxDistance = 500f;
    private float dist;
    //Panels
    public GameObject infoPanel;
    public GameObject armySizePanel;
    //UI text and image elements
    [SerializeField] private Text infoSoldierName;
    [SerializeField] private Text littleSoldierName;
    [SerializeField] private Text armySizeValue;
    [SerializeField] private Text clanValue;
    [SerializeField] private Text troopsValue;
    [SerializeField] private Text currentStateValue;
    [SerializeField] private GameObject smallExclamationMark;
    [SerializeField] private GameObject bigExclamationMark;
    [SerializeField] private SpriteRenderer clanLogo;
    //Clan logos
    [SerializeField] private Sprite WUTANG_LOGO;
    [SerializeField] private Sprite V8_LOGO;
    //script for selectable objects.
    private MoveToObject MoveToObject;
    //main script for enemies
    private EnemyController enemyController;
   


    private void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();
        UpdateTextAndScale();
        MoveToObject = GetComponent<MoveToObject>();
    }

    private void Start()
    {
        UpdateTextAndScale();
    }

    //If mouse enter this gameobject
    private void OnMouseEnter()
    {
        UpdateTextAndScale();
        infoPanel.SetActive(true);
        armySizePanel.SetActive(false);
        //if AI is catching us, we are activating exclamationmark.
        if (enemyController.currentState == EnemyController.CurrentState.Catching)
        {
            bigExclamationMark.SetActive(true);
        }
    }
    //if mouse on this object
    private void OnMouseOver()
    {
        UpdateTextAndScale();
        infoPanel.SetActive(true);
        armySizePanel.SetActive(false);
        if (enemyController.currentState == EnemyController.CurrentState.Catching)
        {
            bigExclamationMark.SetActive(true);
        }
    }
    private void Update()
    {
        //if object is selected, updating text and scale always.
        if (MoveToObject.isSelected)
        {
            UpdateTextAndScale();
        }

        //always checking currentstate
        if(enemyController.currentState == EnemyController.CurrentState.Catching)
        {
            smallExclamationMark.SetActive(true);
        }
        else
        {
            smallExclamationMark.SetActive(false);
        }
    }
    //if mouse exit this object
    private void OnMouseExit()
    {
        if (!MoveToObject.isSelected)
        {
            UpdateTextAndScale();
            bigExclamationMark.SetActive(false);
            infoPanel.SetActive(false);
            armySizePanel.SetActive(true);
        }
    }

    //updating text and scale
    void UpdateTextAndScale()
    {
        infoSoldierName.text = enemyController.soldierName.ToString();
        littleSoldierName.text = enemyController.soldierName.ToString();
        armySizeValue.text = enemyController.troops.ToString();
        clanValue.text = enemyController.clan.ToString();
        troopsValue.text = enemyController.troops.ToString();
        currentStateValue.text = enemyController.currentState.ToString();

        if (enemyController.clan == GameManager.Clans.WUTANG)
        {
            clanLogo.sprite = WUTANG_LOGO;
        }
        else if(enemyController.clan == GameManager.Clans.V8)
        {
            clanLogo.sprite = V8_LOGO;
        }


        if(enemyController.currentState == EnemyController.CurrentState.Patroling)
        {
            currentStateValue.text = enemyController.currentState.ToString() + " " + "<color=red><b>" + enemyController.settlement.Name + "</b></color>";
        }
        else if (enemyController.currentState == EnemyController.CurrentState.Catching)
        {
            currentStateValue.text = enemyController.currentState.ToString() + " " + "<color=red><b>" + enemyController.intrectedSoldierName + "</b></color>";
        }
        else if (enemyController.currentState == EnemyController.CurrentState.RunningFrom)
        {
            currentStateValue.text = enemyController.currentState.ToString() + " " + "<color=red><b>" + enemyController.intrectedSoldierName+ "</b></color>";
        }
        //else if (enemyController.currentState == EnemyController.CurrentState.Idle)
        //{

        //}

        dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        var scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(minDistance, maxDistance, dist));
        infoPanel.transform.localScale = new Vector3(scale, scale, scale);
    }
}