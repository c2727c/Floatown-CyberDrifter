using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Text text;
    public static float x = 0 ;
    // Update is called once per frame
    void Update()
    {
        Text t = GetComponent<Text>();
        t.text = "Current Score: " + x;
    }
}
