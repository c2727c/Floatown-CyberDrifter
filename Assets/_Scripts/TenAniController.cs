using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenAniController : MonoBehaviour
{
    public Animator ani;//获取 Animator//在Inspector 里注入依赖
    public float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {// 按下不同键触发不同动画        
        if (Input.GetKey(KeyCode.UpArrow))
        {
           MoveForward();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
           MoveBackward();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
           MoveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
           MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           ani.SetTrigger("Spawn");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
           Attack01();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
           SpinAttack();
        }
    }





    void Attack01(){
       ani.SetTrigger("Attack01");
    }
    void SpinAttack(){
       ani.SetTrigger("SpinAttack");
    }
    void MoveForward(){
       ani.SetTrigger("Forward");
       transform.position+=Vector3.forward*speed;//Vector3.forward就是z轴正向
    }
    void MoveBackward(){
       ani.SetTrigger("Backward");
       transform.position+=Vector3.back*speed;//Vector3.forward就是z轴正向
    }
    void MoveLeft(){
       ani.SetTrigger("Left");
       transform.position+=Vector3.left*speed;//Vector3.forward就是z轴正向
    }
    void MoveRight(){
       ani.SetTrigger("Right");
       transform.position+=Vector3.right*speed;//Vector3.forward就是z轴正向
    }


    void MoveDirection(Vector3 direction){
       //ani.SetTrigger("Forward");
       //transform.position+=direction*speed;
      // transform.rotation = direction;
    }
}