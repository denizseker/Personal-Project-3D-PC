using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToObject : MonoBehaviour
{
    [SerializeField] private GameObject selectedEffect;

    private GameObject player;
    private NPCAI chaseAndCatch;

    public bool isSelected = false;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //town ise null olacak.
        chaseAndCatch = GetComponentInChildren<NPCAI>();
    }

    //Clicking to enemy or town
    private void OnMouseDown()
    {
        //Resetting selected object for every town/enemy click
        GameManager.Instance.ClearSelectedObjects();
        GameManager.Instance.selectedObjects.Add(selectedEffect);

        selectedEffect.SetActive(true);
        isSelected = true;
        //Clicking to town
        if (isSelected && GetComponentInChildren<NPCAI>() == null)
        {
            NavMeshAgent playerAgent = player.GetComponent<NavMeshAgent>();
            Collider col = GetComponentInParent<Collider>();
            playerAgent.destination = col.ClosestPoint(playerAgent.transform.position);
        }
    }

    private void OnMouseOver()
    {
        selectedEffect.SetActive(true);
    }
    private void OnMouseExit()
    {
        if(!isSelected) selectedEffect.SetActive(false);

    }
    private void Update()
    {
        //If clicked object is enemy we are updating destination for follow.
        if (GetComponentInChildren<NPCAI>() != null)
        {
            if (isSelected && !chaseAndCatch.isCatched)
            {
                player.GetComponent<NavMeshAgent>().SetDestination(transform.position);
            }
        }
    }
}
