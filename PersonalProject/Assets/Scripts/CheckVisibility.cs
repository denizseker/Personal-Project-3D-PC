using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CheckVisibility : MonoBehaviour
{
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private GameObject modelObj;
    private Character character;
    private FadeInOutPanel fadeScript;
    private Animator animator;
    private void Awake()
    {
        character = GetComponentInParent<Character>();
        animator = modelObj.GetComponent<Animator>();
        fadeScript = gameObject.transform.parent.GetComponentInChildren<FadeInOutPanel>();
    }

    private void Start()
    {
        
        SetOff();
    }

    private void SetOn()
    {
        character.isVisible = true;
        animator.enabled = true;
        canvasObj.SetActive(true);
        modelObj.SetActive(true);
    }
    public void InstaSetOff()
    {
        character.isVisible = false;
        animator.enabled = false;
        canvasObj.SetActive(false);
        modelObj.SetActive(false);
    }
    public void SetOff()
    {
        StartCoroutine(WaitTillFadeOut());
    }

    public void InteractAreaOnTriggerStay(Collider other)
    {
        if (other.tag == "VisibleArea" && !character.isVisible)
        {
            SetOn();
        }
    }

    public void InteractAreaOnTriggerEnter(Collider other)
    {
        if (other.tag == "VisibleArea")
        {
            SetOn();
        }
    }
    public void InteractAreaOnTriggerExit(Collider other)
    {
        if (other.tag == "VisibleArea")
        {
            //InstaSetOff();
            SetOff();
        }
    }

    IEnumerator WaitTillFadeOut()
    {
        fadeScript.StartFadeOut();
        yield return new WaitForSecondsRealtime(0.8f);
        InstaSetOff();
    }

    //private void OnBecameVisible()
    //{
    //    SetOn();
    //}
    //private void OnBecameInvisible()
    //{
    //    SetOff();
    //}
}
