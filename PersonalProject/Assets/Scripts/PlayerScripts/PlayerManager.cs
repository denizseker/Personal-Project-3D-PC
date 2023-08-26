using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int troops;
    private Army army;

    public string playerName = "Sir Eternal";
    public GameManager.Clans clan = GameManager.Clans.None;


    private void Awake()
    {
        army = GetComponent<Army>();
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
