using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void DoStop(Rigidbody rb)
    {
        // ��~�\�Ȃ��~����
        rb.velocity *= 0;
    }
}