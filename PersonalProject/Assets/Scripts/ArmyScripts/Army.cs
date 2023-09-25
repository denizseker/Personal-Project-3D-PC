using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Army : MonoBehaviour
{
    //Creating default army with 0 troops.
    [Header("Army")]
    [HideInInspector] public List<Soldier> armyList = new List<Soldier>();
    public Soldier PeasentRecruit;
    public Soldier SwordsMan;
    public Soldier HorseMan;
    public Soldier Cavalary;
    public Soldier EliteCavalary;


    public List<SoldierSO> SoldierSO = new List<SoldierSO>();


    [HideInInspector] public int armyTotalTroops;

    private void Awake()
    {
        //Adding troops to army.
        CreateArmy();
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
            if (armyList[i].amount > 0)
            {
                //Debug.Log("Damage yedi : " + armyList[i].soldierLevel);

                //Calculating how many soldier will die after that damage
                int diedSoldier = _incomingDamage / armyList[i].health;
                //Killing soldier and decreasing from armytroop
                armyList[i].amount -= diedSoldier;
                //if that soldier type negative, setting to 0
                if (armyList[i].amount < 0)
                {
                    armyList[i].amount = 0;
                }
            }
        }
    }

    private void CreateArmy()
    {
        PeasentRecruit = new Soldier(30, 1, 25, 0, 0, SoldierLevel.PeasentRecruit);
        SwordsMan = new Soldier(60, 2, 75, 25, 0, SoldierLevel.SwordsMan);
        HorseMan = new Soldier(120, 5, 225, 75, 0, SoldierLevel.HorseMan);
        Cavalary = new Soldier(250, 10, 675, 225, 0, SoldierLevel.Cavalary);
        EliteCavalary = new Soldier(550, 15, 2025, 0, 0, SoldierLevel.EliteCavalary);

        armyList.Add(PeasentRecruit);
        armyList.Add(SwordsMan);
        armyList.Add(HorseMan);
        armyList.Add(Cavalary);
        armyList.Add(EliteCavalary);
        armyTotalTroops = GetArmySize();

    }
}
