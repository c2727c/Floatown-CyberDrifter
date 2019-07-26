using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToPlayMusic : MonoBehaviour
{
    public AudioSource audio;
    public string tagName="EVERYTHING";
    // Start is called before the first frame update
    public int maxTimes=1;
    void Awake()
    {
        audio.enabled = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag(tagName)||tagName=="EVERYTHING"){
            if(maxTimes>0){
            audio.enabled = true;
            audio.Play();
            maxTimes--;
            }
            
        }
    }
}
