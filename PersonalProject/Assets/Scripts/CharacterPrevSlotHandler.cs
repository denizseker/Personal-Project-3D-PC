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
        //cant click to player prev
        if(character.GetType() != typeof(Player))
        {
            Character playerChar = GameManager.Instance.player.GetComponent<Character>();
            bool isEnemy = ClanManager.Instance.IsEnemy(character.clan, playerChar.clan);
            InteractManager.Instance.interactedCharacter = character.gameObject;
            UIManager.Instance.ToggleInteractCharacterPanel(isEnemy);
        }
    }
}
