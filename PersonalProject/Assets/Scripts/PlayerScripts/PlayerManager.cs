using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int troops;
    private Army army;

    public string playerName = "Sir Eternal";
    [HideInInspector] public Clan clan;


    private void Awake()
    {
        army = GetComponent<Army>();
        clan = ClanManager.Instance.Barbarian;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {

            Debug.Log(army.PeasentRecruit.ShareExp(300));
            
        }
    }
}
