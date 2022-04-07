
//======================================================================
// Bullet.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵の遠距離攻撃処理
// 2022/03/28 author：竹尾　応急　プレイヤーへのダメージ判定
// 2022/03/30 author：小椋　エフェクト処理の追加
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

<<<<<<< HEAD
<<<<<<< HEAD:Assets/Script/OguraScript/Enemy02/Bullet.cs
    // Update is called once per frame
=======

>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/Enemy02/Bullet.cs
=======
    public void SetEffect(GameObject obj) { effect = obj; }


    //---------------------------
    // 初期化
    //---------------------------
    private void Start()
    {
        // エフェクトを180°回転させる
        transform.Rotate(transform.rotation.x, transform.rotation.y + 180.0f, transform.rotation.z);
        effect.transform.rotation = transform.rotation;
    }


    //---------------------------
    // 更新
    //---------------------------
>>>>>>> e2853f8ad6986fc67b6af3dfd7a583e04154f030
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
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack);

            Destroy(effect);
            Destroy(gameObject);
        }
    }

}
