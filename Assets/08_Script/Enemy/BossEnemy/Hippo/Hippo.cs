//======================================================================
// Hippo.cs
//======================================================================
// 開発履歴
//
// 2022/06/14 author：小椋駿 製作開始　
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hippo : MonoBehaviour
{
    // 攻撃の種類
    enum eHippoAttack
    {
        eFireWave = 0,

        eMax
    }

    // 攻撃関数リスト
    private List<Action> AttackList;

    GameObject Cube;
    EnemyBase enemyBase;

    float attackTime = 6.0f;

    void Start()
    {

        // エネミーベース情報取得
        enemyBase = this.GetComponent<EnemyBase>();

        AttackList = new List<Action>();
        AttackList.Add(FireWaveAttack);
    }

    private void Update()
    {
        // 攻撃終了
        if(enemyBase.bAttack)
        {
            attackTime -= Time.deltaTime;
            if(attackTime < 0)
            {
                enemyBase.bAttack = false;
                attackTime = 6.0f;
            }
        }
    }

    // 攻撃の関数をランダムに呼ぶ
    void HippoAttack()
    {
        int range = UnityEngine.Random.Range(0, (int)eHippoAttack.eMax - 1);

        AttackList[range]();
    }


    // ファイヤーウェーブ
    private void FireWaveAttack()
    {
        // 当たり判定用キューブ生成
        Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // サイズ、座標、角度設定
        Cube.transform.localScale = new Vector3(8.0f, 1.0f, 5.0f);
        Cube.transform.rotation = this.transform.rotation;
        Cube.transform.position = transform.position + transform.forward * Cube.transform.localScale.z;    // ボス前方へ生成

        // コンポーネント調整
        Cube.AddComponent<FireWave>();
        Cube.GetComponent<FireWave>().SetPlayer(enemyBase.GetComponent<EnemyBase>().player);
        Cube.GetComponent<FireWave>().SetEnemy(this.gameObject);
        Cube.GetComponent<BoxCollider>().isTrigger = true;
        enemyBase.bAttack = true;

        Cube.AddComponent<Rigidbody>();
        Cube.GetComponent<Rigidbody>().useGravity = false;
        Cube.GetComponent<Rigidbody>().isKinematic = true;

        //Cube.GetComponent<MeshRenderer>().enabled = false;
    }
}
