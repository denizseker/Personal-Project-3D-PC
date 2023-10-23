using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public GameObject clickedTarget;
    const string IDLE = "Idle";
    const string RUN = "Run";
    Animator animator;
    CustomAction input;
    NavMeshAgent agent;
    Player character;

    [HideInInspector] public bool isMovingToTarget = false;
    [SerializeField] ParticleSystem clickEffect;


    void Awake()
    {
        character = GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
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

        //If mouse not over on UI elements & game not stopped
        if (!EventSystem.current.IsPointerOverGameObject() && !TimeManager.Instance.isGameStopped)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 2000))
            {
                //only trigger on terrain click.
                if (hit.collider.gameObject.layer == 6)
                {
                    //Agent moving to hit point
                    agent.SetDestination(hit.point);


                    ClearClickedTarget();
                    //User clicked terrain for move so we are clearing selected objects.
                    UIManager.Instance.ClearSelectedObjects(gameObject);

                    if (clickEffect != null)
                    { Instantiate(clickEffect, hit.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation); }
                }
            }
        } 
    }
    public void MoveToTarget(GameObject _target)
    {
        isMovingToTarget = true;
        clickedTarget = _target;

        if(_target.GetComponent<Settlement>() != null)
        {
            agent.SetDestination(_target.GetComponentInChildren<GetCharacterInSettlement>().transform.position);
            return;
        }

        agent.SetDestination(clickedTarget.transform.position);
    }
    public void ClearClickedTarget()
    {
        clickedTarget = null;
        isMovingToTarget = false;
        UIManager.Instance.ClearSelectedObjects(gameObject);
    }
    public void StopAgent()
    {
        isMovingToTarget = false;
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        agent.ResetPath();
    }

    void Update()
    {
        SetAnimations();
        if (Input.GetMouseButtonDown(0) && character.currentState != Character.CurrentState.InInteraction)
        {
            if (character.currentState == Character.CurrentState.InSettlement)
            {
                ClearClickedTarget();
                character.LeaveSettlement();
            }
            ClickToMove();
        }

        if (isMovingToTarget && character.currentState != Character.CurrentState.InInteraction)
        {
            if (character.currentState == Character.CurrentState.InSettlement)
            {
                ClearClickedTarget();
                character.LeaveSettlement();
            }
            Debug.Log("Target click");
            MoveToTarget(clickedTarget);
        }

        
    }

    void SetAnimations()
    {
        if (animator.gameObject.activeSelf)
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
}