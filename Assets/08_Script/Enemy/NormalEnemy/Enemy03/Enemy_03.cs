//======================================================================
// Enemy_03.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_03 : MonoBehaviour
{
    GameObject spher;
    EnemyBase enemyBase;

    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    private void AttackEnemy03()
    {
        // 当たり判定用スフィア生成
        spher = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // サイズ、座標、角度設定
        spher.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        spher.transform.rotation = this.transform.rotation;
        spher.transform.position = transform.position;

        // コンポーネント調整
        spher.AddComponent<Rush>();
        spher.GetComponent<Rush>().SetPlayer(enemyBase.GetComponent<EnemyBase>().GetPlayer);
        spher.GetComponent<Rush>().SetEnemy(this.gameObject);
        spher.GetComponent<BoxCollider>().isTrigger = true;
            
        spher.AddComponent<Rigidbody>();
        spher.GetComponent<Rigidbody>().useGravity = false;
        spher.GetComponent<Rigidbody>().isKinematic = true;

        spher.GetComponent<MeshRenderer>().enabled = false;
    }
}
