using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SSFS;
using VRTK;

public class Achievement_merge0 : MonoBehaviour//该脚本赋给标志对象，并作为Enemy的一个参数//当死亡且计数为1时//调用该脚本，并传入敌人所在位置
{

    AudioSource audio;
    private SimpleSSFSToggle SSFSToggle;

    int frameSpan = -1; 
    // Start is called before the first frame update
    public VRTK_BodyPhysics bodyPhysics;
    public VRTK_PlayerClimb playerClimb;
    Transform playerTrans;

    public AudioSource firstBloodAudio;
    protected virtual void Awake()
        {
            playerTrans = GameObject.Find("CenterEyeAnchor").transform;
        }
    void Start()
    {
        SSFSToggle = gameObject.GetComponent<SimpleSSFSToggle>();
        SSFSToggle.phaseOn = false;
        audio = GetComponent<AudioSource>();//之前一直出问题居然……是因为没加音源而请求………………
        
    }

    // Update is called once per frame
    void Update()
    {
        //自动面向主角？？
       
    }

    public void Show(Transform enemyPos){
        Debug.Log("Achievement_merge0---Show");
        
        //首先移动到敌人的位置
        //transform.position = enemyPos.position;
        //还是移动到玩家的位置，向前偏移transform.forward
        Debug.Log("playerTrans.rotation"+playerTrans.rotation);
        //transform.rotation = Vector3.zero;
        transform.position =playerTrans.position+playerTrans.forward*3+ Vector3.up*1.2f;
        transform.rotation = Quaternion.LookRotation(transform.position-playerTrans.position);
        
        //然后显示
        SSFSToggle.phaseOn = true;
        //播放音乐
        firstBloodAudio.Play();
    }

    
}
