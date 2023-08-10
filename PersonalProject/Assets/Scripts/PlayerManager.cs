using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int troops;

    [SerializeField] private Text armySizeText;

    private void Awake()
    {
        armySizeText.text = troops.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AddSoldier();
        }
    }



    private void AddSoldier()
    {
        troops++;
        armySizeText.text = troops.ToString();
    }

}
