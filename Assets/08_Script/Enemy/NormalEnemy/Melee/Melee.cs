
//======================================================================
// Melee.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵のひっかき攻撃処理
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Melee : MonoBehaviour
{
    // 当たり判定キューブ
    GameObject cube;

    EnemyBase enemyBase;
    BossBase bossBase;

    // エフェクト関連
    EnemyEffect enemyEffect;
    GameObject objEffect;

    // 当たり判定サイズ
    private float fHitSize;

    // こいつはボス？
    public bool bImBoss = false;

    //------------------------
    // 初期化
    //------------------------
    private void Start()
    {
        // エフェクト取得
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;

        
        enemyBase = this.GetComponent<EnemyBase>();

        // 当たり判定のサイズを敵の大きさの半分にする
        fHitSize = gameObject.transform.localScale.x / 2;
    }

    //----------------------------------------------
    // ひっかき処理(アニメーションに合わせて呼び出す)
    //----------------------------------------------
    private void MeleeAttack()
    {
        // 当たり判定用キューブ生成
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // キューブを非表示に
        cube.GetComponent<MeshRenderer>().enabled = false;

        // キューブのサイズ、回転、位置を設定
        cube.transform.localScale = new Vector3(fHitSize, 1.0f, fHitSize);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * 1.5f, transform.position.y, transform.position.z + transform.forward.z * 1.5f);
        
        // Scratchコンポーネント追加
        cube.AddComponent<Scratch>();

        // 情報をセット （ボスであるかどうか）
        cube.GetComponent<Scratch>().SetPlayer(enemyBase.player);

        cube.GetComponent<Scratch>().SetEnemy(this.gameObject);

        // その他コンポーネント調整
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // エフェクト生成
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eScratch, gameObject,1.5f);
    }
}
