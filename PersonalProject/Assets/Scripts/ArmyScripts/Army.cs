using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Army : MonoBehaviour
{
    //Creating default army with 0 troops.
    [Header("Army")]
    [HideInInspector] public List<Soldier> armyList = new List<Soldier>();
    [Header("SoldiersInArmy")]
    public Soldier PeasentRecruit = new Soldier(30, 1, 25, 0, 0, SoldierLevel.PeasentRecruit);
    public Soldier SwordsMan = new Soldier(60, 2, 75, 0, 25, SoldierLevel.SwordsMan);
    public Soldier HorseMan = new Soldier(120, 5, 225, 0, 75, SoldierLevel.HorseMan);
    public Soldier Cavalary = new Soldier(250, 10, 675, 0, 225, SoldierLevel.Cavalary);
    public Soldier EliteCavalary = new Soldier(550, 15, 2025, 0, 675, SoldierLevel.EliteCavalary);


    public List<ScriptableObject> SoldierSO = new List<ScriptableObject>();


    [HideInInspector] public int armyTotalTroops;

    private void Awake()
    {
        //Adding troops to army.
        armyList.Add(PeasentRecruit);
        armyList.Add(SwordsMan);
        armyList.Add(HorseMan);
        armyList.Add(Cavalary);
        armyList.Add(EliteCavalary);
        armyTotalTroops = GetArmySize();
    }

    public int GetArmySize()
    {
        int armySize = 0;

        for (int i = 0; i < armyList.Count; i++)
        {
            armySize += armyList[i].amount;
        }
        return armySize;
    }
    public int GetArmyPower()
    {
        int armyPower = 0;

        for (int i = 0; i < armyList.Count; i++)
        {
            armyPower += armyList[i].amount * armyList[i].attack;
        }
        return armyPower;
    }
    public int GetArmyHealth()
    {
        int armyHealth = 0;

        for (int i = 0; i < armyList.Count; i++)
        {
            armyHealth += armyList[i].amount * armyList[i].health;
        }
        return armyHealth;
    }

    public void TakeDamage(int _incomingDamage)
    {
        for (int i = 0; i < armyList.Count; i++)
        {
            if(armyList[i].amount > 0)
            {
                //Debug.Log("Damage yedi : " + armyList[i].soldierLevel);

                //Calculating how many soldier will die after that damage
                int diedSoldier = _incomingDamage / armyList[i].health;
                //Killing soldier and decreasing from armytroop
                armyList[i].amount -= diedSoldier;
                //if that soldier type negative, setting to 0
                if(armyList[i].amount < 0)
                {
                    armyList[i].amount = 0;
                }
            }
        }
    }


    public void GetArmyList()
    {
        Debug.Log("Peasent : " + PeasentRecruit.amount);
        Debug.Log("Swordsman : " + SwordsMan.amount);
        Debug.Log("Horseman : " + HorseMan.amount);
        Debug.Log("Cavalary : " + Cavalary.amount);
        Debug.Log("Elitecavalary : " + EliteCavalary.amount);

    }
}
