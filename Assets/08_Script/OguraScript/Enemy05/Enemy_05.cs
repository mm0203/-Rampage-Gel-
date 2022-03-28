<<<<<<< HEAD:Assets/08_Script/OguraScript/Enemy05/Enemy_05.cs
//======================================================================
// Enemy_05.cs
//======================================================================
// 開発履歴
//
// 2022/03/28 author：竹尾　応急 エフェクト発生組み込み
=======

//======================================================================
// Flamethrower.cs
//======================================================================
// 開発履歴
//
// 2022/03/21 author：小椋駿 製作開始　敵の火炎放射
>>>>>>> 8709684d4e54354a91684949987394adf606b0ff:Assets/Script/OguraScript/Enemy05/Enemy_05.cs
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_05 : MonoBehaviour
{
    GameObject cube;
    EnemyBase enemyBase;

<<<<<<< HEAD:Assets/08_Script/OguraScript/Enemy05/Enemy_05.cs
    //*応急* エフェクトスクリプト
    [SerializeField] AID_PlayerEffect effect;

=======
    // 火炎放射距離
>>>>>>> 8709684d4e54354a91684949987394adf606b0ff:Assets/Script/OguraScript/Enemy05/Enemy_05.cs
    float fDistance = 3.0f;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        effect = GameObject.FindWithTag("AID_Effect").GetComponent<AID_PlayerEffect>();
    }


    private void AttackEnemy05()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //*応急*
        effect.StartEffect(5, this.gameObject, 2.0f);


        // 弾のサイズ、座標、角度設定
        cube.transform.localScale = new Vector3(1.0f, 1.0f, 5.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * fDistance, transform.position.y, transform.position.z + transform.forward.z * fDistance);

        cube.AddComponent<Flamethrower>();
        cube.GetComponent<Flamethrower>().SetEnemy(gameObject);
        cube.GetComponent<Flamethrower>().SetDiss(fDistance);
        cube.GetComponent<BoxCollider>().isTrigger = true;
        enemyBase.SetAttack(true);

        // 当たり判定用キューブを透明に(デバッグ用)
        cube.GetComponent<MeshRenderer>().enabled = false; // 3/28 MeshRendererをオフ
        cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        cube.GetComponent<MeshRenderer>().material.color -= new Color32(255, 255, 255, 255);
    }
}
