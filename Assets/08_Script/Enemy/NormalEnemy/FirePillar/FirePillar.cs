//======================================================================
// FirePillar.cs
//======================================================================
// 開発履歴
//
// 2022/03/21 author：小椋駿 製作開始　敵の火柱攻撃
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class FirePillar : MonoBehaviour
{
    // 当たり判定用キューブ
    GameObject cube;

    EnemyBase enemyBase;
    GameObject player;

    // エフェクト関連
    EnemyEffect enemyEffect;
    GameObject objEffect;

    [SerializeField] private GameObject Circle;

    // サークルサイズ
    [Header("予測サークルのサイズ")] [SerializeField] private Vector3 vCircleSize = new Vector3(0.5f, 0.5f, 0.5f);

    // 火柱サイズ
    [Header("火柱サイズ")] [SerializeField] private Vector3 vFireSize = new Vector3(0.5f, 3.0f, 0.5f);

    private void Start()
    {
        // エフェクト取得
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;

        // エネミーベース情報取得
        enemyBase = this.GetComponent<EnemyBase>();

        // プレイヤー取得
        player = enemyBase.player;
    }

    private void FirePillarAttack()
    {
        // 当たり判定用キューブ
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // 当たり判定用キューブを非表示
        cube.GetComponent<MeshRenderer>().enabled = false;

        // 火柱のサイズ、座標、角度設定
        cube.transform.localScale = vFireSize;
        transform.Rotate(-90.0f,0.0f,0.0f);
        cube.transform.position = player.transform.position;

        // 火柱コンポーネント調整
        // すり抜ける判定にする
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // 必要な情報をセット
        Circle.transform.localScale = vCircleSize;

        // Fire.cs追加
        cube.AddComponent<Fire>();

        // 攻撃サークルセット
        cube.GetComponent<Fire>().SetCircle(Circle);

        // 敵情報セット
        cube.GetComponent<Fire>().enemy = gameObject;
    
        // プレイヤー情報セット
        cube.GetComponent<Fire>().player = player;

        // プレイヤー情報セット
        cube.GetComponent<Fire>().effect = enemyEffect;
    }
}
