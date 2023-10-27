using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPrevSlotHandler : MonoBehaviour
{
    public Character character;
    public Image clanLogo;
    public Image charPrev;
    public TMP_Text charName;

    public void SetCharacter(Character _character)
    {
        character = _character;
        clanLogo.sprite = character.clan.clanLogo;
        charPrev.sprite = character.charPrev;
        charName.text = character.characterName;
    }

    public void ResetCharacter()
    {
        character = null;
        clanLogo.sprite = null;
        charPrev.sprite = null;
        charName.text = null;
    }

    public void Click()
    {
        Debug.Log("Clicked to " + character.characterName);
    }
}
