using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{

    public string Name;
    public GameManager.Clans RullerClan;
    public int Defenders;
    public int Power;
    public string Economy;
    public int WallLevel;
    public bool isHavePatrol;

    private void Start()
    {
        GameManager.Instance.Settlements.Add(gameObject);
        
    }


}
