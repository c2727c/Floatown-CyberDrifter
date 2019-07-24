using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAway : MonoBehaviour
{
    bool flag = false;
    public float speed = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flag){
            transform.position+= transform.forward*speed;
        }
    }
    public void MoveAway(){
        Debug.Log("MoveAway");
        flag = true;
    }
}
