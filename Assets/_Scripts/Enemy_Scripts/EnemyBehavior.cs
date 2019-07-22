using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; 

public class EnemyBehavior : MonoBehaviour
{
    //巡逻参数
    public float patrolSpeed=2.0f;
    public float arri_radius=2.0f;
    //生命参数
    public int life = 2;


     GameObject[] pathPoints;
    //记录下一个路点
    int nextPathPointIndex = 1;
    // Use this for initialization

    public String tagName="Spot";





    //定义寻路组件
    UnityEngine.AI.NavMeshAgent m_agent;
    //定义一个主角类的对象
    GameObject m_player;
    public float detect_radius=6.0f;
    public float attack_radius = 2.0f;
    public float moveSpeed = 3.0f;


    //动画控制
    Animator ani;
    bool attacking_flag = false;
    int hurt_cnt = 0;
    bool canBeHurt=false;
	float proTime = 0.0f; 
    float NextTime = 0.0f; 





    void Start () {
        m_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //初始化主角
        m_player = GameObject.FindGameObjectWithTag("Player");
        hurt_cnt = 0;






        pathPoints = GameObject.FindGameObjectsWithTag(tagName);
        Debug.Log("pathPoints: " + pathPoints.Length);
        //Array.Reverse(pathPoints);
        Array.Sort(pathPoints, (x, y) => { return x.gameObject.name.CompareTo(y.gameObject.name); });
        transform.position = pathPoints[0].transform.position;
        //两点之差计算向前移动的方向
        transform.forward = pathPoints[nextPathPointIndex].transform.position - transform.position;


        ani = this.GetComponent<Animator>();
    }






    // Update is called once per frame
    void Update () {
        /*time是从程序开始执行到现在的时间，deltaTime上一帧完成的时间，fixedTime表示FixedUpdate已经执行的时间，而fixedDeltatime是一个固定的时间增量。
        在update（）中time、deltaTime获取的是一个正确的值，fixedTime的值并不会增加，如果是在FixedUpdate
        中，则fixedTime值会更新并且和time的值一样，deltaTime和fixedDeltatime的值一样。注意除了fixedDeltatime其他3个值都是只读的，可以通过fixedDeltatime来改变FixedUpdate的跟新速率。 */
        // proTime = Time.time;//返回某一帧开始播放的值（只读）。该时间是游戏开始运行后以秒为单位记录的值。
		// if (proTime - NextTime == 3) 
		// {
		// 	if(!canBeHurt)canBeHurt=true;
		// 	NextTime = proTime;
		// }
        //
        AnimatorStateInfo animatorInfo;
        animatorInfo = ani.GetCurrentAnimatorStateInfo(0);
        if ((animatorInfo.normalizedTime > 0.0f) && (animatorInfo.IsName("Disappear")))//normalizedTime: 范围0 -- 1,  0是动作开始，1是动作结束
        {
            //ani.SetInteger("MyPlay", 0);//通过这种方式可以编程实现状态转移
            this.gameObject.SetActive(false);
        }
        float dist = Vector3.Distance(transform.position, m_player.transform.position);        
        //检查是否有敌人在攻击范围内
        if (dist < attack_radius)
        {
            Debug.Log("dist < attack_radius");
            if(!attacking_flag) ani.SetTrigger("SpinAttack");
            ani.SetBool("Idle", false);
            attacking_flag = true;
        }
        //检查是否有敌人在追踪范围内
        else if (dist < detect_radius){
            Debug.Log("dist < detect_radius");
            if (attacking_flag)
            {
                Debug.Log("attacking_flag");
                attacking_flag = false;
                ani.SetTrigger("Idle");
                ani.SetBool("SpinAttack",false);
            }
            //调用寻路函数实现寻路移动
            MoveTo();
        }else{//否则巡逻
            Debug.Log("Patrol2");
            if (attacking_flag)
            {
                attacking_flag = false;
                ani.SetTrigger("Idle");
                ani.SetBool("SpinAttack", false);
                
            }
            Patrol2(); 
        }       
    }

    //void Patrol()
    //{
    //    if (Vector3.Distance(pathPoints[nextPathPointIndex].transform.position, transform.position) < arri_radius)
    //    {
    //        //距离小于0.1视为已到达，选中新的下一点
    //        if (nextPathPointIndex < pathPoints.Length - 1)
    //        {
    //            nextPathPointIndex++;
    //        }else{
    //            nextPathPointIndex=0;
    //        }
           
    //    }
    //    //计算新的前进方向//这里冗余了但考虑结束寻路后的返回，暂时每帧都
    //    transform.forward = pathPoints[nextPathPointIndex].transform.position - transform.position;
    //    //移动
    //    transform.Translate(Vector3.forward * patrolSpeed * Time.deltaTime, Space.Self);
    //}
    void Patrol2()
    {
        if (Vector3.Distance(pathPoints[nextPathPointIndex].transform.position, transform.position) < arri_radius)
        {
            //距离小于0.1视为已到达，选中新的下一点
            if (nextPathPointIndex < pathPoints.Length - 1)
            {
                nextPathPointIndex++;
            }
            else
            {
                nextPathPointIndex = 0;
            }

        }
        //计算新的前进方向//这里冗余了但考虑结束寻路后的返回，暂时每帧都
        m_agent.SetDestination(pathPoints[nextPathPointIndex].transform.position);
        //移动
        m_agent.Move(this.transform.TransformDirection(new Vector3(0, 0, moveSpeed * Time.deltaTime)));
    }


    //寻路函数
    void MoveTo()
    {
            //设置敌人的寻路目标
            m_agent.SetDestination(m_player.transform.position);
            //定义敌人的移动量
            //通过寻路组件的Move()方法实现寻路移动
            m_agent.Move(this.transform.TransformDirection(new Vector3(0, 0, moveSpeed * Time.deltaTime)));
    }

    //受伤死亡函数
    public void Hurt()
    {
        Debug.Log("Take Damage");
        ani.SetTrigger("Take Damage");
        hurt_cnt++;
        if (hurt_cnt >= life)
        {
            //播放死亡动画
            ani.SetBool("NoHealth",true);
            //在Update里判断每状态，适时消失

            //Add Scores when enemy down
            Score.x += 2;
            // Score.text.text = "Current Score: " + Score.x;
        }

       

    }


}
