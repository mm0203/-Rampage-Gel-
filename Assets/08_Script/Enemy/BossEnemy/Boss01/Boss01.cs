//======================================================================
// BossBase.cs
//======================================================================
// 開発履歴
//
// 2022/03/27 author：小椋駿 製作開始　ボスベース処理
// 2022/03/28 author：竹尾　応急　ポータル出現、リスト消去機能コメントアウト
// 2022/04/16 author：松野将之 設計変更 コンポーネントを細分化
// 2022/05/09 author：竹尾　本来のワーム挙動へ変更
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]

public class Boss01 : MonoBehaviour
{
    // ボスの攻撃種類
    enum eAttackType
    { 
        eFire = 0,
        eFlame,
        
        eAttackMax
    }

    // ボスの攻撃
    private BossAttack bossAttack;
    // プレイヤー
    private GameObject player;
    // ボスの基底クラス
    private BossBase BossBase;
    // 前フレームの座標
    private Vector3 vOldPos;

    // 攻撃関連
    //[Header("攻撃を開始する距離")]
    //[SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 5.0f;

    [Header("攻撃頻度")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 10.0f;

    private float fAttackCount;

    int nAttackType;

    // エフェクト
    [Header("エフェクトシステム")] [SerializeField] EnemyEffect effect;

    // ボスの体
    [Header("体")] [SerializeField] GameObject body;
    [Header("尾")] [SerializeField] GameObject tail;

    public EnemyEffect GetEffect { get { return effect; } }

    // ボスの前面に当たり判定用意
    GameObject FrontCube;


    //
    public float speed = 10.0f;
    public float rotSpeed = 1;
   
    //


    void Start()
    {
        player = GameObject.Find("Player");
        BossBase = GetComponent<BossBase>();

        // 攻撃関連
        bossAttack = GetComponent<BossAttack>();
        bossAttack.SetPlayer(player);
        fAttackCount = fAttackTime;

        // ボスの体、尾を生成
        body = Instantiate(body, transform.position, transform.rotation);
        tail = Instantiate(tail, transform.position, transform.rotation);

        // サイズを全て同じにする
        body.transform.localScale = tail.transform.localScale = transform.localScale;

        // 体にスクリプトを追加する
        body.AddComponent<Boss01_body>();
        tail.AddComponent<Boss01_body>();

        // 情報をセット
        body.GetComponent<Boss01_body>().SetBossFront(gameObject);
        body.GetComponent<Boss01_body>().SetBossHead(gameObject);
        tail.GetComponent<Boss01_body>().SetBossFront(body);
        tail.GetComponent<Boss01_body>().SetBossHead(gameObject);

        

    }

    void Update()
    {
        // 攻撃
        Attack();
        Aim_at_Player();

        // ボスが死んだら子オブジェクトも破壊
        if (BossBase.Death() <= 0)
            DestroyObject();
    }

    // 子オブジェクトの破壊
    public void DestroyObject()
    {
        Destroy(tail);
        Destroy(body);
    }

    // // Playerを狙い、動き続ける *****************************
    void Aim_at_Player() 
    {
        //追従するように走る
        Quaternion lookatWP = Quaternion.LookRotation(player.transform.position - this.transform.position);

        this.transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, rotSpeed * Time.deltaTime);

        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
    //**********************************************************

    // 攻撃
    void Attack()
    {
        //一定時間毎に攻撃
        fAttackCount -= Time.deltaTime;
        if (fAttackCount < 0.0f)
        {
            rotSpeed = 5;
            
        }
        else
        {
            rotSpeed = 1;
        }

        // 一定時間毎に攻撃
        //fAttackCount -= Time.deltaTime;
        //if(fAttackCount < 0.0f)
        //{
        //     //攻撃種類をランダムで
        //    nAttackType = Random.Range(0, (int)eAttackType.eAttackMax) % (int)eAttackType.eAttackMax;

        //     //攻撃の分岐
        //    switch (nAttackType)
        //    {
        //         //火柱生成
        //        case (int)eAttackType.eFire:
        //            bossAttack.CreateFire(player.transform.position);
        //            break;

        //         //火炎放射生成
        //        case (int)eAttackType.eFlame:
        //            bossAttack.CreateFlame(player.transform.position);
        //            break;

        //        default:
        //            break;
        //    }

        //     //タイマー初期化
        //    fAttackCount = fAttackTime;
        //}


    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーとの衝突時ダメージ
        if (other.CompareTag("Player"))
        {
            fAttackCount = fAttackTime;
        }
    }

}
