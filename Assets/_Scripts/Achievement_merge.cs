using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SSFS;
using VRTK;

public class Achievement_merge : MonoBehaviour
{

    public AudioSource audio;
    private SimpleSSFSToggle SSFSToggle;

    int frameSpan = -1; 
    // Start is called before the first frame update
    //public VRTK_BodyPhysics bodyPhysics;
    public VRTK_PlayerClimb playerClimb;
    private bool triggerEntered = false;
    Transform playerTrans;
    public AudioSource AchievementAudio;            
    protected virtual void Awake()
        {
            //bodyPhysics = (bodyPhysics != null ? bodyPhysics : FindObjectOfType<VRTK_BodyPhysics>());
            playerClimb = (playerClimb != null ? playerClimb : FindObjectOfType<VRTK_PlayerClimb>());
            playerTrans = GameObject.Find("CenterEyeAnchor").transform;
        }
    void Start()
    {
        SSFSToggle = gameObject.GetComponent<SimpleSSFSToggle>();
        SSFSToggle.phaseOn = false;
        audio.enabled=false;
        //audio = GetComponent<AudioSource>();//之前一直出问题居然……是因为没加音源而请求………………
        
    }

    // Update is called once per frame
    void Update()
    {

        if(frameSpan>0)
        {
            frameSpan--;
        }else if(frameSpan ==0){
            frameSpan--;
            //调整位置
            transform.position =playerTrans.position+playerTrans.forward*3+ Vector3.up*1.2f;
            transform.rotation = Quaternion.LookRotation(transform.position-playerTrans.position);
            //然后显示
            //置标志脚本的Boolean为true
            SSFSToggle.phaseOn = true;
            //播放标志出现音乐
        }
    }

    void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.CompareTag("Player")&&frameSpan<0)
        {
            //判断一下玩家是否在地上（onground）只有在地上才……
            Debug.Log("Achievement_merge:OnTriggerEnter");
            if(!playerClimb.IsClimbing()){
                //播放情节背景音乐
                audio.enabled=true;
                audio.Play();
                frameSpan = 90;//约1.5秒后出现标志
            }
            
        }

    }
}
