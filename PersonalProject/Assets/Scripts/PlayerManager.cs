using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int troops;

    [SerializeField] private Text armySizeText;
    public string playerName = "Sir Eternal";

    private Army army;



    private void Awake()
    {
        armySizeText.text = troops.ToString();
        army = GetComponent<Army>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {

            Debug.Log(army.PeasentRecruit.ShareExp(300));
            
        }
    }


}
