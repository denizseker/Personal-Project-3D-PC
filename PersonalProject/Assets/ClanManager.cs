using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanManager : MonoBehaviour
{
    //Clan enums for dropdown list at inspector for easy chose soldier's clan.
    public enum ENUM_Clan
    {
        SHUNEM,
        WUTANG,
        APHALUX,
        DARTRONG,
        SOLVENNA,
        VALANDOR,
        BARBARIAN
    }

    public static ClanManager Instance;
    [Header("ClanList")]
    public List<Clan> clanList = new List<Clan>();
    [Header("Clans")]

    //Creating clans
    public Clan Shunem = new Clan("SHUNEM",null);
    public Clan Wutang = new Clan("WUTANG", null);
    public Clan Aphalux = new Clan("APHALUX", null);
    public Clan Dartrong = new Clan("DARTRONG", null);
    public Clan Solvenna = new Clan("SOLVENNA", null);
    public Clan Valandor = new Clan("VALANDOR", null);
    public Clan Barbarian = new Clan("BARBARIAN", null);
    private void Awake()
    {
        Instance = this;

        //Adding clans to the clan list.
        Instance.clanList.Add(Shunem);
        Instance.clanList.Add(Wutang);
        Instance.clanList.Add(Aphalux);
        Instance.clanList.Add(Dartrong);
        Instance.clanList.Add(Solvenna);
        Instance.clanList.Add(Valandor);
        Instance.clanList.Add(Barbarian);
    }
}


