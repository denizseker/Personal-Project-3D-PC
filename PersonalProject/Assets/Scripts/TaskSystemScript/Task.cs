using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface Task
{
    NPC NPC { get; set; }
    GameObject TargetObject { get; set; }
    Character.State CharacterState { get; set; }
    public void ExecuteTask();

}
public class Chase : Task
{
    public NPC NPC { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public GameObject TargetObject { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Character.State CharacterState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Chase(NPC _npc,GameObject _targetObject, Character.State _characterState)
    {
        NPC = _npc;
        TargetObject = _targetObject;
        CharacterState = _characterState;
    }

    public void ExecuteTask()
    {
        Debug.Log(NPC.gameObject);
    }

}
