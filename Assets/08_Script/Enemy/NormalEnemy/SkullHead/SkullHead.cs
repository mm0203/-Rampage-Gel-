//======================================================================
// SkullDragon.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵の遠距離攻撃処理
// 2022/03/30 author：小椋駿 エフェクト処理追加
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class SkullHead : MonoBehaviour
{
    // 当たり判定用スフィア
    GameObject spher;

    EnemyBase enemyBase;

    // エフェクト関連
    EnemyEffect enemyEffect;
    GameObject objEffect;

    // 火球の速度
    [Header("火球速度")]
    [SerializeField] float fSpeed = 5.0f;

    //------------------------
    // 初期化
    //------------------------
    private void Start()
    {
        // エフェクト取得
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;

        // エネミーベース情報取得
        enemyBase = this.GetComponent<EnemyBase>();
    }

    //----------------------------------------------
    // 火球処理(アニメーションに合わせて呼び出す)
    //----------------------------------------------
    private void SkullHeadAttack()
    {
        // 弾を生成
        spher = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // 弾のサイズ、座標、角度設定
        spher.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        spher.transform.rotation = this.transform.rotation;
        spher.transform.position = new Vector3(transform.position.x + transform.forward.x, transform.position.y, transform.position.z + transform.forward.z);

        // 弾にコンポーネント追加
        spher.AddComponent<Bullet>();

        // 弾のコンポーネントに情報をセット
        Bullet bullet = spher.GetComponent<Bullet>();
        bullet.Speed = fSpeed;
        bullet.SetPlayer(enemyBase.GetComponent<EnemyBase>().player);
        bullet.SetEnemy(gameObject);

        // エフェクト生成
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFireBall, gameObject);
        bullet.SetEffect(objEffect);

        // すり抜けるように
        spher.GetComponent<SphereCollider>().isTrigger = true;
    }
}
