using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarHandler : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartFight(Character _character1,Character _character2)
    {
        Debug.Log(_character1.characterName + " is Fighting with " + _character2.characterName);
    }


}
