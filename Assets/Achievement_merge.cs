using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Achievement_merge : MonoBehaviour
{

    public GameObject notice;
    public GameObject achievement_trigger;
    DateTime start_time = DateTime.Now;
    DateTime end_time = DateTime.Now;

    TimeSpan ts; 

    bool isCountingtime = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(isCountingtime)
        // {
        //     end_time = DateTime.Now;
        //     ts = end_time - start_time;

        //     if(ts.Seconds == 5 )
        //     {
        //         notice.SetActive(false);
        //         achievement_trigger.SetActive(false);
        //     }
        // }
    }

    void OnTriggerEnter(Collider other)
    {
        // if(!isCountingtime)
        // {
        //     Debug.Log("PreJug");
        //     if(other.gameObject.CompareTag("Sword"))
        //     {
        //         Debug.Log("Trigger");
        //         isCountingtime = true;
        //         start_time = DateTime.Now;
        //         end_time = DateTime.Now;
        //         notice.SetActive(true);
        //     }
        // }
        
        //画布的出现
    }
}
