using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float radian = 0;
    public float perRadian = 0.03f;
    public float radius = 0.8f;
    Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        radian += perRadian;
        float dy = Mathf.Cos(radian) * radius;
        transform.position = oldPos += new Vector3(0, dy, 0);
    }
}
