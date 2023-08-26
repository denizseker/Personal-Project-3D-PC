using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoldierLevel
{
    PeasentRecruit,
    SwordsMan,
    HorseMan,
    Cavalary,
    EliteCavalary,
}

[System.Serializable]
public class Soldier
{
    public int health;
    public int attack;
    public int expLimit;
    public int amount;
    public int exp;
    public SoldierLevel soldierLevel;

    //Instantiating at army script with default values
    public Soldier(int _health,int _attack,int _expLimit,int _amount,int _exp,SoldierLevel _soldierLevel)
    {
        health = _health;
        attack = _attack;
        expLimit = _expLimit;
        amount = _amount;
        exp = _exp;
        soldierLevel = _soldierLevel;
    }

    //calculate how many units will level up with inComingExp and return leveledUpUnit count.
    public int ShareExp(int _inComingExp)
    {
        int expNeedForLevelUp = expLimit - exp;

        int leveledUpUnit = _inComingExp / expNeedForLevelUp;
        
        //if inComingExp was too much all unit will levelup.
        if(leveledUpUnit > amount)
        {
            return amount;
        }
        else
        {
            return leveledUpUnit;
        }  
    }


}
