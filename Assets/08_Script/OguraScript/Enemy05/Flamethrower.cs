
//======================================================================
// Flamethrower.cs
//======================================================================
// 開発履歴
//
// 2022/03/21 author：小椋駿 製作開始　敵の火炎放射
// 2022/03/28 author：竹尾　応急　プレイヤーへのダメージ判定（エラー）
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    // 火炎放射の時間
    float fLifeTime = 2.0f;

    // 火炎放射の長さ
    float fDis;

    // ダメージを与える間隔
    float fInterval = 0.5f;
    float fTime;

    GameObject player;
    GameObject enemy;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    public void SetDiss(float dis) { fDis = dis; }

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
<<<<<<< HEAD:Assets/Script/OguraScript/Enemy05/Flamethrower.cs
            //enemy.GetComponent<EnemyBase>().SetAttack(false);
=======
            if(enemy.GetComponent<EnemyBase>() != null)
            {
                enemy.GetComponent<EnemyBase>().SetAttack(false);
            }
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/Enemy05/Flamethrower.cs
            Destroy(gameObject);        
        }

        // 敵が死亡したとき、一緒に消える
        if (enemy == null)
        {
            Destroy(gameObject);
        }

        if(enemy != null)
        {
            // 角度、座標が追従するように
            transform.rotation = enemy.transform.rotation;
            transform.position = new Vector3(enemy.transform.position.x + transform.forward.x * fDis, transform.position.y, enemy.transform.position.z + transform.forward.z * fDis);
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

            //*応急* ダメージ処理
            //player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack); //エラー NullRefalence

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

                //*応急* (初撃より少なく) ダメージ処理
                //player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack / 10); //エラー NullRefalence

                Debug.Log("ダメージ");
                fTime = fInterval;
            }

        }
    }
}
