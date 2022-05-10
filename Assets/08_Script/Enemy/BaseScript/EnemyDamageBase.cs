//======================================================================
// EnemyDamageBase.cs
//======================================================================
// 開発履歴
//
// 2022/05/02 author：小椋駿 製作開始　敵ダメージ処理を移行。
// 2022/05/05 author：竹尾　プレイヤーの速度に対してダメージ出せるように

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageBase : MonoBehaviour
{
    // 初期マテリアル保存用
    Material TmpMat;

    // 子オブジェクト保存用
    SkinnedMeshRenderer[] renders;

    // ダメージフラグ
    bool bDamage = false;

    GameObject player;

    [Header("DamegeUI")]
    [SerializeField] private GameObject DamageObj;

    [Header("ダメージ時に変更するマテリアル")]
    [SerializeField] private Material mat;

    [Header("変化する時間")]
    [SerializeField] private float fDamageTime = 0.2f;
    float fDamageCount;

    // 速度に対するダメージ補正
    float fSpeedtoDamage = 0.03f;

    //--------------------------
    // 初期化
    //--------------------------
    void Start()
    {
        // SkinnedMeshRenderer持ちの子オブジェクト取得
        renders = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var meshRenderer in renders)
        {
            // 最初のマテリアルを保存
            TmpMat = meshRenderer.material;
        }

        // カウント初期化
        fDamageCount = fDamageTime;

        // プレイヤー取得
        player = gameObject.GetComponent<EnemyBase>().player;
    }

    //--------------------------
    // 更新
    //--------------------------
    void Update()
    {
        // ダメージを受けていなければスキップ
        if (!bDamage) return;

        // ダメージカウント減少
        fDamageCount -= Time.deltaTime;
        

        if(fDamageCount < 0.0f)
        {
            // 初期化
            fDamageCount = fDamageTime;
            bDamage = false;

            foreach (var meshRenderer in renders)
            {
                // 最初のマテリアルに戻す
                meshRenderer.material = TmpMat;
            }
        }
    }

    //----------------------------
    // プレイヤーとの接触時
    //----------------------------
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーとの衝突時ダメージ
        if (other.CompareTag("Player"))
        {
            // ダメージ処理
            int n = (int)((player.GetComponent<PlayerStatus>().Attack * (int)player.GetComponent<Rigidbody>().velocity.magnitude) * fSpeedtoDamage);
            gameObject.GetComponent<EnemyBase>().nHp -= n;
            ViewDamage(n);

            // 一瞬色を変える
            ChangeMaterial();
        }
    }

    //----------------------------
    // ダメージ処理
    //----------------------------
    public void IsDamage(int damage)
    {
        // ダメージ処理
        gameObject.GetComponent<EnemyBase>().nHp -= damage;
        ViewDamage(damage);

        // 一瞬色を変える
        ChangeMaterial();
    }

    //----------------------------
    // ダメージ表記
    //----------------------------
    private void ViewDamage(int damage)
    {
        // テキストの生成
        GameObject text = Instantiate(DamageObj);
        text.GetComponent<TextMesh>().text = damage.ToString();

        // 少しずらした位置に生成(z + 1.0f)
        text.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.0f);
    }

    //--------------------------
    // マテリアル変更処理
    //--------------------------
    public void ChangeMaterial()
    {
        foreach (var meshRenderer in renders)
        {
            // 指定したマテリアルに変更
            meshRenderer.material = mat;

            // ダメージフラグON
            bDamage = true;
        }
    }
}
