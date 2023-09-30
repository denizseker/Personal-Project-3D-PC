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
        BARBARIAN,
        NONE
    }

    public static ClanManager Instance;
    [Header("ClanList")]
    public List<Clan> clanList = new List<Clan>();
    [Header("Clans")]
    //Creating clans
    //public Clan Shunem = new Clan("SHUNEM",null);
    //public Clan Wutang = new Clan("WUTANG", null);
    //public Clan Aphalux = new Clan("APHALUX", null);
    //public Clan Dartrong = new Clan("DARTRONG", null);
    //public Clan Solvenna = new Clan("SOLVENNA", null);
    //public Clan Valandor = new Clan("VALANDOR", null);
    //public Clan Barbarian = new Clan("BARBARIAN", null);

    public Clan Shunem = new Clan("SHUNEM",null);
    public Clan Wutang = new Clan("WUTANG", null);
    public Clan Aphalux = new Clan("APHALUX", null);
    public Clan Dartrong = new Clan("DARTRONG", null);
    public Clan Solvenna = new Clan("SOLVENNA", null);
    public Clan Valandor = new Clan("VALANDOR", null);
    public Clan Barbarian = new Clan("BARBARIAN", null);
    public Clan None = new Clan("NONE", null);

    public Sprite ShunemLogo;
    public Sprite WutangLogo;
    public Sprite AphaluxLogo;
    public Sprite DartrongLogo;
    public Sprite SolvennaLogo;
    public Sprite ValandorLogo;
    public Sprite BarbarianLogo;
    public Sprite NoneLogo;

    private void Awake()
    {
        Instance = this;
        //Logo
        SetLogo();
        //Adding clans to the clan list.
        Instance.clanList.Add(Shunem);
        Instance.clanList.Add(Wutang);
        Instance.clanList.Add(Aphalux);
        Instance.clanList.Add(Dartrong);
        Instance.clanList.Add(Solvenna);
        Instance.clanList.Add(Valandor);
        Instance.clanList.Add(Barbarian);
        Instance.clanList.Add(None);
    }
    private void Start()
    {
        DeclareWar(Wutang, Dartrong);
        DeclareWar(Shunem, Valandor);
        DeclareWar(Aphalux, Solvenna);

        DeclareWar(Wutang, Barbarian);
        DeclareWar(Dartrong, Barbarian);
        DeclareWar(Shunem, Barbarian);
        DeclareWar(Valandor, Barbarian);
        DeclareWar(Aphalux, Barbarian);
        DeclareWar(Solvenna, Barbarian);
    }

    private void SetLogo()
    {
        Shunem.clanLogo = ShunemLogo;
        Wutang.clanLogo = WutangLogo;
        Aphalux.clanLogo = AphaluxLogo;
        Dartrong.clanLogo = DartrongLogo;
        Solvenna.clanLogo = SolvennaLogo;
        Valandor.clanLogo = ValandorLogo;
        Barbarian.clanLogo = BarbarianLogo;
        None.clanLogo = NoneLogo;
    }

    //Looking for clan in enemy clans, and returning found or not.
    private void MakePeace(Clan _clan1,Clan _clan2)
    {
        for (int i = 0; i < _clan1.enemies.Count; i++)
        {
            if (_clan1.enemies[i] == _clan2)
            {
                _clan1.enemies.RemoveAt(i);
                break;
            }
        }
        for (int i = 0; i < _clan2.enemies.Count; i++)
        {
            if (_clan2.enemies[i] == _clan1)
            {
                _clan2.enemies.RemoveAt(i);
                break;
            }
        }
    }

    private void DeclareWar(Clan _clan1, Clan _clan2)
    {
        bool canAdd = true;

        for (int i = 0; i < _clan1.enemies.Count; i++)
        {
            if (_clan1.enemies[i] == _clan2)
            {
                canAdd = false;
                break;
            } 
        }
        if (canAdd)
        {
            _clan1.enemies.Add(_clan2);
            _clan2.enemies.Add(_clan1);
        }
    }

    public bool isEnemy(Clan _clan1,Clan _clan2)
    {
        bool isEnemy = false;

        for (int i = 0; i < _clan1.enemies.Count; i++)
        {
            if (_clan1.enemies[i] == _clan2)
            {
                isEnemy = true;
                break;
            }
        }
        return isEnemy;
    }

}


