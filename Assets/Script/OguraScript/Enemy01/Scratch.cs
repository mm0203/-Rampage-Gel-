//======================================================================
// Scratch.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵のひっかき攻撃処理
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    MeshRenderer mr;

    public void SetPlayer (GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }


    private void Start()
    {
        // 当たり判定を透明に
        mr = GetComponent<MeshRenderer>();
        mr.material.color = new Color32(0, 0, 0, 0);
    }

    void Update()
    {
        Destroy(gameObject, 0.5f);
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
