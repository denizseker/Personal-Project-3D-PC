using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{

    public string settlementName;
    public int manPower;
    public int manPowerLimit;
    public ClanManager.ENUM_Clan enumClan;
    [HideInInspector] public Clan clan;
    [HideInInspector] public bool isHavePatrol;
    [HideInInspector] public bool startCollectManPower;
    [HideInInspector] public bool isStartedCollectManPower;
    public List<GameObject> characterInTown = new List<GameObject>();

    private void Awake()
    {
        startCollectManPower = true;
        GetClanWithEnum();
        gameObject.name = (string.Format("[{0}] [{1}]", clan.clanName, settlementName));
        clan.AddSettlement(gameObject);
    }

    private void GetClanWithEnum()
    {
        if (enumClan == ClanManager.ENUM_Clan.APHALUX) clan = ClanManager.Instance.Aphalux;
        else if (enumClan == ClanManager.ENUM_Clan.DARTRONG) clan = ClanManager.Instance.Dartrong;
        else if (enumClan == ClanManager.ENUM_Clan.SHUNEM) clan = ClanManager.Instance.Shunem;
        else if (enumClan == ClanManager.ENUM_Clan.SOLVENNA) clan = ClanManager.Instance.Solvenna;
        else if (enumClan == ClanManager.ENUM_Clan.VALANDOR) clan = ClanManager.Instance.Valandor;
        else if (enumClan == ClanManager.ENUM_Clan.WUTANG) clan = ClanManager.Instance.Wutang;
    }

    //Fucting increasing manpower
    public void IncreaseManPower(int _amount)
    {
        manPower += _amount;
        if(manPower > manPowerLimit)
        {
            manPower = manPowerLimit;
        }
    }
    //Fucting decreasing manpower
    public void DecreaseManPower(int _amount)
    {
        manPower -= _amount;
        if(manPower < 0)
        {
            manPower = 0;
        }
        startCollectManPower = true;
    }

    //Handling town manPower 
    private IEnumerator GetManPower()
    {
        while (true)
        {
            yield return new WaitForSeconds(96);
            IncreaseManPower(10);
            if (manPower >= manPowerLimit)
            {
                manPower = manPowerLimit;
                isStartedCollectManPower = false;
                break;
            }
        }
    }

    //Adding characters gameobject to list
    public void AddCharacter(GameObject _character)
    {
        //Adding character to settlement character list.
        characterInTown.Add(_character);
        //teleporting character to settlement town
        _character.transform.position = GetComponentInChildren<GetCharacterInSettlement>().transform.position;
        //setting character for town
        _character.GetComponent<Character>().EnterSettlement();
    }
    //Removing characters gameobject from list
    public void RemoveCharacter(GameObject _character)
    {
        for (int i = 0; i < characterInTown.Count; i++)
        {
            if(characterInTown[i] == _character)
            {
                characterInTown.RemoveAt(i);
            }
        }
    }

    private void Update()
    {
        //Starting collecting manpower
        if(startCollectManPower && !isStartedCollectManPower)
        {
            //Corotutine handle manpower
            StartCoroutine(GetManPower());
            isStartedCollectManPower = true;
            startCollectManPower = false;
        }
    }
}
