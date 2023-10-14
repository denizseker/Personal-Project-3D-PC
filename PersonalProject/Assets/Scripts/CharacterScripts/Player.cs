using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    private GameObject target;
    [HideInInspector] public bool isMoving = false;

    private void Awake()
    {
        Setup();
        currentState = CurrentState.Free;
    }

    public void MoveToDestination(GameObject _target)
    {
        isMoving = true;
        target = _target;
        agent.SetDestination(target.transform.position);

        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < 7)
        {
            StopMoving();
        }
    }

    public void StopMoving()
    {
        isMoving = false;
        target = null;
        agent.ResetPath();
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveToDestination(target);
        }
    }

}
