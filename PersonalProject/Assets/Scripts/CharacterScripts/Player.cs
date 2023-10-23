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
    public void InteractAreaOnTriggerEnter(Collider other)
    {

    }
    public void InteractAreaOnTriggerExit(Collider other)
    {

    }
    public void DetectAreaOnTriggerEnter(Collider other)
    {

    }
    public void DetectAreaOnTriggerExit(Collider other)
    {
        
    }
    public void MoveToTarget(GameObject _target)
    {
        isMovingToTarget = true;
        clickedTarget = _target;
        agent.SetDestination(clickedTarget.transform.position);
    }

    public void ClearClickedTarget()
    {
        clickedTarget = null;
        isMovingToTarget = false;
        UIManager.Instance.ClearSelectedObjects(gameObject);
    }
    public void StopAgent()
    {
        isMovingToTarget = false;
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
