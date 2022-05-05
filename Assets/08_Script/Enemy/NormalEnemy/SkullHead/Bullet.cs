
//======================================================================
// Bullet.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵の遠距離攻撃処理
// 2022/03/28 author：竹尾　応急　プレイヤーへのダメージ判定
// 2022/03/30 author：小椋　エフェクト処理の追加
// 2022/04/21 author：小椋　敵の攻撃力をEnemyDataから参照するように変更
// 2022/05/05 author：竹尾　発射した本人がいなくても攻撃力がわかるように
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
    GameObject effect;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    public void SetEffect(GameObject obj) { effect = obj; }

    private int nSetAttack;


    //---------------------------
    // 初期化
    //---------------------------
    private void Start()
    {
        // エフェクトを180°回転させる
        transform.Rotate(transform.rotation.x, transform.rotation.y + 180.0f, transform.rotation.z);
        effect.transform.rotation = transform.rotation;

        // 発射した本人がいなくても攻撃力がわかるように
        nSetAttack = enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack;
    }


    //---------------------------
    // 更新
    //---------------------------
    void Update()
    {
        // 前方へ飛ばす(エフェクトを回転させたため、「-」を付けて計算する)
        transform.position += -transform.forward * Time.deltaTime * Speed;
        effect.transform.position = transform.position;

        Destroy(effect, 3.0f);
        Destroy(gameObject, 3.0f);
    }


    //--------------------------------
    // プレイヤーとの接触時ダメージ
    //--------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            // ダメージ処理
            player.GetComponent<PlayerHP>().OnDamage(nSetAttack);

            Destroy(effect);
            Destroy(gameObject);
        }
    }

}
