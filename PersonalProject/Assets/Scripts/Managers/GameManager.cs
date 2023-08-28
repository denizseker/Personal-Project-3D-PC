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
    public List<GameObject> Settlements = new List<GameObject>();
    public List<GameObject> NPCSoldiers = new List<GameObject>();

    private void Awake() 
    {
        Instance = this;
    }

   

}
