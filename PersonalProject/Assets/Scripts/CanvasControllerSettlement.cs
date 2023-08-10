using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CanvasControllerSettlement : MonoBehaviour
{
    [SerializeField] private BaseSettlement townBase;
    [SerializeField] private Text titleValue;
    [SerializeField] private Text rulerValue;
    [SerializeField] private Text powerValue;
    [SerializeField] private Text wallValue;
    [SerializeField] private Text economyValue;
    [SerializeField] private Text defendersValue;
    public GameObject InfoPanel;
    private NavMeshAgent playerAgent;
    private MoveToObject moveToObject;


    private float minScale = 1f;
    private float maxScale = 3f;
    private float minDistance = 50f;
    private float maxDistance = 150f;
    private float dist;

    private void Awake()
    {
        UpdateTextAndScale();
        moveToObject = GetComponent<MoveToObject>();
        playerAgent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
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
        titleValue.text = townBase.Name;
        rulerValue.text = townBase.RullerName;
        powerValue.text = townBase.Power.ToString();
        wallValue.text = townBase.WallLevel.ToString();
        economyValue.text = townBase.Economy;
        defendersValue.text = townBase.Defenders.ToString();
        dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        var scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(minDistance, maxDistance, dist));
        InfoPanel.transform.localScale = new Vector3(scale, scale, scale);
    }

}