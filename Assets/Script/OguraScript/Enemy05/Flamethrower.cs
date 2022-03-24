
//======================================================================
// Flamethrower.cs
//======================================================================
// 開発履歴
//
// 2022/03/21 author：小椋駿 製作開始　敵の火炎放射
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    // 火柱の時間
    float fLifeTime = 2.0f;

    // ダメージを与える間隔
    float fInterval = 0.5f;
    float fTime;

    GameObject player;
    GameObject enemy;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }


    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color += new Color32(255, 0, 0, 122);

 

        
    }


    void Update()
    {
        // 設定時間後、攻撃終了
        fLifeTime -= Time.deltaTime;
        if(fLifeTime <= 0.0f)
        {
            enemy.GetComponent<EnemyBase>().SetAttack(false);
            Destroy(gameObject);        
        }

        // 敵が死亡したとき、一緒に消える
        if (enemy == null)
        {
            Destroy(gameObject);
        }


    }


    //----------------------------------
    // 当たり判定
    //----------------------------------
    // 火炎放射に入った瞬間ダメージ
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // ダメージ処理
            //player.GetComponent<StatusComponent>().HP -= enemy.GetComponent<StatusComponent>().Attack;

            Debug.Log("ダメージ");

        }
    }

    // 火炎放射に当たり続けているとき
    private void OnTriggerStay(Collider other)
    {
        // プレイヤーに当たった時 & 火柱が出ているとき
        if (other.tag == "Player")
        {
            fTime -= Time.deltaTime;

            // 設定インターバル毎にダメージを与える(0.5秒)
            if (fTime < 0.0f)
            {
                // ダメージ処理
                //player.GetComponent<StatusComponent>().HP -= enemy.GetComponent<StatusComponent>().Attack;

                Debug.Log("ダメージ");
                fTime = fInterval;
            }

        }
    }
}
