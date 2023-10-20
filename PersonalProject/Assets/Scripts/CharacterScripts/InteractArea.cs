using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script handling InteractArea collider 

public class InteractArea : MonoBehaviour
{
    NPCAI npcAI;
    Collider col;
    private void Awake()
    {
        if (GetComponentInParent<NPCAI>() != null) npcAI = GetComponentInParent<NPCAI>();
        col = GetComponent<Collider>();
    }

    public void OnOffCollider()
    {
        col.enabled = !col.enabled;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(npcAI !=null) npcAI.InteractAreaOnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if(npcAI != null) npcAI.InteractAreaOnTriggerExit(other);
    }

    //private void OnMouseEnter()
    //{
    //    mouseInteraction.InteractAreaOnMouseEnter();
    //}
    //private void OnMouseExit()
    //{
    //    mouseInteraction.InteractAreaOnMouseExit();
    //}
    //private void OnMouseOver()
    //{
    //    mouseInteraction.InteractAreaOnMouseOver();
    //}
    //private void OnMouseDown()
    //{
    //    mouseInteraction.InteractAreaOnMouseDown();
    //}
}
