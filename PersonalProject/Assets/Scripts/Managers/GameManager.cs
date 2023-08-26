using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Clans
    {
        WUTANG,
        V8,
        EHLIMEDDAH,
        KAZZAK,
        None,
    }

    public static GameManager Instance;

    public List<GameObject> selectedObjects = new List<GameObject>();
    public List<GameObject> Settlements = new List<GameObject>();
    public List<GameObject> NPCSoldiers = new List<GameObject>();

    private void Awake() 
    {
        Instance = this;
    }

    public void ClearSelectedObjects()
    {
        //If we have a clicked object already
        if (Instance.selectedObjects.Count == 1)
        {

            Instance.selectedObjects[0].transform.parent.GetComponentInChildren<MoveToObject>().isSelected = false;
            //checking what is selected object - This is soldier.
            if (Instance.selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerAISoldier>() != null)
            {
                Instance.selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerAISoldier>().infoPanel.SetActive(false);
                Instance.selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerAISoldier>().armySizePanel.SetActive(true);
            }
            //checking what is selected object - This is town.
            else if (Instance.selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerSettlement>() != null)
            {
                Instance.selectedObjects[0].transform.parent.GetComponentInChildren<CanvasControllerSettlement>().InfoPanel.SetActive(false);
            }
            //SelectEffect deactivating
            Instance.selectedObjects[0].SetActive(false);
            //Selectedoject list cleared.
            Instance.selectedObjects.Clear();
        }
    }

}
