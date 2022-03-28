//======================================================================
// BossBase.cs
//======================================================================
// 開発履歴
//
// 2022/03/27 author：小椋駿 製作開始　ボスベース処理
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]

public class Boss01 : MonoBehaviour
{
    enum eAttackType
    { 
        eFire = 0,
        eFlame,
        
        eAttackMax
    }

    private BossAttack bossAttack;
    private StatusComponent status;
    private GameObject player;
    private EnemyManager manager;
    private NavMeshAgent myAgent;
    private Animator animator;
    private Rigidbody rb;


    // 前フレームの座標
    private Vector3 vOldPos;

    // 攻撃中か
    private bool bAttack = false;

    // 攻撃範囲に入ってから、一度目の攻撃か
    private bool bFirstAttack = false;

    // ダメージUI
    [SerializeField] private GameObject DamageObj;

    // 効果音
    [Header("死亡時効果音")] [SerializeField] private AudioClip DeathSE;

    // 攻撃関連
    [Header("攻撃を開始する距離")] [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 3.0f;
    [Header("攻撃頻度")] [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;
    private float fAttackCount;
    int nAttackType;

    // ランダムで動く距離
    [SerializeField] private float fRangeDiff = 10.0f;

    // 目的地変更する時間
    [SerializeField] private float fRangeTime = 2.0f;
    private float fRangeCount;

    // 次の目的地用
    Vector3 vRandomPos;


    public void SetAttack(bool flag) { bAttack = flag; }


    //-------------------------
    // 初期化
    //-------------------------
    void Start()
    {
        player = GameObject.Find("Player");

        // ステータス初期化
        status = GetComponent<StatusComponent>();
        status.Level = 0;
        status.HP = status.HP + (status.Level * status.UpHP);
        status.Attack = status.Attack + (status.Level * status.UpAttack);
        status.Speed = status.Speed;

        // ナビメッシュ初期化（ステータスからスピードを取得）
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = status.Speed;

        // その他初期化
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        bossAttack = GetComponent<BossAttack>();
        bossAttack.SetPlayer(player);

        fAttackCount = fAttackTime;
        fRangeCount = fRangeTime;
    }

    //-------------------------
    // 更新
    //-------------------------
    void Update()
    {
        Death();
        Move();
        Attack();
    }

    //----------------------------
    // 死亡
    //----------------------------
    private void Death()
    {
        // HP0以下で消滅
        if (status.HP <= 0)
        {
            // 効果音再生
            AudioSource.PlayClipAtPoint(DeathSE, transform.position);

            // リストから削除
            manager.NowEnemyList.Remove(gameObject);
            Destroy(this.gameObject);
        }
    }

    //----------------------------
    // 移動
    //----------------------------
    void Move()
    {
        fRangeCount -= Time.deltaTime;
        if(fRangeCount < 0.0f)
        {
            // 次の目的地を計算
            vRandomPos = new Vector3(Random.Range(-fRangeDiff, fRangeDiff), 0, Random.Range(-fRangeDiff, fRangeDiff));
            fRangeCount = fRangeTime;
        }


        if(!bAttack)
        {
            // 目的地へ移動
            myAgent.SetDestination(vRandomPos);
        }
        else if(bAttack)
        {
            myAgent.speed = 0;
            myAgent.velocity = Vector3.zero;
        }


        //// 動いているか
        //if ((vOldPos.x == transform.position.x || vOldPos.z == transform.position.z))
        //{
        //    // 待機モーション
        //    //animator.SetInteger("Parameter", (int)eAnimetion.eWait);
        //}
        //else
        //{
        //    // 移動モーション
        //    //animator.SetInteger("Parameter", (int)eAnimetion.eMove);
        //    bFirstAttack = false;
        //}

        vOldPos = gameObject.transform.position;
    }

    //----------------------------
    // 攻撃
    //----------------------------
    void Attack()
    {
        // 一定時間毎に攻撃
        fAttackCount -= Time.deltaTime;
        if(fAttackCount < 0.0f)
        {
            // 攻撃種類をランダムで
            nAttackType = Random.Range(0, (int)eAttackType.eAttackMax) % (int)eAttackType.eAttackMax;

            // 攻撃の分岐
            switch (nAttackType)
            {
                // 火柱生成
                case (int)eAttackType.eFire:
                    bossAttack.CreateFire(player.transform.position);
                    break;

                // 火炎放射生成
                case (int)eAttackType.eFlame:
                    bossAttack.CreateFlame(player.transform.position);
                    break;

                default:
                    break;
            }

            // タイマー初期化
            fAttackCount = fAttackTime;
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
            status.HP -= 10;     // TODO:ここにプレイヤーの攻撃力が入る

            // ダメージ表記
            ViewDamage(10);      // TODO:ここにプレイヤーの攻撃力が入る
        }
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
}
