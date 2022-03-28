using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    GameObject cube;
    GameObject player;
    GameObject boss;

    // 火炎放射距離
    [Header("火炎放射の距離")][SerializeField] float fDistance = 3.0f;

    // 攻撃予測テクスチャ
    [Header("攻撃予測サークル")] [SerializeField] private GameObject Circle;

    public void SetPlayer(GameObject obj) { player = obj; }


    private void Start()
    {
        boss = this.gameObject;
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

        // 火柱のサイズ、座標、角度設定
        cube.transform.localScale = new Vector3(1.5f, 3.0f, 1.5f);
        cube.transform.rotation = transform.rotation;
        cube.transform.position = vpos;

        // 火柱コンポーネント調整
        // すり抜ける判定にする
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // 必要な情報をセット
        Circle.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        cube.AddComponent<Fire>();
        cube.GetComponent<Fire>().SetCircle(Circle);
        cube.GetComponent<Fire>().SetEnemy(gameObject);
        cube.GetComponent<Fire>().SetPlayer(player);
    }

    public void CreateFlame(Vector3 vpos)
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // サイズ、座標、角度設定
        cube.transform.localScale = new Vector3(1.5f, 1.0f, 8.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * fDistance, transform.position.y, transform.position.z + transform.forward.z * fDistance);

        cube.AddComponent<Flamethrower>();
        cube.GetComponent<Flamethrower>().SetEnemy(gameObject);
        cube.GetComponent<Flamethrower>().SetDiss(fDistance);
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // 当たり判定用キューブを透明に(デバッグ用)
        cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse"); ;
        cube.GetComponent<MeshRenderer>().material.color -= new Color32(255, 255, 255, 255);
    }
}
