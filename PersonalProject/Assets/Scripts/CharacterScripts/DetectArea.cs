using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script handling DetectArea collider 
public class DetectArea : MonoBehaviour
{

    NPCAI npcAI;
    Collider capsuleCollider;
    private void Awake()
    {
        npcAI = GetComponentInParent<NPCAI>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void OnOffCollider()
    {
        capsuleCollider.enabled = !capsuleCollider.enabled;
    }


    private void OnTriggerEnter(Collider other)
    {
        npcAI.DetectAreaOnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        npcAI.DetectAreaOnTriggerExit(other);
    }

}
