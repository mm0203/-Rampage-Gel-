//======================================================================
// Scratch.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵のひっかき攻撃処理
// 2022/03/28 author：竹尾　応急　プレイヤーへのダメージ判定
// 2022/04/21 author：小椋　敵の攻撃力をEnemyDataから参照するように変更
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : MonoBehaviour
{
    GameObject player;
    GameObject enemy;

    public void SetPlayer (GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }


    
    void Update()
    {
        Destroy(gameObject, 0.5f);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")

        {
            // ダメージ処理
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack);

            Destroy(gameObject);

        }
    }
}
