
//======================================================================
// Enemy_05.cs
//======================================================================
// 開発履歴
//
// 2022/03/28 author：竹尾　応急 エフェクト発生組み込み
//======================================================================
// Flamethrower.cs
//======================================================================
// 開発履歴
//
// 2022/03/21 author：小椋駿 製作開始　敵の火炎放射

//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_05 : MonoBehaviour
{
    GameObject cube;
    EnemyBase enemyBase;

    // エフェクト関連
    EnemyEffect enemyEffect;
    GameObject objEffect;

    [Header("火炎放射の距離")][SerializeField]float fDistance = 3.0f;

    //------------------------
    // 初期化
    //------------------------
    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();

        // エフェクト取得（EnemyBase.csより）
        enemyEffect = enemyBase.GetEffect;
    }

    //----------------------------------------------
    // 火炎放射処理(アニメーションに合わせて呼び出す)
    //----------------------------------------------
    private void AttackEnemy05()
    {
        // 当たり判定用のキューブ生成
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // サイズ、座標、角度設定
        cube.transform.localScale = new Vector3(1.0f, 1.0f, 5.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * fDistance, transform.position.y, transform.position.z + transform.forward.z * fDistance);

        // 火炎放射のコンポーネントを追加
        cube.AddComponent<Flamethrower>();

        // 情報セット
        cube.GetComponent<Flamethrower>().SetEnemy(gameObject);
        cube.GetComponent<Flamethrower>().SetPlayer(enemyBase.GetPlayer);
        cube.GetComponent<Flamethrower>().SetDiss(fDistance);

        // エフェクト生成
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFlame, gameObject);
        cube.GetComponent<Flamethrower>().SetEffect(objEffect);

        // すり抜ける判定に
        cube.GetComponent<BoxCollider>().isTrigger = true;
<<<<<<< HEAD
<<<<<<< HEAD:Assets/Script/OguraScript/Enemy05/Enemy_05.cs
        //enemy.GetComponent<EnemyBase>().SetAttack(true);
        //enemyBase.SetAttack(true);
=======
=======

        // 攻撃フラグをON（敵が動かなくなる）
>>>>>>> e2853f8ad6986fc67b6af3dfd7a583e04154f030
        enemyBase.SetAttack(true);
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/Enemy05/Enemy_05.cs

        // 当たり判定キューブを非表示
        cube.GetComponent<MeshRenderer>().enabled = false; 
    }
}
