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
        while (true)
        {
            //GetArmiesPower();
            yield return attackFrequency;
        }
    }

    private void GetArmiesPower()
    {
        Debug.Log($"Ordu1 Güç={character1.army.GetArmyPower()} | Ordu2 Güç={character2.army.GetArmyPower()}");
        Debug.Log($"Ordu1 Can={character1.army.GetArmyHealth()} | Ordu2 Can={character2.army.GetArmyHealth()}");

        int currentPowerParty1 = character1.army.GetArmyPower();
        int currentPowerParty2 = character2.army.GetArmyPower();

        character1.army.TakeDamage(currentPowerParty2);
        character2.army.TakeDamage(currentPowerParty1);
    }


    public void StartFight(Character _character1,Character _character2)
    {
        startTime = Time.time;
        isBattleStarted = true;
        character1 = _character1;
        character2 = _character2;
        StartCoroutine(WarGoingOn());
    }


}
