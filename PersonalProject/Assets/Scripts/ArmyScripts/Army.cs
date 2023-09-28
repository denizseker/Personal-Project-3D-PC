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
        armyTotalTroops = armySize;
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
        //sending damage to every soldier in party
        for (int i = 0; i < armyList.Count; i++)
        {
            //if that soldier have more than 0
            if(armyList[i].amount != 0)
            {
                //calculating killable soldier
                int killedSoldier = _incomingDamage / armyList[i].health;
                //if cannot kill any soldier with incoming damage we are killing 1
                if (killedSoldier == 0)
                {
                    killedSoldier = 1;
                }
                //decreasing the amount of that soldier if its not already lower than 0 
                if ((armyList[i].amount - killedSoldier) > 0) armyList[i].amount -= killedSoldier;
                else armyList[i].amount = 0;
            }

            armyTotalTroops = GetArmySize();
        }
    }

    //Its collecting soldier for army;
    public int RecruitSoldier()
    {
        int addedSoldier = 0;

        for (int i = 0; i < armyList.Count; i++)
        {

            int x = Random.Range(1, 5);
            addedSoldier += x;
            Debug.Log(addedSoldier);
            armyList[i].amount += x;
        }
        armyTotalTroops = GetArmySize();
        return addedSoldier;
    }

    public void FillWithRandomSoldier()
    {
        //calculating min maxx
        int minX = (MinArmySize / armyList.Count);
        int minY = (MaxArmySize / armyList.Count);
        //setting troops amount with min max
        for (int i = 0; i < armyList.Count; i++)
        {
            //High level soldier amount will be slightly lower than low level soldier amount and cannot exceed the amount of the previous one.
            if (i == 0)
            {
                int pickedNumber = Random.Range(minY, minY * 2);
                if (pickedNumber < 0)
                {
                    pickedNumber = 0;
                }
                armyList[i].amount += pickedNumber;
            }
            else
            {
                int pickedNumber = Random.Range(minX - i, armyList[i - 1].amount);
                if (pickedNumber < 0)
                {
                    pickedNumber = 0;
                }
                armyList[i].amount += pickedNumber;
            }
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
    }


    //Creating randomized army troops.
    private void CreateArmy()
    {
        //creating troops instance
        PeasentRecruit = new Soldier(SoldierSO[0].soldierName, SoldierSO[0].health, SoldierSO[0].attack, SoldierSO[0].expLimit, SoldierSO[0].exp,0, SoldierSO[0].soldierLevel);
        SwordsMan = new Soldier(SoldierSO[1].soldierName, SoldierSO[1].health, SoldierSO[1].attack, SoldierSO[1].expLimit, SoldierSO[1].exp, 0, SoldierSO[1].soldierLevel);
        HorseMan = new Soldier(SoldierSO[2].soldierName, SoldierSO[2].health, SoldierSO[2].attack, SoldierSO[2].expLimit, SoldierSO[2].exp, 0, SoldierSO[2].soldierLevel);
        Cavalary = new Soldier(SoldierSO[3].soldierName, SoldierSO[3].health, SoldierSO[3].attack, SoldierSO[3].expLimit, SoldierSO[3].exp, 0, SoldierSO[3].soldierLevel);
        EliteCavalary = new Soldier(SoldierSO[4].soldierName, SoldierSO[4].health, SoldierSO[4].attack, SoldierSO[4].expLimit, SoldierSO[4].exp, 0, SoldierSO[4].soldierLevel);
        //adding those instance to armyList
        armyList.Add(PeasentRecruit);
        armyList.Add(SwordsMan);
        armyList.Add(HorseMan);
        armyList.Add(Cavalary);
        armyList.Add(EliteCavalary);
        FillWithRandomSoldier();
        armyTotalTroops = GetArmySize();
    }
}
