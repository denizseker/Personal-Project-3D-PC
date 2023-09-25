using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SoldierName", menuName = "NewSoldierSO")]
public class SoldierSO : ScriptableObject
{
    public string soldierName;
    public SoldierLevel soldierLevel;
    public int health;
    public int attack;
    public int expLimit;
    public int amount;
    public int exp;
}
