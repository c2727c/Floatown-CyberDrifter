using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twinkle : MonoBehaviour
{
    private float calTime;//计时器    
    void Start()    
    {        
        calTime = 0f;    
    }    
    void Update()    
    {   
        calTime += Time.deltaTime;//每一帧的间隔时间累加       
        if (calTime % 2 > 0.5)//除以2余数大于0.5即每1秒显隐一次
        {            
            GetComponent<MeshRenderer>().enabled = true;
        }        
        else        
        {            
            GetComponent<MeshRenderer>().enabled = false;
        }    
    }

}
