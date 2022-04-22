using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    GameObject cube;
    GameObject player;
    GameObject boss;

    // エフェクト関連
    EnemyEffect enemyEffect;
    GameObject objEffect;

    // 火炎放射距離
    [Header("火炎放射の距離")][SerializeField] float fDistance = 3.0f;

    // 攻撃予測テクスチャ
    [Header("攻撃予測サークル")] [SerializeField] private GameObject Circle;

    public void SetPlayer(GameObject obj) { player = obj; }


    private void Start()
    {
        boss = this.gameObject;

        // エフェクト取得（Boss01.csより）
        enemyEffect = boss.GetComponent<Boss01>().GetEffect;
    }

    //-------------------------
    // 火柱生成
    //-------------------------
    public void CreateFire(Vector3 vpos)
    {
        // 当たり判定用キューブ
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // 当たり判定用キューブを透明に
        cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse"); ;
        cube.GetComponent<MeshRenderer>().material.color -= new Color32(255, 255, 255, 255);

        // サイズ、座標、角度設定
        cube.transform.localScale = new Vector3(1.5f, 3.0f, 1.5f);
        cube.transform.rotation = transform.rotation;
        cube.transform.position = vpos;

        // 火柱コンポーネント調整
        // すり抜ける判定にする
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // 攻撃予測サークルのサイズ設定
        Circle.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        // 火柱コンポーネント追加
        cube.AddComponent<Fire>();

        // 予測サークルセット
        cube.GetComponent<Fire>().SetCircle(Circle);

        // 敵情報セット
        cube.GetComponent<Fire>().enemy = gameObject;

        // プレイヤー情報セット
        cube.GetComponent<Fire>().player = player;
    }

    //-------------------------
    // 火炎放射生成
    //-------------------------
    public void CreateFlame(Vector3 vpos)
    {
        // 判定用キューブ生成
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // サイズ、座標、角度設定
        cube.transform.localScale = new Vector3(1.5f, 1.0f, 5.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * fDistance, transform.position.y, transform.position.z + transform.forward.z * fDistance);

        // 火炎放射コンポーネント追加
        cube.AddComponent<Flamethrower>();

        // 情報セット
        cube.GetComponent<Flamethrower>().SetEnemy(gameObject);
        cube.GetComponent<Flamethrower>().SetPlayer(player);
        cube.GetComponent<Flamethrower>().SetDiss(fDistance);

        // すり抜ける設定
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // キューブを非表示
        cube.GetComponent<MeshRenderer>().enabled = false;

        // エフェクト生成
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFlame, gameObject);
        cube.GetComponent<Flamethrower>().SetEffect(objEffect);
    }
}
