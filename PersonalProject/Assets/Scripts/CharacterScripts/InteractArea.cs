using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script handling InteractArea collider 

public class InteractArea : MonoBehaviour
{
    NPCAI npcAI;
    Player player;
    Collider col;
    private void Awake()
    {
        if (GetComponentInParent<NPCAI>() != null) npcAI = GetComponentInParent<NPCAI>();
        if (GetComponentInParent<Player>() != null) player = GetComponentInParent<Player>();
        col = GetComponent<Collider>();
    }

    public void OnOffCollider()
    {
        col.enabled = !col.enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(npcAI !=null) npcAI.InteractAreaOnTriggerEnter(other);
        if (player != null) player.InteractAreaOnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if(npcAI != null) npcAI.InteractAreaOnTriggerExit(other);
        if (player != null) player.InteractAreaOnTriggerExit(other);
    }
}
