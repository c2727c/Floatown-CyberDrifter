using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour//该脚本赋给玩家的剑
{
    private AudioSource[] m_ArrayMusic;
    private AudioSource[] m_ArrayMusic0;
    private AudioSource m_music1;
    private AudioSource m_music2;

    public GameObject musicplayer;
    private AudioSource m_music3;
    float energy = 0;

    public Transform stopT;

    public SpawnPoint spawnPoint;   //to invoke function from SpawnPoint script
    public GameObject player;   //to initialize the preSpawnPosition
    public Vector3 preSpawnPosition;


    void Start()
    {
        m_ArrayMusic = GetComponents<AudioSource>();
        m_ArrayMusic0 = musicplayer.GetComponents<AudioSource>();
        m_music1 = m_ArrayMusic[0];
        m_music2 = m_ArrayMusic0[0];
        m_music3 = m_ArrayMusic0[1];

        preSpawnPosition = player.transform.position;
    }
    void Update()
    {
        m_music1.Play();
        if(Input.GetMouseButtonDown(0)){

        }
    }
    void OnTriggerEnter(Collider other)
        //The GameObject with Collider.isTrigger set to true has OnTriggerEnter called when the other GameObject touches or passes through it. 
        //OnTriggerEnter occurs after FixedUpdate ends.
        //Either of the two GameObjects must have a Rigidbody component.The Rigidbody component has both Rigidbody.useGravity and Rigidbody.
        //isKinematic set to false. These prevents the GameObject from falling under gravity and having kinematic behavior. 
        //应该是这个other设置isTrigger，而我们主体不一定
    {
        Debug.Log("OnTriggerEnter");
        if(other.gameObject.CompareTag("Enemy"))//碰到敌人
        {
            Debug.Log("OnTriggerEnter--isEnemy");
            EnemyBehavior enemy = other.gameObject.GetComponent<EnemyBehavior>();
            enemy.Hurt();//敌人受伤

            m_music2.Play();
        }
        if(other.gameObject.CompareTag("PickUp"))//碰到能量盒
        {
            m_music3.Play();
            Debug.Log("OnTriggerEnter--isPickUp");
            EnergyBox energyBox = other.gameObject.GetComponent<EnergyBox>();
            //光剑亮度增加
            //energy+=energyBox.energy;
            //this.renderer.material.SetColor("_EmissionColor", new Color(0.3f, 0.4f, 0.6f, 0.3f));
            //能量盒消失
            energyBox.ExplodeDisappear();
            Score.x += 1;
            // Score.text = "Current Score: " + Score.x;
        }
        if(other.gameObject.CompareTag("Vehicle"))//碰到飞行器等
        {
            Debug.Log("OnTriggerEnter--isVehicle");
            MovingPlatformEvents mpEvents = other.gameObject.GetComponent<MovingPlatformEvents>();
            mpEvents.RunTo(stopT);

        }

        if(other.gameObject.CompareTag("SpawnPoint"))
        {
            Debug.Log("OnTriggerEnter--isSpawnPoint");
            other.gameObject.SetActive(false);
            //更新preSpawnPosition
            preSpawnPosition = other.gameObject.transform.position;
            Debug.Log("The spawn position is " + preSpawnPosition);
        }

        if(other.gameObject.CompareTag("DeathHeight"))
        {
            Debug.Log("OnTriggerEnter--isDeathHeight");
            spawnPoint.toPreSpawnPoint();
        }
    }
}
