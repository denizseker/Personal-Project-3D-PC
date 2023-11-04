using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MouseInteraction : MonoBehaviour
{
    private Character character;
    private Settlement settlement;
    private WarHandler warHandler;
    public GameObject ringEffect;


    private IInteractable interactable;

    private GameObject player;
    private NPCAI NPCAI;

    public bool isSelected = false;
    private void Awake()
    {
        interactable = GetComponent<IInteractable>();

        //if (GetComponent<Character>() != null) character = GetComponent<Character>();
        //if (GetComponent<Settlement>() != null) settlement = GetComponent<Settlement>();
        //if (GetComponent<WarHandler>() != null) warHandler = GetComponent<WarHandler>();
        ////town ise null olacak.
        //NPCAI = GetComponent<NPCAI>();
        player = GameManager.Instance.player;
    }

    private void OnMouseEnter()
    {
        //if (character.isVisible)
        //{
        //    //At character
        //    if (character != null)
        //    {
        //        UIManager.Instance.ActivateCharacterInfoPanel(character);
        //        ringEffect.SetActive(true);
        //    }
        //    //At settlement
        //    else if (settlement != null)
        //    {
        //        UIManager.Instance.ActivateSettlementInfoPanel(settlement);
        //        ringEffect.SetActive(true);
        //    }
        //    //At warhandler
        //    else if (warHandler != null)
        //    {
        //        UIManager.Instance.ActivateWarInfoPanel(warHandler);
        //        ringEffect.SetActive(true);
        //    }
        //}
        interactable.MouseEnter();

    }
    private void OnMouseOver()
    {
        //if (character.isVisible)
        //{
        //    ringEffect.SetActive(true);
        //    if (UIManager.Instance.UI_warInfoPanel.isPanelActive) UIManager.Instance.UI_warInfoPanel.UpdatePanel(warHandler);
        //    if (UIManager.Instance.UI_settlementInfoPanel.isPanelActive) UIManager.Instance.UI_settlementInfoPanel.UpdatePanel(settlement);

        //}
        //else
        //{
        //    UIManager.Instance.DeActivateSettlementInfoPanel();
        //    UIManager.Instance.DeActivateWarInfoPanel();
        //    UIManager.Instance.DeActivateCharacterInfoPanel();
        //    if (!isSelected) ringEffect.SetActive(false);

        //}
        interactable.MouseOver();

    }
    private void OnMouseExit()
    {
        //UIManager.Instance.DeActivateSettlementInfoPanel();
        //UIManager.Instance.DeActivateWarInfoPanel();
        //UIManager.Instance.DeActivateCharacterInfoPanel();
        //if (!isSelected) ringEffect.SetActive(false);
        interactable.MouseExit();
    }
    private void OnMouseDown()
    {
        //Cant click if player state is those
        if (!player.GetComponent<Character>().IsCharacterState(Character.State.InSettlement, Character.State.InInteraction, Character.State.InWar))
        {
            interactable.Click();
            

            ////Clicking to town
            //if (isSelected && settlement != null)
            //{
            //    //GameObject townDoor = settlement.gameObject.GetComponentInChildren<GetCharacterInSettlement>().gameObject;
            //    player.GetComponent<PlayerController>().MoveToTarget(gameObject);
            //}
            //if (isSelected && NPCAI != null)
            //{
            //    player.GetComponent<PlayerController>().MoveToTarget(gameObject);
            //}
        }

        

    }

    public void OnOffCollider()
    {
        GetComponent<Collider>().enabled = !GetComponent<Collider>().enabled;
    }

}
