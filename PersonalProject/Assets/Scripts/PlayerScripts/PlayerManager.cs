using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private Army army;
    public Clan clan;
    public string playerName = "Sir Eternal";



    [HideInInspector] public GameObject targetSoldier;
    [HideInInspector] public NPCManager.CurrentState currentState = NPCManager.CurrentState.Idle;
    


    private void Awake()
    {
        army = GetComponent<Army>();
        clan = ClanManager.Instance.Barbarian;
    }



    // Update is called once per frame
    void Update()
    {

    }
}
