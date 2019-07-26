using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Exit : MonoBehaviour
{
    VRTK_InteractableObject interactableObject;
    int timer=-1;
    // Start is called before the first frame update
    void Start()
    {
        interactableObject = gameObject.GetComponent<VRTK_InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactableObject.IsGrabbed()){//方向盘被抓住后发生的事情
            timer = 600;
        }
        if(timer>0){
            --timer;
            if(timer==150){
                interactableObject.ForceStopInteracting();
                GameObject.Destroy(gameObject);
            }
        }else if(timer==0){
            --timer;
            //退出程序
            Application.Quit();
        }
        
    }
}
