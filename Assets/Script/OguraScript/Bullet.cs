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

    // Update is called once per frame
    void Update()
    {
        // 前方へ飛ばす
        transform.position += transform.forward * Time.deltaTime * Speed;

        Destroy(gameObject, 3.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // ダメージ処理
            //player.GetComponent<StatusComponent>().HP -= enemy.GetComponent<StatusComponent>().Attack;
            //Debug.Log(player.GetComponent<StatusComponent>().HP);
        }

        Destroy(gameObject);
    }

    
}
