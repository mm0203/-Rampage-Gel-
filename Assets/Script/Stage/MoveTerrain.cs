using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTerrain : MonoBehaviour
{
    public float speed;
    float size = 2000;  //NOTE!

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(0, 0, speed);

        if (this.transform.position.z + size < 0)
        {
            Debug.Log("out of display");
            this.transform.Translate(0, 0, size * 2);
        }
    }
}
