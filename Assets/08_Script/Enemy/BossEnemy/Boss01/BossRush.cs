//======================================================================
// BossRushcs
//======================================================================
// 開発履歴
//
// 2022/04/15 author：松野将之　ボスのダメージ処理
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRush : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    Rigidbody rb;

    void Start()
    {
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // ダメージ処理
            //player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack);

            //Vector3 vec = new Vector3(Mathf.Abs(enemy.transform.forward.x * 10), 0.0f, Mathf.Abs(enemy.transform.forward.z * 10));
            //Debug.Log(vec);

            //rb.AddForce(0,0,100);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // ダメージ処理
            //player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack);

            //Vector3 vec = new Vector3(Mathf.Abs(enemy.transform.forward.x * 10), 0.0f, Mathf.Abs(enemy.transform.forward.z * 10));
            //Debug.Log(vec);

            rb.AddForce(0, 0, 100);

        }
    }
}
