
//======================================================================
// Bullet.cs
//======================================================================
// J­π
//
// 2022/03/05 authorF¬Έx »μJn@GΜ£U
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed { get; set;  }

    GameObject player;
    GameObject enemy;
    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }


    void Update()
    {
        // OϋΦςΞ·
        transform.position += transform.forward * Time.deltaTime * Speed;

        Destroy(gameObject, 3.0f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            // _[W
            //player.GetComponent<StatusComponent>().HP -= enemy.GetComponent<StatusComponent>().Attack;
            //Debug.Log(player.GetComponent<StatusComponent>().HP);
            Destroy(gameObject);
        }
    }

}
