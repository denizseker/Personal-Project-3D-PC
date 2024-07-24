using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InSettlementPanel : MonoBehaviour
{
    [SerializeField] private CharacterPrevSlotHandler[] settlementPrevSlots;
    [SerializeField] private GameObject charPrevPanel;
    [SerializeField] private GameObject interactPanel;
    [SerializeField] private Image townClanLogo;
    [SerializeField] private TMP_Text townNameText;
    [HideInInspector] public bool isPanelActive = false;

    public void ResetCharPrev()
    {
        for (int i = 0; i < settlementPrevSlots.Length; i++)
        {
            settlementPrevSlots[i].ResetCharacter();
            settlementPrevSlots[i].gameObject.SetActive(false);
        }
    }

    public void UpdateCharPrev()
    {
        ResetCharPrev();
        Settlement _settlement = InteractManager.Instance.interactedSettlement.GetComponent<Settlement>();
        for (int i = 0; i < _settlement.characterInTown.Count; i++)
        {
            settlementPrevSlots[i].SetCharacter(_settlement.characterInTown[i].GetComponent<Character>());
            settlementPrevSlots[i].gameObject.SetActive(true);
        }
    }

    public void TogglePanel()
    {
        //Panel inactive
        if (gameObject.activeSelf)
        {
            isPanelActive = false;
            ResetCharPrev();
            gameObject.SetActive(false);
        }
        //Panel active
        else
        {
            Settlement _settlement = InteractManager.Instance.interactedSettlement.GetComponent<Settlement>();
            townClanLogo.sprite = _settlement.clan.clanLogo;
            townNameText.text = _settlement.settlementName;
            isPanelActive = true;
            UpdateCharPrev();
            gameObject.SetActive(true);
        }
    }


}
