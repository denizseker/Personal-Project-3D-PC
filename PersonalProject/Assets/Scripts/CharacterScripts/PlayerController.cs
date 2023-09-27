using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string RUN = "Run";
    Animator animator;
    CustomAction input;
    NavMeshAgent agent;


    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        input = new CustomAction();
        AssignInputs();
    }

    void AssignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();
    }

    //Left click to terrain
    void ClickToMove()
    {
        RaycastHit hit;

        //If mouse not over on UI elements.
        if (!EventSystem.current.IsPointerOverGameObject() && !TimeManager.Instance.isGameStopped)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 2000))
            {
                //only trigger on terrain click.
                if (hit.collider.gameObject.layer == 6)
                {
                    //Agent moving to hit point

                    agent.destination = hit.point;

                    //User clicked terrain for move so we are clearing selected objects.
                    UIManager.Instance.ClearSelectedObjects();

                    if (clickEffect != null)
                    { Instantiate(clickEffect, hit.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation); }
                }
            }
        } 
    }

    void Update()
    {
        SetAnimations();

        if (Input.GetMouseButtonDown(0))
        {
            ClickToMove();
        }
    }


    void SetAnimations()
    {
        if (agent.hasPath)
        {
            animator.Play(RUN);
        }
        else
        {

            animator.Play(IDLE);
        }
    }
}