using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using VRTK;
public class MovingPlatformEvents : MonoBehaviour
{
    WaypointProgressTracker wayTracker;
    VRTK_InteractableObject interactableObject;
    //public  Vector3 stopPoint;
    public Transform stopTrans;//公用的每次Stop按照它
    public Transform stopT1;//只是记录下第一个停点
    public Transform stopT2;//只是记录下第二个停点
    bool flag = true;
    public AudioSource decendingAudio;


    // Start is called before the first frame update
    bool firstRound = true;
    void Start()
    {
        //stopPoint=new Vector3(186.66f,-28.35f,501.95f);
        wayTracker = gameObject.GetComponent<WaypointProgressTracker>();
        wayTracker.enabled = true;
        wayTracker.lookAheadForTargetOffset = 8;
        interactableObject = gameObject.transform.Find("SM_Veh_Future_01SteeringW").gameObject.GetComponent<VRTK_InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Stop();
        if(interactableObject.IsGrabbed()&&flag){//方向盘被抓住后发生的事情
            RunTo(stopT2);
            flag = false;
        }
        
    }
    public void Run(){
        wayTracker.enabled = true;
    }
    // public void RunTo(float y){
    //     wayTracker.enabled = true;
    //     //stopPoint = y;
    // }
    public void RunTo(Transform stopT){
        this.stopTrans = stopT;
        wayTracker.enabled = true;
    }
    public void RunToTower(){
        RunTo(stopT1);
        decendingAudio.Play();
    }
    public void Stop(){
        float delta = Vector3.Distance(transform.position, stopTrans.position);
        //Debug.Log("Stop:delta="+delta+"|this:"+transform.position+"|stopTrans:"+stopTrans.position);
        if(delta<1.0f)
        {
            wayTracker.enabled = false;
            wayTracker.lookAheadForTargetOffset = 0.8F;
        }
        
    }
}
