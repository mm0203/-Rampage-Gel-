//======================================================================
// Enemy_04.cs
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

public class Enemy_04 : MonoBehaviour
{
    GameObject cube;
    EnemyBase enemyBase;
    GameObject player;

    [SerializeField] private GameObject Circle;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        player = enemyBase.GetPlayer;
    }

    private void AttackEnemy04()
    {
        // 当たり判定用キューブ
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // 当たり判定用キューブを透明に
        cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse"); ;
        cube.GetComponent<MeshRenderer>().material.color -= new Color32(255,255,255,255);

        // 火柱のサイズ、座標、角度設定
        cube.transform.localScale = new Vector3(0.5f, 3.0f, 0.5f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = player.transform.position;

        // 火柱コンポーネント調整
        // すり抜ける判定にする
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // 必要な情報をセット
        Circle.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        cube.AddComponent<Fire>();
        cube.GetComponent<Fire>().SetCircle(Circle);
        cube.GetComponent<Fire>().SetEnemy(gameObject);
        cube.GetComponent<Fire>().SetPlayer(player);

        //// 弾のコンポーネント調整
        //spher.AddComponent<Bullet>();
        //spher.GetComponent<Bullet>().Speed = 3.0f;
        //spher.GetComponent<Bullet>().SetPlayer(enemyBase.GetComponent<EnemyBase>().GetPlayer);
        //spher.GetComponent<Bullet>().SetEnemy(this.gameObject);
        //spher.AddComponent<Rigidbody>();
        //spher.GetComponent<Rigidbody>().useGravity = false;
        //spher.GetComponent<Rigidbody>().isKinematic = true;
    }
}
