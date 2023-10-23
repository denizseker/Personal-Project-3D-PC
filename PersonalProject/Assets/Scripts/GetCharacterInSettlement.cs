using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCharacterInSettlement : MonoBehaviour
{
    private Settlement settlement;


    private void Awake()
    {
        settlement = GetComponentInParent<Settlement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InteractArea")
        {
            Character interactedCharacter = other.GetComponentInParent<Character>();

            if(interactedCharacter.currentState == Character.CurrentState.Defeated)
            {
                //Adding characters gameobject to settlement characterintown list.
                settlement.AddCharacter(interactedCharacter.gameObject);
                interactedCharacter.currentState = Character.CurrentState.InSettlement;
                interactedCharacter.OnOffCharacterComponentForTown(false);
            }
            //if player interact
            if(interactedCharacter.GetType() == typeof(Player))
            {
                //if player clicked this
                if(interactedCharacter.GetComponent<PlayerController>().clickedTarget == settlement.gameObject)
                {
                    settlement.AddCharacter(interactedCharacter.gameObject);
                    interactedCharacter.currentState = Character.CurrentState.InSettlement;
                    interactedCharacter.town = settlement.gameObject;
                    interactedCharacter.OnOffCharacterComponentForTown(false);
                    interactedCharacter.GetComponent<PlayerController>().StopAgent();
                }
            }
        }
    }
}
