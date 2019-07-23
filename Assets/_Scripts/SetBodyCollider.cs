using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBodyCollider : MonoBehaviour
{
    // Start is called before the first frame update
    Transform bodytrans;
    public float ypara = 0.4f;
    bool flag = true;
    void Start()
    {
        bodytrans = transform.Find("[VRTK][AUTOGEN][BodyColliderContainer]");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flag){
        bodytrans = transform.Find("[VRTK][AUTOGEN][BodyColliderContainer]");
        bodytrans.gameObject.tag="Player";
        flag = false;

        }
        
        //bodytrans = transform.Find("[VRTK][AUTOGEN][BodyColliderContainer]");
        
        
    }
}
