//======================================================================
// Fire.cs
//======================================================================
// 開発履歴
//
// 2022/03/21 author：小椋駿 製作開始　火柱処理
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // 火柱の時間
    float fLifeTime = 3.0f;

    // ダメージを与える間隔
    float fInterval = 0.5f;
    float fTime;

    // 攻撃サークルが出てから火柱が出る時間
    float fAttackStart = 1.0f;

    bool bAttackStart = false;

    GameObject player;
    GameObject enemy;
    GameObject AttackCircle,TimeCircle;

    float fScale;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }
    public void SetCircle(GameObject obj) { AttackCircle = obj; }


    //----------------------------------
    // 初期化
    //----------------------------------
    private void Start()
    {
        // インターバルセット
        fTime = fInterval;

        // 広がるサークル生成
        TimeCircle = Instantiate(AttackCircle, new Vector3(player.transform.position.x, 0.1f, player.transform.position.z), AttackCircle.transform.rotation);
        // 大きさはゼロ
        TimeCircle.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);


        // 攻撃サークル生成
        AttackCircle = Instantiate(AttackCircle, new Vector3(player.transform.position.x, 0.1f, player.transform.position.z), AttackCircle.transform.rotation);
        // サークルの透明度を下げる
        AttackCircle.GetComponent<SpriteRenderer>().color -= new Color32(0, 0, 0, 125);

        // サークルの大きさ拡大
        fScale = AttackCircle.transform.localScale.x / (fAttackStart * 50.0f);
    }

    //----------------------------------
    // 更新
    //----------------------------------
    void Update()
    {
        Destroy(gameObject, fLifeTime);
        Destroy(AttackCircle, fAttackStart);
        Destroy(TimeCircle, fAttackStart);

       // 当たり判定用キューブを不透明に(デバッグ用)
        fAttackStart -= Time.deltaTime;
        if(fAttackStart < 0.0f && !bAttackStart)
        {
            gameObject.GetComponent<MeshRenderer>().material.color += new Color32(255, 255, 255, 122);
            bAttackStart = true;
        }
    }

    private void FixedUpdate()
    {
        if(TimeCircle != null)
            TimeCircle.transform.localScale = new Vector3(TimeCircle.transform.localScale.x + fScale, TimeCircle.transform.localScale.y + fScale, 1.0f);
    }

    //----------------------------------
    // 当たり判定
    //----------------------------------
    // 火柱に入った瞬間ダメージ
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player" && bAttackStart)
        {
             // ダメージ処理
             //player.GetComponent<StatusComponent>().HP -= enemy.GetComponent<StatusComponent>().Attack;

             Debug.Log("ダメージ");

        }
    }

     // 火柱に当たり続けているとき
    private void OnTriggerStay(Collider other)
    {
        // プレイヤーに当たった時 & 火柱が出ているとき
        if (other.tag == "Player" && bAttackStart)
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
