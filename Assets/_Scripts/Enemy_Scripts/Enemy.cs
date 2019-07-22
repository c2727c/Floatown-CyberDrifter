using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{

    //定义敌人的Transform
    Transform m_transform;
    //CharacterController m_ch;

    //定义动画组件
    Animator m_animator;

    //定义寻路组件
    UnityEngine.AI.NavMeshAgent m_agent;

    //定义一个主角类的对象
    PlayerControl m_player;
    //角色移动速度
    public float m_moveSpeed = 0.5f;
    //角色旋转速度
    public float m_rotSpeed = 120;
    //定义生命值
    public int m_life = 15;

    //定义计时器 
    float m_timer = 2;
    //定义生成点
    //protected EnemySpawn m_spawn;


    // Use this for initialization
    void Start()
    {
        //初始化m_transform 为物体本身的tranform
        m_transform = this.transform;

        //初始化动画m_ani 为物体的动画组件
        m_animator = this.GetComponent<Animator>();

        //初始化寻路组件m_agent 为物体的寻路组件
        m_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //初始化主角
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();


    }

    // Update is called once per frame
    void Update()
    {
        //设置敌人的寻路目标
        m_agent.SetDestination(m_player.m_transform.position);

        //调用寻路函数实现寻路移动
        MoveTo();        

        
    }


    //敌人的自动寻路函数
    void MoveTo()
    {
        //定义敌人的移动量
        float speed = m_moveSpeed * Time.deltaTime;

        //通过寻路组件的Move()方法实现寻路移动
        m_agent.Move(m_transform.TransformDirection(new Vector3(0, 0, speed)));
    }



}