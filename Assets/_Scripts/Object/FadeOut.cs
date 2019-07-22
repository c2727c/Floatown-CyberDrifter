using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//适用于支持透明度的shader！比如LegancyLegancy/Transparent/diffuse
public class FadeOut : MonoBehaviour
{
    public float deltaAlpha = -0.02f;
    private float AlphaValue = 1.0f;//透明度
    private float time = 0.0f;//时间   
    private bool state = false;    
    void Update()    
    { 
        //每0.05秒       
        time += Time.deltaTime;        
        if (time > 0.05f)        
        {           
            time = 0;
            AlphaValue += deltaAlpha;             
            state = false;            
            if (AlphaValue <= 0||AlphaValue>=1)            
            {                
                deltaAlpha = -deltaAlpha;             
            }
            Color co =  GetComponent<MeshRenderer>().material.color;       
            GetComponent<MeshRenderer>().material.color = new Color(co.r, co.g, co.b, AlphaValue);
            //在进行交互之前判断AlphaValue，小于某值则不可得分/抓取
        }
    }

}
