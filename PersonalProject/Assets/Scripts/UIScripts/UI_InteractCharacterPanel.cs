using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InteractCharacterPanel : MonoBehaviour
{

    [SerializeField] private GameObject enterWarButton;
    [SerializeField] private GameObject followButton;
    [SerializeField] private CharacterPrevSlotHandler charPrevSlot;
    [HideInInspector] public bool isPanelActive = false;

    public void TogglePanel(bool _isEnemy)
    {
        //Panel inactive
        if (gameObject.activeSelf)
        {
            isPanelActive = false;
            charPrevSlot.ResetCharacter();
            gameObject.SetActive(false);
        }
        //Panel active
        else
        {
            isPanelActive = true;
            Character _character = InteractManager.Instance.interactedCharacter.GetComponent<Character>();

            if (_isEnemy) SetPanelForEnemy(_character);
            else SetPanelForAlly(_character);

            charPrevSlot.SetCharacter(_character);
            gameObject.SetActive(true);
        }
    }

    public void SetPanelForEnemy(Character _character)
    {
        followButton.SetActive(false);
        //if interactedcharacter is not in settlement, enable enterwar button
        if(!_character.IsCharacterState(Character.State.InSettlement)) enterWarButton.SetActive(true);
    }
    public void SetPanelForAlly(Character _character)
    {
        enterWarButton.SetActive(false);
        followButton.SetActive(true);
    }
   

}
