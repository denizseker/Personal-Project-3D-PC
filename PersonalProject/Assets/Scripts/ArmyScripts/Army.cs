using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Army : MonoBehaviour
{
    //Creating default army with 0 troops.
    [Header("Army")]
    public int MinArmySize;
    public int MaxArmySize;
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
        //creating troops instance
        PeasentRecruit = new Soldier(SoldierSO[0].health, SoldierSO[0].attack, SoldierSO[0].expLimit, SoldierSO[0].exp,0, SoldierSO[0].soldierLevel);
        SwordsMan = new Soldier(SoldierSO[1].health, SoldierSO[1].attack, SoldierSO[1].expLimit, SoldierSO[1].exp, 0, SoldierSO[1].soldierLevel);
        HorseMan = new Soldier(SoldierSO[2].health, SoldierSO[2].attack, SoldierSO[2].expLimit, SoldierSO[2].exp, 0, SoldierSO[2].soldierLevel);
        Cavalary = new Soldier(SoldierSO[3].health, SoldierSO[3].attack, SoldierSO[3].expLimit, SoldierSO[3].exp, 0, SoldierSO[3].soldierLevel);
        EliteCavalary = new Soldier(SoldierSO[4].health, SoldierSO[4].attack, SoldierSO[4].expLimit, SoldierSO[4].exp, 0, SoldierSO[4].soldierLevel);
        //adding those instance to armyList
        armyList.Add(PeasentRecruit);
        armyList.Add(SwordsMan);
        armyList.Add(HorseMan);
        armyList.Add(Cavalary);
        armyList.Add(EliteCavalary);
        //calculating min maxx
        int minX = (MinArmySize / armyList.Count);
        int minY = (MaxArmySize / armyList.Count);
        //setting troops amount with min max
        for (int i = 0; i < armyList.Count; i++)
        {
            armyList[i].amount = Random.Range(minX-i, minY-i);
        }
        //getting armysize
        int currentArmySize = GetArmySize();
        //Checking armysizeminmax with currentarmysize and adjusting with lowest level soldier
        if (currentArmySize < MinArmySize)
        {
            int addSoldier = MinArmySize - currentArmySize;
            PeasentRecruit.amount += addSoldier;
        }
        currentArmySize = GetArmySize();
        if (currentArmySize > MaxArmySize)
        {
            int removeSoldier = currentArmySize - MaxArmySize;
            PeasentRecruit.amount -= removeSoldier;
        }
        armyTotalTroops = GetArmySize();
    }
}
