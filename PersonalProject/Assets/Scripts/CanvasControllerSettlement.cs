using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CanvasControllerSettlement : MonoBehaviour
{
    private Settlement settlement;
    [SerializeField] private Text titleValue;
    [SerializeField] private Text rulerValue;
    [SerializeField] private Text powerValue;
    [SerializeField] private Text wallValue;
    [SerializeField] private Text economyValue;
    [SerializeField] private Text defendersValue;
    public GameObject InfoPanel;
    private MoveToObject moveToObject;


    private float minScale = 1f;
    private float maxScale = 10f;
    private float minDistance = 50f;
    private float maxDistance = 500f;
    private float dist;

    private void Awake()
    {
        
        moveToObject = GetComponent<MoveToObject>();
        settlement = GetComponentInParent<Settlement>();
        UpdateTextAndScale();
    }

    private void OnMouseEnter()
    {
        UpdateTextAndScale();
        InfoPanel.SetActive(true);
        
    }
    //Mouse þehrin üzerindeyse
    private void OnMouseOver()
    {
        UpdateTextAndScale();
        InfoPanel.SetActive(true);
    }

    private void Update()
    {
        if (moveToObject.isSelected)
        {
            UpdateTextAndScale();
        }
    }
    private void OnMouseExit()
    {
        if (!moveToObject.isSelected)
        {
            InfoPanel.SetActive(false);
        }
        
    }
    private void UpdateTextAndScale()
    {
        titleValue.text = settlement.settlementName;
        rulerValue.text = settlement.clan.ToString();
        powerValue.text = settlement.Power.ToString();
        wallValue.text = settlement.WallLevel.ToString();
        economyValue.text = settlement.Economy;
        defendersValue.text = settlement.Defenders.ToString();
        dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        var scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(minDistance, maxDistance, dist));
        InfoPanel.transform.localScale = new Vector3(scale, scale, scale);
    }

}