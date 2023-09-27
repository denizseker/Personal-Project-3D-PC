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
        if (other.tag == "Player" || other.tag == "NPC")
        {
            Character interactedCharacter = other.GetComponent<Character>();

            if(interactedCharacter.currentState == Character.CurrentState.Defeated)
            {
                //Adding characters gameobject to settlement characterintown list.
                settlement.characterInTown.Add(interactedCharacter.gameObject);
                interactedCharacter.currentState = Character.CurrentState.InSettlement;
                interactedCharacter.ChangeCharacterVisibility();
            }
        }
    }
}
