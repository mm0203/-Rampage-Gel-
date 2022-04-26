using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmor : MonoBehaviour
{
    private PlayerStatus status;
    private float fTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if(status.bArmor)
        {
            fTime += Time.deltaTime;
            if(fTime >= status.fArmorTime)
            {
                status.bArmor = false;
                fTime = 0.0f;
            }
        }
    }
}
