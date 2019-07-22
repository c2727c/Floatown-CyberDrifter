using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBodyCollider : MonoBehaviour
{
    // Start is called before the first frame update
    Transform bodytrans;
    public float ypara = 0.4f;
    void Start()
    {
        //bodytrans = transform.Find("[VRTK][AUTOGEN][BodyColliderContainer]");
        //bodytrans.localScale = new Vector3(1.0f, ypara, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        bodytrans = transform.Find("[VRTK][AUTOGEN][BodyColliderContainer]");
        bodytrans.localScale = new Vector3(1.0f, ypara, 1.0f);
        
    }
}
