//======================================================================
// IcePillar.cs
//======================================================================
// 開発履歴
//
// 2022/04/21 author：小椋駿 製作開始
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class IcePillar : MonoBehaviour
{
    // 当たり判定用キューブ
    GameObject cube;

    EnemyBase enemyBase;
    GameObject player;

    // 攻撃予測サークル
    [SerializeField] private GameObject Circle;

    [Header("予測サークルのサイズ")]
    [SerializeField] private Vector3 vCircleSize = new Vector3(0.5f, 0.5f, 0.5f);

    [Header("氷柱サイズ")]
    [SerializeField] private Vector3 vFireSize = new Vector3(0.5f, 3.0f, 0.5f);

    [Header("氷柱時間")]
    [SerializeField] private float fIceTime = 6.0f;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        player = enemyBase.player;
    }

    private void IcePillarAttack()
    {
        // 当たり判定用キューブ
        cube = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        // 氷柱のサイズ、座標、角度設定
        cube.transform.localScale = vFireSize;
        transform.Rotate(-90.0f, 0.0f, 0.0f);
        cube.transform.position = player.transform.position;

        // 攻撃サークルの大きさ変更
        Circle.transform.localScale = vCircleSize;

        // キューブに当たらないように
        cube.GetComponent<CapsuleCollider>().isTrigger = true;

        // キューブを非表示
        cube.GetComponent<MeshRenderer>().enabled = false;

        // Ice.cs追加
        cube.AddComponent<Ice>();

        // 攻撃サークルセット
        cube.GetComponent<Ice>().SetCircle(Circle);

        // 敵情報セット
        cube.GetComponent<Ice>().enemy = gameObject;

        // プレイヤー情報セット
        cube.GetComponent<Ice>().player = player;

        // 氷柱の生存時間設定
        cube.GetComponent<Ice>().fLifeTime = fIceTime;
    }
}

