//======================================================================
// Enemy_02.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵の遠距離攻撃処理
//
//======================================================================



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Enemy_02 : MonoBehaviour
{
    GameObject spher;
    EnemyBase enemyBase;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    private void AttackEnemy02()
    {
        // 弾を生成して飛ばす
        spher = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // 弾のサイズ、座標、角度設定
        spher.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        spher.transform.rotation = this.transform.rotation;
        spher.transform.position = new Vector3(transform.position.x + transform.forward.x, transform.position.y, transform.position.z + transform.forward.z);

        // 弾のコンポーネント調整
        spher.AddComponent<Bullet>();
        spher.GetComponent<Bullet>().Speed = 3.0f;
        spher.GetComponent<Bullet>().SetPlayer(enemyBase.GetComponent<EnemyBase>().GetPlayer);
        spher.GetComponent<Bullet>().SetEnemy(this.gameObject);
        spher.AddComponent<Rigidbody>();
        spher.GetComponent<Rigidbody>().useGravity = false;
        spher.GetComponent<Rigidbody>().isKinematic = true;
    }
}
