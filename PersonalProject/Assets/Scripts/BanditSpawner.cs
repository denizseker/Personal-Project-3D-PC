using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    private void OnDrawGizmos()
    {
        //Handles.Label(transform.position + new Vector3(0, 1, 0), "Bandit Spawn Point");
        Gizmos.DrawCube(transform.position, new Vector3(1,1,1));
    }

}
