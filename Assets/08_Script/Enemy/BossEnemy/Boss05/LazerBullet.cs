
//======================================================================
// Bullet.cs
//======================================================================
// 開発履歴
//
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBullet : MonoBehaviour
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
