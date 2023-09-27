using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    
    private void Awake()
    {
        Setup();
        currentState = CurrentState.Free;
    }

    
}
