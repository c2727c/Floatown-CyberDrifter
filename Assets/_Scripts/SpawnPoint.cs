using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public SwordHit swordHitScript;  //invoke the variable prespawnLocation
    public GameObject player;   //change the position of the player

    private int prespawnIndex;
    private int postspawnIndex;


    void Awake()
    {
        //spawnLocations = GameObject.FindGameObjectWithTag("SpawnPoint");
        Debug.Log("Spawn points loaded");
    }

    void Start()
    {
        getLocation();
    }

    void Update()
    {
        getLocation();
    }

    private void getLocation()
    {
        int i;
        for(i = 0; i < spawnLocations.Length; i++)
        {
            if(spawnLocations[i].transform.position == SwordHit.preSpawnPosition)
            {
                prespawnIndex = i;
                break;
            }
        }
        if(i == spawnLocations.Length)
            prespawnIndex = 0;
        
        postspawnIndex = prespawnIndex + 1;

        Debug.Log("prespawnIndex : " + prespawnIndex);
        Debug.Log("postspawnIndex : " + postspawnIndex);
    }

    public void toPreSpawnPoint()
    {
        //GameObject.Instantiate(player, spawnLocations[prespawnIndex].transform.position, Quaternion.identity);
        Debug.Log("Before transition player position is " + player.transform.position);
        Debug.Log("position the player needs to go" + spawnLocations[prespawnIndex].transform.position);
        player.transform.position = spawnLocations[prespawnIndex].transform.position;
        Debug.Log("After transition player position is " + player.transform.position);
    }

    public void toPostSpawnPoint()
    {
        if(postspawnIndex != spawnLocations.Length)
            //GameObject.Instantiate(player, spawnLocations[postspawnIndex].transform.position, Quaternion.identity);
            player.transform.position = spawnLocations[postspawnIndex].transform.position;
    }

}
