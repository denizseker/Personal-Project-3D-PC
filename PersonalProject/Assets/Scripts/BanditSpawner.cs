using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditSpawner : MonoBehaviour
{

    public GameObject npcPrefab;
    public int banditOfNumber;

    public Clan clan;


    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < banditOfNumber; i++)
        //{
        //    GameObject NPC = Instantiate(npcPrefab, transform.position, transform.rotation);
        //    NPC.GetComponent<Character>().clan = clan;
        //}

        clan = ClanManager.Instance.None;
    }
}
