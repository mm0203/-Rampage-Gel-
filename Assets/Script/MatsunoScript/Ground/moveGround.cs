using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGround : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Transform a = this.transform;
        Vector3 point = a.position;
        point.x = Camera.main.transform.position.x;
        point.y = 0f;
        point.z = Camera.main.transform.position.z;

        a.position = point;
    }
}
