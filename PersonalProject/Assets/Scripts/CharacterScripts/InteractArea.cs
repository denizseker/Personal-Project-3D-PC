using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script handling InteractArea collider 

public class InteractArea : MonoBehaviour
{
    NPCAI npcAI;
    Player player;
    Collider col;
    CheckVisibility checkVisibility;
    private void Awake()
    {
        if (GetComponentInParent<NPCAI>() != null)
        {
            npcAI = GetComponentInParent<NPCAI>();
            checkVisibility = gameObject.transform.parent.GetComponentInChildren<CheckVisibility>();
        } 
        if (GetComponentInParent<Player>() != null) player = GetComponentInParent<Player>();
        col = GetComponent<Collider>();
    }

    public void OnOffCollider()
    {
        col.enabled = !col.enabled;
    }



    private void OnTriggerStay(Collider other)
    {
        if (npcAI != null)
        {
            checkVisibility.InteractAreaOnTriggerStay(other);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(npcAI !=null)
        {
            npcAI.InteractAreaOnTriggerEnter(other);
            checkVisibility.InteractAreaOnTriggerEnter(other);
        }
            
            
        if (player != null) player.InteractAreaOnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if(npcAI != null)
        {
            npcAI.InteractAreaOnTriggerExit(other);
            checkVisibility.InteractAreaOnTriggerExit(other);
        }
            
            
        if (player != null) player.InteractAreaOnTriggerExit(other);
    }
}
