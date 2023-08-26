using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToObject : MonoBehaviour
{
    [SerializeField] private GameObject selectedEffect;

    private GameObject player;
    private NPCAI chaseAndCatch;
    private GameObject parent;

    public bool isSelected = false;
    private void Awake()
    {
        parent = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        //town ise null olacak.
        chaseAndCatch = parent.GetComponentInChildren<NPCAI>();

        
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
        if (isSelected && parent.GetComponentInChildren<NPCAI>() == null)
        {
            NavMeshAgent playerAgent = player.GetComponent<NavMeshAgent>();
            Collider col = GetComponentInParent<Collider>();
            playerAgent.destination = col.ClosestPoint(playerAgent.transform.position);
        }
    }


    private void Update()
    {
        //If clicked object is enemy we are updating destination for follow.
        if (parent.GetComponentInChildren<NPCAI>() != null)
        {
            if (isSelected && !chaseAndCatch.isCatched)
            {
                player.GetComponent<NavMeshAgent>().SetDestination(transform.parent.transform.position);
            }
        }
    }
}
