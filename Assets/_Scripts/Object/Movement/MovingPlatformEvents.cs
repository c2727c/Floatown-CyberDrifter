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
    public Transform stopTrans;
    public Transform stopTrans2;
    bool flag = true;


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
        if(interactableObject.IsGrabbed()&&flag){
            RunTo(stopTrans2);
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
