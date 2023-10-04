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
        Instance.clanList.Add(Instance.Shunem);
        Instance.clanList.Add(Instance.Wutang);
        Instance.clanList.Add(Instance.Aphalux);
        Instance.clanList.Add(Instance.Dartrong);
        Instance.clanList.Add(Instance.Solvenna);
        Instance.clanList.Add(Instance.Valandor);
        Instance.clanList.Add(Instance.Barbarian);
        Instance.clanList.Add(Instance.None);
    }
    private void Start()
    {
        DeclareWar(Instance.Wutang, Instance.Dartrong);
        DeclareWar(Instance.Shunem, Instance.Valandor);
        DeclareWar(Instance.Aphalux, Instance.Solvenna);

        DeclareWar(Instance.Wutang, Instance.Barbarian);
        DeclareWar(Instance.Dartrong, Instance.Barbarian);
        DeclareWar(Instance.Shunem, Instance.Barbarian);
        DeclareWar(Instance.Valandor, Instance.Barbarian);
        DeclareWar(Instance.Aphalux, Instance.Barbarian);
        DeclareWar(Instance.Solvenna, Instance.Barbarian);
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


