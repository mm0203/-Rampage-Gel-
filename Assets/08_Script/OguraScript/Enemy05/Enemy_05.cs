
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

        // 火炎放射コンポーネントに情報セット
        cube.GetComponent<Flamethrower>().SetEnemy(gameObject);
        cube.GetComponent<Flamethrower>().SetPlayer(enemyBase.GetPlayer);
        cube.GetComponent<Flamethrower>().SetDiss(fDistance);

        // エフェクト生成
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFlame, gameObject);
        cube.GetComponent<Flamethrower>().SetEffect(objEffect);

        cube.GetComponent<BoxCollider>().isTrigger = true;
        enemyBase.SetAttack(true);

        // 当たり判定用キューブを透明に(デバッグ用)
        cube.GetComponent<MeshRenderer>().enabled = false; // 3/28 MeshRendererをオフ
        //cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        //cube.GetComponent<MeshRenderer>().material.color -= new Color32(255, 255, 255, 255);
    }
}
