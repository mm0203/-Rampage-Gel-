using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPlayer : MonoBehaviour
{

    public GameObject player = null;
    public int nParticleNum = 9999;

    void Update()
    {
        this.gameObject.transform.position = player.transform.position;
        this.gameObject.transform.rotation = player.transform.rotation;
    }


}
