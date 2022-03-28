
//======================================================================
// Bullet.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵の遠距離攻撃処理
// 2022/03/28 author：竹尾　応急　プレイヤーへのダメージ判定
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
    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }


    void Update()
    {
        // 前方へ飛ばす
        transform.position += transform.forward * Time.deltaTime * Speed;

        Destroy(gameObject, 3.0f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            // ダメージ処理
            //player.GetComponent<StatusComponent>().HP -= enemy.GetComponent<StatusComponent>().Attack;
            //Debug.Log(player.GetComponent<StatusComponent>().HP);
<<<<<<< HEAD:Assets/08_Script/OguraScript/Enemy02/Bullet.cs

            //*応急*
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack);
=======
            Destroy(gameObject);
>>>>>>> 8709684d4e54354a91684949987394adf606b0ff:Assets/Script/OguraScript/Enemy02/Bullet.cs
        }
    }

}
