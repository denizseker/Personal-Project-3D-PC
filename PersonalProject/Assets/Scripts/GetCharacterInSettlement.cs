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

            if(interactedCharacter.currentState == Character.State.Defeated)
            {
                //Adding characters gameobject to settlement characterintown list.
                settlement.AddCharacter(interactedCharacter.gameObject);
            }
            //if player interact
            if(interactedCharacter.GetType() == typeof(Player))
            {
                //if player clicked this
                if(interactedCharacter.GetComponent<PlayerController>().clickedTarget == settlement.gameObject)
                {
                    settlement.AddCharacter(interactedCharacter.gameObject);
                    interactedCharacter.town = settlement.gameObject;
                    interactedCharacter.GetComponent<PlayerController>().ClearClickedTarget();
                    InteractManager.Instance.TakeDataActivateSettlementInteractPanel(settlement.gameObject,interactedCharacter.gameObject);
                }
            }
        }
    }
}
