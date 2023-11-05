using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstantiateManager : MonoBehaviour
{
    public static InstantiateManager Instance;
    public GameObject npcPrefab;
    public int npcCount;

    public TMP_Text npcText;

    public List<GameObject> spawnPointList = new List<GameObject>();
    public List<GameObject> instantiateList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void LoadNPC()
    {
        for (int i = 0; i < spawnPointList.Count; i++)
        {
            Clan _clan;

            if(spawnPointList[i].GetComponent<Settlement>() != null)
            {
                _clan = spawnPointList[i].GetComponent<Settlement>().clan;
            }
            else
            {
                _clan = spawnPointList[i].GetComponent<BanditSpawner>().clan;
            }

            for (int j = 0; j < npcCount; j++)
            {
                GameObject NPC = Instantiate(npcPrefab, spawnPointList[i].transform.position, spawnPointList[i].transform.rotation);
                NPC.GetComponent<Character>().clan = _clan;
                NPC.GetComponentInChildren<CheckVisibility>().InstaSetOff();
                instantiateList.Add(NPC);
            }
        }
        npcText.text = instantiateList.Count.ToString();
        Debug.Log("Done");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadNPC();
        }
    }
}
