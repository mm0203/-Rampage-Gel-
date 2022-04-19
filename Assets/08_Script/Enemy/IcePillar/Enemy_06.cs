//======================================================================
// Enemy_06.cs
//======================================================================
// 開発履歴
//
// 2022/04/15 author：小椋駿 敵追加
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_06 : MonoBehaviour
{
    GameObject cube;
    EnemyBase enemyBase;
    GameObject player;

    [SerializeField] private GameObject Circle;

    // サークルサイズ
    [Header("予測サークルのサイズ")][SerializeField] private Vector3 vCircleSize = new Vector3(0.5f, 0.5f, 0.5f);

    // 氷柱サイズ
    [Header("氷柱サイズ")][SerializeField] private Vector3 vIceSize = new Vector3(0.5f, 3.0f, 0.5f);


    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        player = enemyBase.GetPlayer;
    }

    private void IcePillarAttack()
    {
        // 当たり判定用キューブ
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // 当たり判定用キューブの色
        cube.GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, 255);
        cube.GetComponent<MeshRenderer>().enabled = false;

        // 氷柱のサイズ、座標、角度設定
        cube.transform.localScale = vIceSize;
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = player.transform.position;

        // すり抜ける判定にする
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // 必要な情報をセット
        // 予測サークルのサイズ変更
        Circle.transform.localScale = vCircleSize;
        cube.AddComponent<Ice>();
        cube.GetComponent<Ice>().SetCircle(Circle);
        cube.GetComponent<Ice>().SetEnemy(gameObject);
        cube.GetComponent<Ice>().SetPlayer(player);
    }
}
