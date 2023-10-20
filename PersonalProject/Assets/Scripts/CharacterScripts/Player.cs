using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public GameObject clickedTarget;
    [HideInInspector] public bool isMovingToTarget = false;

    private void Awake()
    {
        Setup();
        currentState = CurrentState.Free;
    }

    public void MoveToTarget(GameObject _target)
    {
        isMovingToTarget = true;
        clickedTarget = _target;
        agent.SetDestination(clickedTarget.transform.position);

        //float distance = Vector3.Distance(transform.position, clickedTarget.transform.position);
        //if (distance < 7)
        //{
        //    StopMoving();
        //}
    }

    public void ClearClickedTarget()
    {
        clickedTarget = null;
        isMovingToTarget = false;
    }
    public void StopMoving()
    {
        isMovingToTarget = false;
        clickedTarget = null;
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        agent.ResetPath();
    }

    private void Update()
    {
        if (isMovingToTarget)
        {
            MoveToTarget(clickedTarget);
        }
    }

}
