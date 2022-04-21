
//======================================================================
// Flamethrower.cs
//======================================================================
// 開発履歴
//
// 2022/03/21 author：小椋駿 製作開始　敵の火炎放射
// 2022/03/28 author：竹尾　応急　プレイヤーへのダメージ判定（エラー）
// 2022/04/21 author：小椋　敵の攻撃力をEnemyDataから参照するように変更
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    // 火炎放射の時間
    float fLifeTime = 3.0f;

    // 火炎放射の長さ
    float fDis;

    // ダメージを与える間隔
    float fInterval = 0.5f;
    float fTime;

    GameObject player;
    GameObject enemy;
    GameObject effect;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    public void SetDiss(float dis) { fDis = dis; }

    public void SetEffect(GameObject obj) { effect = obj; }

    //---------------------------
    // 更新
    //---------------------------
    void Update()
    {

        // 設定時間後、攻撃終了
        fLifeTime -= Time.deltaTime;
        if (fLifeTime <= 0.0f)
        {
            // 敵の攻撃フラグをおろす（移動可能状態へ）
            if (enemy.GetComponent<EnemyBase>() != null)
            {
                enemy.GetComponent<EnemyBase>().bAttack = false;
            }
            Destroy(effect);
            Destroy(gameObject);
        }

        // 敵が死亡したとき、一緒に消える
        if (enemy == null)
        {
            Destroy(effect);
            Destroy(gameObject);
        }

        // 敵が生きているとき
        if (enemy != null)
        {
            // 角度、座標が追従するように
            transform.rotation = enemy.transform.rotation;
            transform.position = new Vector3(enemy.transform.position.x + transform.forward.x * fDis, transform.position.y, enemy.transform.position.z + transform.forward.z * fDis);

            // エフェクトも追従するように
            effect.transform.rotation = transform.rotation;
            effect.transform.position = enemy.transform.position;
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
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack);
        }
    }

    // 火炎放射に当たり続けているとき
    private void OnTriggerStay(Collider other)
    {
        // プレイヤーに当たった時 & 火炎放射が出ているとき
        if (other.tag == "Player")
        {
            fTime -= Time.deltaTime;

            // 設定インターバル毎にダメージを与える(0.5秒)
            if (fTime < 0.0f)
            {
                // ダメージ処理（ダメージを少なく）
                player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack / 10);

                fTime = fInterval;
            }

        }
    }
}
