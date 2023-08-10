using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> selectedObjects = new List<GameObject>();



    private void Start()
    {
        
    }


    public void ClearSelectedObjects()
    {
        //If we have a clicked object already
        if (selectedObjects.Count == 1)
        {

            selectedObjects[0].transform.parent.GetComponentInChildren<MoveToObject>().isSelected = false;
            //checking what is selected object - This is soldier.
            if (selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerSoldier>() != null)
            {
                selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerSoldier>().infoPanel.SetActive(false);
                selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerSoldier>().armySizePanel.SetActive(true);
            }
            //checking what is selected object - This is town.
            else if (selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerSettlement>() != null)
            {
                selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerSettlement>().InfoPanel.SetActive(false);
            }
            //SelectEffect deactivating
            selectedObjects[0].SetActive(false);
            //Selectedoject list cleared.
            selectedObjects.Clear();
        }
    }

}
