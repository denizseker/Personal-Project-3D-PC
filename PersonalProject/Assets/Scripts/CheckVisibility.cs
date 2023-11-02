using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CheckVisibility : MonoBehaviour
{
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private GameObject modelObj;
    private FadeInOutPanel fadeScript;
    private Animator animator;
    public bool isVisible = false;
    private void Awake()
    {
        animator = modelObj.GetComponent<Animator>();
        fadeScript = gameObject.transform.parent.GetComponentInChildren<FadeInOutPanel>();
    }

    private void Start()
    {
        SetOff();
    }

    private void SetOn()
    {
        isVisible = true;
        animator.enabled = true;
        canvasObj.SetActive(true);
        modelObj.SetActive(true);
    }
    private void SetOff()
    {
        StartCoroutine(WaitTillFadeOut());
        
    }

    public void InteractAreaOnTriggerStay(Collider other)
    {
        if (other.tag == "VisibleArea" && !isVisible)
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
            SetOff();
        }
    }

    IEnumerator WaitTillFadeOut()
    {
        fadeScript.StartFadeOut();
        yield return new WaitForSecondsRealtime(0.5f);
        isVisible = false;
        animator.enabled = false;
        canvasObj.SetActive(false);
        modelObj.SetActive(false);
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
