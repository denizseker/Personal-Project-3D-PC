using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script handling InteractArea collider 

public class InteractArea : MonoBehaviour
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
        npcAI.InteractAreaOnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        npcAI.InteractAreaOnTriggerExit(other);
    }
}
