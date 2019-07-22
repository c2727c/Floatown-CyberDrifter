using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float max_health = 100;
    private float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hurt(float dh){
        health-=dh;
        if(health<0){
            //died
        }
    }
    public void recover(float dh){
        health+=dh;
        health%=max_health;
    }
}
