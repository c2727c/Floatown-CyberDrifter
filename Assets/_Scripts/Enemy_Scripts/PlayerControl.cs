using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControl : MonoBehaviour
{

    //定义玩家的Transform
    public Transform m_transform;
    //定义玩家的角色控制器
    CharacterController m_ch;
    //定义玩家的移动速度
   public float m_movespeed = 10.0f;
    //定义玩家的重力
    public float m_gravity = 2.0f;   
    //定义玩家的生命
    public int m_life = 5;

    //定义摄像机的Transform
    Transform m_cameraTransform;
    //定义摄像机的旋转角度
    Vector3 m_cameraRotation;
    //定义摄像机的高度
    float m_cameraHeight = 1.4f;
    //定义小地图摄像机
    public Transform m_miniMap;

    //定义枪口的Transform m_muzzlepPoint;
    Transform m_muzzlePoint;
    //定义射击时,射线射到的碰撞层
    public LayerMask m_layer;
    //定义射中目标后粒子效果的Transform
    public Transform m_fx;
    //定义射击音效
    public AudioClip m_shootAudio;
    //定义射击间隔时间计时器
    float m_shootTimer = 0;

    

    // Use this for initialization
    void Start()
    {
        //获取玩家本身的Transform 赋给 m_transform
        m_transform = this.transform;
        //获取玩家本身的CharacterController组件 赋给 m_ch
        m_ch = this.GetComponent<CharacterController>();       

        //摄像机的控制的初始化
        //获取摄像机的Transform
        m_cameraTransform = Camera.main.transform;
        //定义一个三维向量用来表示摄像机位置 并把玩家的位置赋给它 设置摄像机初始位置
        Vector3 pos = m_transform.position;
        //摄像机的Y轴坐标 为 本来的坐标加上上面定义的摄像机高度
        pos.y += m_cameraHeight;
        //把修改后的摄像机坐标重新赋给m_cameraTransform
        m_cameraTransform.position = pos;
        //把主角的旋转角度 赋给 摄像机的旋转角度
        m_cameraTransform.rotation = m_transform.rotation;
        //获取摄像机的角度
        m_cameraRotation = m_transform.eulerAngles;        

        //隐藏鼠标
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //如果玩家的生命小于等于0 什么也不做
        if (m_life <= 0)
        {
            return;
        }

        //如果玩家的生命大于0 那么调用玩家控制函数 
        //移动函数
        MoveControl();
        //摄像机控制函数
        CameraControl();
        //跳跃函数
        Jump();
    }


    //定义玩家的控制函数
    void MoveControl()
    {

        //定义玩家在XYZ轴上的移动量
        float xm = 0, ym = 0, zm = 0;

        //玩家的重力运动 为 减等于玩家的重力乘以每帧时间
        ym -= m_gravity * Time.deltaTime;

        //实现玩家上下左右的运动
        //如果按下 W键 玩家在Z轴上的量增加
        if (Input.GetKey(KeyCode.W))
        {
            zm += m_movespeed * Time.deltaTime;
        }
        //如果按下 S键 玩家在Z轴上的量减少  这里用else if是因为每帧只能按下相反方向的一个键
        else if (Input.GetKey(KeyCode.S))
        {
            zm -= m_movespeed * Time.deltaTime;
        }
        //如果按下 A键 玩家在X轴上的量减少
        if (Input.GetKey(KeyCode.A))
        {
            xm -= m_movespeed * Time.deltaTime;
        }
        //如果按下 D键 玩家在X轴上的量增加
        else if (Input.GetKey(KeyCode.D))
        {
            xm += m_movespeed * Time.deltaTime;
        }

        ////当玩家在地面上的时候 才能前后左右移动  在空中不能移动
        if (!m_ch.isGrounded)
        {
            xm = 0;
            zm = 0;
        }

        //通过角色控制器的Move()函数,实现移动
        m_ch.Move(m_transform.TransformDirection(new Vector3(xm, ym, zm))); 
        

    }


    //定义玩家的摄像机控制函数
    void CameraControl()
    {

        //实现对摄像机的控制
        //定义主角在horizon方向X轴移动的量  也就是获取主角鼠标移动的量
        float rh = Input.GetAxis("Mouse X");
        //定义主角在Vertical 方向Y轴移动的量  
        float rv = Input.GetAxis("Mouse Y");


        //旋转摄像机
        //把鼠标在屏幕上移动的量转化为摄像机的角度  rv(上下移动的量) 等于 角色X轴的角度    rh(水平移动的量) 等于 角色Y轴上的角度
        m_cameraRotation.x -= rv;
        //Debug.Log(rv);  向下时 rv 为正值(顺时针)   向上时 rv 为负值(逆时针)
        m_cameraRotation.y += rh;
        //Debug.Log(rh);  向右时 rh 为正值(顺时针)   向左时 rh 为负值(逆时针)


        //限制X轴的移动在-60度到60度之间
        if (m_cameraRotation.x >= 60)
        {
            m_cameraRotation.x = 60;
        }
        if (m_cameraRotation.x <= -60)
        {
            m_cameraRotation.x = -60;
        }
        m_cameraTransform.eulerAngles = m_cameraRotation;

        //使主角的面向方向与摄像机一致  用Vector3定义一个中间变量是因为 eularAngles 无法直接作为变量
        Vector3 camrot = m_cameraTransform.eulerAngles;
        //初始化摄像机的欧拉角为0
        camrot.x = 0;
        camrot.z = 0;
        //把摄像机的欧拉角 赋给 主角
        m_transform.eulerAngles = camrot;

        //使摄像机的位置与主角一致  用Vector3定义一个中间变量是因为 position 无法直接作为变量
        Vector3 pos = m_transform.position;
        //摄像机的Y轴位置 为 主角的Y轴位置加上摄像机的高度
        pos.y += m_cameraHeight;
        //把主角的位置 赋给 摄像机的位置
        m_cameraTransform.position = pos;

    }


    //定义玩家的Jump函数
    void Jump()
    {
        //当玩家在地面上的时候  玩家的i跳才有效果
        if (m_ch.isGrounded)
        {
            //此时玩家的重力为10
            m_gravity = 10;
            //如果按下 space键 玩家的重力变为负数  实现向上运动
            if (Input.GetKey(KeyCode.Space))
            {
                m_gravity = -8;
            }
        }
        //此时玩家跳了起来
        else
        {
            //玩家的重力 为 玩家的重力10 乘以 每帧的时间
            m_gravity +=10f*Time.deltaTime;
            //如果玩家的重力大于10的话 让他等于10
            if (m_gravity>=10)
            {
                m_gravity = 10f;
            }
        }
    
    }
}