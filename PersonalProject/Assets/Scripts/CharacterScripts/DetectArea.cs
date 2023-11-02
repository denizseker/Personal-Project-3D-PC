using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script handling DetectArea collider 
public class DetectArea : MonoBehaviour
{

    NPCAI npcAI;
    Player player;
    Collider capsuleCollider;
    private void Awake()
    {
        if (GetComponentInParent<NPCAI>() != null) npcAI = GetComponentInParent<NPCAI>();
        if (GetComponentInParent<Player>() != null) player = GetComponentInParent<Player>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void OnOffCollider()
    {
        capsuleCollider.enabled = !capsuleCollider.enabled;
    }


    //private void OnTriggerStay(Collider other)
    //{
    //    if (npcAI != null) npcAI.DetectAreaOnTriggerEnter(other);
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (npcAI != null) npcAI.DetectAreaOnTriggerEnter(other);
    //}

    private void OnTriggerExit(Collider other)
    {
        if (npcAI != null) npcAI.DetectAreaOnTriggerExit(other);
    }

}
