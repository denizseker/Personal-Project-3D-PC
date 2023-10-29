using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InSettlementPanel : MonoBehaviour
{
    [SerializeField] private CharacterPrevSlotHandler[] settlementPrevSlots;
    [SerializeField] private GameObject charPrevPanel;
    [SerializeField] private GameObject interactPanel;
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
            isPanelActive = true;
            UpdateCharPrev();
            gameObject.SetActive(true);
        }
    }


}
