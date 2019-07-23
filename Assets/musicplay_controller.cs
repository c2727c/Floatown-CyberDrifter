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
        music3.Play();
    }

    public void ClimbingMusicPlay()
    {
        music4.Play();
    }
}
