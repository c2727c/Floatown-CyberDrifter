using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerTriggerEvent : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
         if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter--SpawnPoint-Player");
            gameObject.SetActive(false);
            //更新preSpawnPosition
            SpawnPoint.preSpawnPosition = gameObject.transform.position;
            Debug.Log("The spawn position is " + SpawnPoint.preSpawnPosition);
        }
     }
}
