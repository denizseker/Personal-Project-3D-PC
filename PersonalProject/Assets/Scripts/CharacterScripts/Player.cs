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
        SetCharacterState(State.Free);
    }
    private void Start()
    {
        isVisible = true;
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

}
