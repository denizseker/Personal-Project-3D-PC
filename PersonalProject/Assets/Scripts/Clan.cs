using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Clan
{
    public string clanName;
    public Sprite clanLogo;
    public List<GameObject> members = new List<GameObject>();
    public List<GameObject> settlements = new List<GameObject>();

    
    public Clan(string _clanName,Sprite _clanLogo)
    {
        clanName = _clanName;
        clanLogo = _clanLogo;
    }


    public void AddMember(GameObject member)
    {
        members.Add(member);
    }
    public void AddSettlement(GameObject settlement)
    {
        settlements.Add(settlement);
    }
}
