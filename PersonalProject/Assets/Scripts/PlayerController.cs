using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string RUN = "Run";
    Animator animator;
    CustomAction input;
    NavMeshAgent agent;


    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;

    private GameManager gameManager;


    void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500))
        {
            //only trigger on terrain click.
            if(hit.collider.gameObject.layer == 6)
            {
                //Agent moving to hit point

                agent.destination = hit.point;


                gameManager.ClearSelectedObjects();

                ////If we have a clicked object already and somewhere else for move we are going to unselect that object.
                //if (gameManager.selectedObjects.Count == 1)
                //{

                //    gameManager.selectedObjects[0].transform.parent.GetComponentInChildren<MoveToObject>().isSelected = false;
                //    //checking what is selected object - This is soldier.
                //    if (gameManager.selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerSoldier>() != null)
                //    {
                //        gameManager.selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerSoldier>().infoPanel.SetActive(false);
                //        gameManager.selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerSoldier>().armySizePanel.SetActive(true);
                //    }
                //    //checking what is selected object - This is town.
                //    else if (gameManager.selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerSettlement>() != null)
                //    {
                //        gameManager.selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerSettlement>().InfoPanel.SetActive(false);
                //    }
                //    //SelectEffect deactivating
                //    gameManager.selectedObjects[0].SetActive(false);
                //    //Selectedoject list cleared.
                //    gameManager.selectedObjects.Clear();
                //}
                //Click effect instantiating at hit point.
                if (clickEffect != null)
                { Instantiate(clickEffect, hit.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation); }
            }

        }
    }

    void OnEnable()
    { input.Enable(); }

    void OnDisable()
    { input.Disable(); }

    void Update()
    {
        SetAnimations();
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