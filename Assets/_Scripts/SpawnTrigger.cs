using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    // public SpawnPoint spawnpoint;
    // // Start is called before the first frame update
    // void Awake()
    // {
    //     spawnpoint = GameObject.Find("SpawnScript").GetComponents<SpawnPoint>()[0];
        
    // }
    public SpawnPoint spawnpoint;
    // Update is called once per frame

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            spawnpoint.toPreSpawnPoint();

        }
    }
    void OnTriggerStay(Collider other){
        if(other.gameObject.CompareTag("Player")){
            spawnpoint.toPreSpawnPoint();

        }
    }
}
