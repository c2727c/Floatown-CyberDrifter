using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicplay_controller : MonoBehaviour
{
    private AudioSource[] m_MusicArray;

    private AudioSource music1, music2, music3, music4;

    public bool isWalking = false;
    public bool isClimbing = false;
    // Start is called before the first frame update
    void Start()
    {
        m_MusicArray = GetComponents<AudioSource>();
        music1 = m_MusicArray[0];
        music2 = m_MusicArray[1];
        music3 = m_MusicArray[2];
        music4 = m_MusicArray[3];
        int length = m_MusicArray.Length;
        for(int i=0;i<length&&i!=1;++i){
            m_MusicArray[i].enabled = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(isWalking)
        // {
        //     music3.Play();
        // }
        
        // if(isClimbing)
        // {
        //     music4.Play();
        // }
    }

    public void WalkingMusicPlay()
    {
        m_MusicArray[2].enabled = true;
        music3.Play();
    }

    public void ClimbingMusicPlay()
    {
        m_MusicArray[3].enabled = true;
        music4.Play();
    }
    // public void BackgroundMusicPlay_0(){
    //     m_MusicArray[4].enabled = true;
    //     m_MusicArray[4].Play();
    // }
    public void BoxHitMusicPlay()
    {
        m_MusicArray[1].enabled = true;
        music2.Play();
    }
}
