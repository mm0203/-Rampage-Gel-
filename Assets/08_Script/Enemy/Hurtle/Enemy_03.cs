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
    // 当たり判定用キューブ
    GameObject cube;

    EnemyBase enemyBase;

    //-----------------------
    // 初期化
    //-----------------------
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    //-----------------------
    // 更新
    //-----------------------
    private void HurtleAttack()
    {
        // 当たり判定用キューブ生成
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // キューブ非表示
        cube.GetComponent<MeshRenderer>().enabled = false;

        // サイズ、座標、角度設定
        cube.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = transform.position;

        // コンポーネント調整
        cube.AddComponent<Rush>();
        cube.GetComponent<Rush>().SetPlayer(enemyBase.GetComponent<EnemyBase>().GetPlayer);
        cube.GetComponent<Rush>().SetEnemy(this.gameObject);
        cube.GetComponent<BoxCollider>().isTrigger = true;
    }
}
