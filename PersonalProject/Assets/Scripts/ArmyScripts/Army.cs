using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Army : MonoBehaviour
{
    //Creating default army with 0 troops.
    public List<Soldier> armyList = new List<Soldier>();
    public Soldier PeasentRecruit = new Soldier(20, 2, 25, 0, 0, SoldierLevel.PeasentRecruit);
    public Soldier SwordsMan = new Soldier(35, 5, 75, 0, 25, SoldierLevel.SwordsMan);
    public Soldier HorseMan = new Soldier(60, 8, 225, 0, 75, SoldierLevel.HorseMan);
    public Soldier Cavalary = new Soldier(90, 15, 675, 0, 225, SoldierLevel.Cavalary);
    public Soldier EliteCavalary = new Soldier(150, 25, 2025, 0, 675, SoldierLevel.EliteCavalary);

    private void Awake()
    {
        //Adding troops to army.
        armyList.Add(PeasentRecruit);
        armyList.Add(SwordsMan);
        armyList.Add(HorseMan);
        armyList.Add(Cavalary);
        armyList.Add(EliteCavalary);
    }


    public void BattleReport()
    {

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

    public void GetArmyList()
    {
        Debug.Log("Peasent : " + PeasentRecruit.amount);
        Debug.Log("Swordsman : " + SwordsMan.amount);
        Debug.Log("Horseman : " + HorseMan.amount);
        Debug.Log("Cavalary : " + Cavalary.amount);
        Debug.Log("Elitecavalary : " + EliteCavalary.amount);
    }

    
}
