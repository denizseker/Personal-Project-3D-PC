using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarHandler : MonoBehaviour
{
    //Those variables will add for for bigger army fights
    //private List<Character> party1 = new List<Character>();
    //private List<Character> party2 = new List<Character>();
    public Character character1;
    public Character character2;
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

    private IEnumerator WarGoingOn()
    {
        int round = 1; //round counter
        while (true)
        {
            yield return attackFrequency;
            Attack(round);
            round += 1;

            //checking if any army lose
            if(character1.army.armyTotalTroops <= 0)
            {
                CloseWar(character2,character1);
                break;
            }
            else if (character2.army.armyTotalTroops <= 0)
            {
                CloseWar(character1,character2);
                break;
            }
        }
    }

    private void Attack(int _round)
    {
        //Armies power are multiplying with round
        int currentPowerParty1 = (character1.army.GetArmyPower()/5) * _round;
        int currentPowerParty2 = (character2.army.GetArmyPower()/5) * _round;


        //Debug.Log($"Ordu1 Güç={currentPowerParty1} | Ordu2 Güç={currentPowerParty2}");
        //Debug.Log($"Ordu1 Can={character1.army.GetArmyHealth()} | Ordu2 Can={character2.army.GetArmyHealth()}");

        //Sending armies power as a incoming damage so the army script take damage function can handle it for splitting to soldiers
        character1.army.TakeDamage(currentPowerParty2);
        character2.army.TakeDamage(currentPowerParty1);
    }

    //This function is calling from NPCAI script. Initializing battle start variables.
    public void StartFight(Character _character1,Character _character2)
    {
        startTime = Time.time;
        isBattleStarted = true;
        character1 = _character1;
        character2 = _character2;
        StartCoroutine(WarGoingOn());
    }

    //Taking winner and loser character and setting their values
    public void CloseWar(Character _winner,Character _loser)
    {
        _winner.currentState = Character.CurrentState.Patroling;
        _loser.currentState = Character.CurrentState.Defeated;
        _winner.ChangeColliderState();
        _loser.ChangeColliderState();
        _winner.ResetTarget();
        _loser.ResetTarget();
        isBattleStarted = false;
        Destroy(gameObject);
    }
}
