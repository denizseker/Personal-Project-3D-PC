using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Created,
    Queued,
    Running,
    Stopped,
    Waiting,
    Done,
}

public interface ITask
{
    NPC NPC { get; set; }
    State TaskState { get; set; }
    GameObject TargetObject { get; set; }
    Character.State CharacterState { get; set; }
    public void ExecuteTask();
    public void StopTask();
    public void ContinueTask();
    public void CreateTask(NPC _npc, GameObject _targetObject, Character.State _characterState);
    public void DeleteTask();

}
public class GoToTarget : ITask
{
    public NPC NPC { get; set; }
    public GameObject TargetObject { get; set; }
    public Character.State CharacterState { get; set; }
    public State TaskState { get; set; }

    public GoToTarget(NPC _npc,GameObject _targetObject, Character.State _characterState)
    {
        CreateTask(_npc, _targetObject, _characterState);
    }

    public void ExecuteTask()
    {
        //Target is town
        if(TargetObject.GetComponent<Settlement>() != null)
        {
            //town door position
            Vector3 doorPosition = NPC.town.GetComponentInChildren<GetCharacterInSettlement>().transform.position;
            NPC.agent.SetDestination(doorPosition);
        }
        //Target not town
        else
        {
            NPC.agent.SetDestination(TargetObject.transform.position);

            if (NPC.agent.remainingDistance < 3f && !NPC.agent.pathPending)
            {
                NPCAI _npcAi = NPC.GetComponent<NPCAI>();
                _npcAi.InteractWithCharacter(TargetObject);
                DeleteTask();
            }
        }
    }

    public void StopTask()
    {
        throw new System.NotImplementedException();
    }

    public void ContinueTask()
    {
        throw new System.NotImplementedException();
    }

    public void CreateTask(NPC _npc, GameObject _targetObject, Character.State _characterState)
    {
        NPC = _npc;
        TargetObject = _targetObject;
        CharacterState = _characterState;
        TaskState = State.Created;
        //NPC.taskList.Add(this);
        TaskState = State.Queued;

    }

    public void DeleteTask()
    {
        TaskState = State.Done;
        //NPC.taskList.Remove(this);
    }
}
