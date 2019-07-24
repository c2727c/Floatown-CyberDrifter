using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBox : MonoBehaviour
{
    public enum EnergyBoxType { Normal, Event };
    public EnergyBoxType type = EnergyBoxType.Normal;
    public float energy = 10.0f;
    // Start is called before the first frame update
    MovingPlatformEvents mpEvent;
    public int id = 0;
    public MovingAway platform2;





    void Start()
    {
        // audio = GetComponent<AudioSource>();//之前一直出问题居然……是因为没加音源而请求………………
        if(type==EnergyBoxType.Event){
            Debug.Log("type==EnergyBoxType.Event!");
            mpEvent = GameObject.Find("Floating_Platform Variant").GetComponent<MovingPlatformEvents>();//没请求到音源…后面的就没有执行？
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            ExplodeDisappear();

        }
        
    }

    public void ExplodeDisappear(){
        if(type==EnergyBoxType.Event){//之前一直出问题居然……是因为没加音源而请求………………
        Debug.Log(" ExplodeDisappear:Event!");
        if(id==2){
            Debug.Log("id==2");
            CallEvent2();
        }else{
            CallEvent();
        }
        }
        Debug.Log(" ExplodeDisappear!");
        transform.Find("Energy Box 01").gameObject.GetComponent<MeshExploder>().Explode();
        GameObject.Destroy(gameObject);
    }
    public void LightUp(){

    }
    public void CallEvent(){
        Debug.Log("CallEvent!");
        //获取家长的Folating Platform
        //然后控制它开始运动
        mpEvent.RunToTower();
    }
    public void CallEvent2(){
        Debug.Log("CallEvent2!");
        platform2.MoveAway();
    }
}
