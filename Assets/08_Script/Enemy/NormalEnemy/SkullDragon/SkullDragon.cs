
//======================================================================
// SkullDragon.cs
//======================================================================
// 開発履歴
//
// 2022/03/28 author：竹尾　応急 エフェクト発生組み込み

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullDragon : MonoBehaviour
{
    GameObject cube;
    EnemyBase enemyBase;

    // エフェクト関連
    EnemyEffect enemyEffect;
    GameObject objEffect;

    [Header("火炎放射の距離")]
    [SerializeField] float fDistance = 3.0f;

    [Header("火炎放射の時間")]
    [SerializeField] float fFlameTime = 1.0f;

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
    private void SkullDragonAttack()
    {
        // 当たり判定用のキューブ生成
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // サイズ、座標、角度設定
        cube.transform.localScale = new Vector3(1.0f, 1.0f, 5.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * fDistance, transform.position.y, transform.position.z + transform.forward.z * fDistance);

        // 火炎放射のコンポーネントを追加
        cube.AddComponent<Flamethrower>();

        // 敵セット
        cube.GetComponent<Flamethrower>().enemy = gameObject;

        // プレイヤーセット
        cube.GetComponent<Flamethrower>().player = enemyBase.player;

        // 火炎放射距離セット
        cube.GetComponent<Flamethrower>().fDis = fDistance;

        // エフェクト生成
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFlame, gameObject);

        // エフェクトセット
        cube.GetComponent<Flamethrower>().effect = objEffect;

        // すり抜ける判定に
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // 攻撃フラグをON（敵が動かなくなる）
        enemyBase.bAttack = true;

        // 当たり判定キューブを非表示
        cube.GetComponent<MeshRenderer>().enabled = false;

        // 火炎放射時間設定
        cube.GetComponent<Flamethrower>().fLifeTime = fFlameTime;
    }
}
