using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager Instance;
    public RectTransform UI_interactCharacterPanel;

    private GameObject interactedCharacter;
    private GameObject player;

    private void Awake()
    {
        Instance = this;
    }

    public void TakeDataActivateInteractPanel(GameObject _characterObj, GameObject _player)
    {
        Instance.interactedCharacter = _characterObj;
        Instance.player = _player;
        ToggleInteractCharacterPanel();
    }

    public void ToggleInteractCharacterPanel()
    {
        //Panel inactive
        if (Instance.UI_interactCharacterPanel.gameObject.activeSelf)
        {
            Instance.UI_interactCharacterPanel.gameObject.SetActive(false);
        }
        //Panel active
        else
        {
            Instance.UI_interactCharacterPanel.gameObject.SetActive(true);
        }
    }

    public void EnterWar()
    {
        Instance.interactedCharacter.GetComponent<NPCAI>().SpawnWarHandler(player.GetComponent<Character>());
    }
}
