using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> Settlements = new List<GameObject>();
    public List<GameObject> NPCSoldiers = new List<GameObject>();



    private void Awake() 
    {
        Instance = this;
    }

    private void Update()
    {

    }

}
