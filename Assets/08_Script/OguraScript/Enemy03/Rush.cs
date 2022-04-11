//======================================================================
// Rush.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　
// 2022/03/28 author：竹尾　応急　プレイヤーへのダメージ判定
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    void Update()
    {
        // 敵死亡時、当たり判定用のキューブも消える
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        // 突進処理
        enemy.transform.position += enemy.transform.forward * (Time.deltaTime * 5.0f);

        // 当たり判定キューブも敵と一緒に動く
        transform.position = new Vector3(enemy.transform.position.x + enemy.transform.forward.x,
                                         enemy.transform.position.y,
                                         enemy.transform.position.z + enemy.transform.forward.z);

        // 一秒で消滅
        Destroy(gameObject, 1.0f);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // ダメージ処理
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack);

            Destroy(gameObject);
        }
    }


}
