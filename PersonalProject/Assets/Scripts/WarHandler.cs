using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarHandler : MonoBehaviour
{
    public List<Character> party1 = new List<Character>();
    public List<Character> party2 = new List<Character>();
    public Army partyArmyHolder;
    public bool isBattleStarted = false;
    public float pastTime = 0;
    public float startTime;
    public string pastTimeString;

    private WaitForSeconds attackFrequency = new WaitForSeconds(3);

    // Update is called once per frame
    void Update()
    {
        //Calculating past time and making it string.
        if (isBattleStarted)
        {
            pastTime = Time.time - startTime;
            pastTimeString = Mathf.Floor(pastTime / 60).ToString("00") + ":" + Mathf.FloorToInt(pastTime % 60).ToString("00");
        }
    }

    private void Awake()
    {
        partyArmyHolder = GetComponent<Army>();
    }

    private IEnumerator WarGoingOn()
    {
        int round = 1; //round counter
        while (true)
        {
            yield return attackFrequency;
            Attack(round);
            round += 1;

            //Debug.Log("Party1 :" + CheckPartyTroops(party1) + " Party2 :" +CheckPartyTroops(party2));

            //checking if any army lose
            if (ReturnPartyTroops(party1) <= 0)
            {
                CloseWar(party2,party1);
                break;
            }
            else if (ReturnPartyTroops(party2) <= 0)
            {
                CloseWar(party1,party2);
                break;
            }
        }
    }

    private int ReturnPartyTroops(List<Character> _party)
    {
        int totalTroops = 0;

        for (int i = 0; i < _party.Count; i++)
        {
            totalTroops += _party[i].army.armyTotalTroops;
        }
        return totalTroops;
    }

    //Checking if any party have enemy
    public bool CanJoinWar(Clan _clan)
    {
        bool canJoinWar = false;

        for (int i = 0; i < party1.Count; i++)
        {
            canJoinWar = ClanManager.Instance.IsEnemy(_clan,party1[i].clan);
        }
        for (int i = 0; i < party2.Count; i++)
        {
            canJoinWar = ClanManager.Instance.IsEnemy(_clan, party2[i].clan);
        }

        return canJoinWar;
    }

    private int ReturnPartyPower(List<Character> _party)
    {
        int partyPower = 0;

        for (int i = 0; i < _party.Count; i++)
        {
            partyPower += _party[i].army.GetArmyPower();
        }

        return partyPower;
    }

    private void Attack(int _round)
    {
        //Armies power are multiplying with round
        int currentPowerParty1 = (ReturnPartyPower(party1)/5) * _round;
        int currentPowerParty2 = (ReturnPartyPower(party2)/5) * _round;

        //Debug.Log($"Ordu1 Güç={currentPowerParty1} | Ordu2 Güç={currentPowerParty2}");
        //Debug.Log($"Ordu1 Can={character1.army.GetArmyHealth()} | Ordu2 Can={character2.army.GetArmyHealth()}");

        //Sending armies power as an incoming damage so the army script take damage function can handle it for splitting to soldiers
        SplitAttack(party1, currentPowerParty2);
        SplitAttack(party2, currentPowerParty1);
    }

    //This function handle party attacks
    private void SplitAttack(List<Character> _party,int _incomingDamage)
    {
        for (int i = 0; i < _party.Count; i++)
        {
            _party[i].army.TakeDamage(_incomingDamage/_party.Count);
        }
    }

    //Merging party armies to partyArmyHolder. Using when sending stats to UIManager
    public Army ReturnPartyArmy(List<Character> _party)
    {
        partyArmyHolder.ClearArmy();

        for (int i = 0; i < _party.Count; i++)
        {
            for (int j = 0; j < partyArmyHolder.armyList.Count; j++)
            {
                partyArmyHolder.armyList[j].amount += _party[i].army.armyList[j].amount;
            }
        }
        partyArmyHolder.GetArmySize();
        return partyArmyHolder;
    }

    //This function is calling from NPCAI script. Initializing battle start variables.
    public void StartFight(Character _character1,Character _character2)
    {
        startTime = Time.time;
        isBattleStarted = true;
        party1.Add(_character1);
        party2.Add(_character2);

        StartCoroutine(WarGoingOn());
    }

    //Taking winner and loser character and setting their values
    public void CloseWar(List<Character> _winnerParty,List<Character> _loserParty)
    {
        SetWinnerParty(_winnerParty);
        SetLoserParty(_loserParty);
        isBattleStarted = false;
        Destroy(gameObject);
    }

    private void SetWinnerParty(List<Character> _party)
    {
        for (int i = 0; i < _party.Count; i++)
        {
            _party[i].ResetTarget();
            _party[i].currentState = Character.State.Patroling;
            _party[i].ChangeColliderState();
        }
    }
    private void SetLoserParty(List<Character> _party)
    {
        for (int i = 0; i < _party.Count; i++)
        {
            _party[i].ResetTarget();
            _party[i].currentState = Character.State.Defeated;
            _party[i].ChangeColliderState();
        }
    }
    public void AddCharacterToWar(Character _character)
    {
        for (int i = 0; i < party1.Count; i++)
        {
            if(ClanManager.Instance.IsEnemy(_character.clan, party1[i].clan))
            {
                party2.Add(_character);
                return;
            }
        }
        for (int i = 0; i < party2.Count; i++)
        {
            if(ClanManager.Instance.IsEnemy(_character.clan, party2[i].clan))
            {
                party1.Add(_character);
                return;
            }
        }
    }
}
