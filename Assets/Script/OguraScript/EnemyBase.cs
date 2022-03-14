//======================================================================
// EnemyBase.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 製作開始 敵のベース処理（攻撃、移動、死亡）
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]

public class EnemyBase : MonoBehaviour
{

    // アニメーションの種類
    enum eAnimetion
    {
        eWait = 0,
        eMove,
        eAttack,
    }

    private StatusComponent status;
    private GameObject player;
    private EnemyManager manager;
    private NavMeshAgent myAgent;
    private SphereCollider SpherCol;
    private Animator animator;


    private bool bFind = false;
    private bool bAttack = false;
    private float nMoveTime = 2.0f; // 仮
    private Vector3 vOldPos;


    // 基本ステータス
    [SerializeField] private int HP = 20;
    [SerializeField] private int Attack = 10;
    [SerializeField] private int Speed = 1;

    [Header("レベルアップ時の上がり幅")] [SerializeField, Range(1.0f, 10.0f)] private int nUpHP = 1;
    [SerializeField, Range(1.0f, 10.0f)] private int nUpAttack = 1;

    [Header("ターゲットを見つける距離")] [SerializeField, Range(1.0f, 50.0f)] private float fRadius = 5.0f;
    [Header("ターゲットを見失う距離")] [SerializeField, Range(1.0f, 50.0f)] private float fMissDis = 8.0f;
    [Header("ランダムに動く距離")] [SerializeField, Range(1.0f, 100.0f)] private float fRandMove = 10.0f;
    [Header("攻撃を開始する距離")] [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 3.0f;
    [Header("攻撃頻度")] [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;
    private float fAttackCount;

    private Rigidbody rb;
    private float rbTime = 2.0f;

    public void SetManager(EnemyManager obj) { manager = obj; }
    public void SetPlayer(GameObject obj) { player = obj; }

    public GameObject GetPlayer { get { return player; } }

    void Start()
    {
        // ステータス初期化
        status = GetComponent<StatusComponent>();
        status.Level = 1;   // TODO:後々Managerで設定する
        status.HP = HP + (status.Level * nUpHP);
        status.Attack = Attack + (status.Level * nUpAttack);
        status.Speed = Speed;

        // ナビメッシュ設定
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = status.Speed;        

        // SpherCollider追加（プレイヤー探索用）
        SpherCol = gameObject.AddComponent<SphereCollider>();
        SpherCol.isTrigger = true;
        SpherCol.radius = fRadius;

        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();

        bFind = false;
        fAttackCount = fAttackTime;
    }


    void Update()
    {
        Move();
        Death();
        Burst();
    }

    // 死亡処理
    private void Death()
    {
        if (status.HP <= 0)
        {
            manager.NowEnemyList.Remove(gameObject);
            Destroy(this.gameObject);
        }
    }




    //-------------------
    // 攻撃開始
    //-------------------
    private void EnemyAttack()
    {
        myAgent.speed = 0.0f;   

        // 攻撃開始(仮)
        bAttack = StartAttack();
        if (bAttack)
        {
            // 攻撃モーション
            animator.SetInteger("Parameter", (int)eAnimetion.eAttack);  
        }
    }

    //-------------------
    // 移動
    //-------------------
    private void Move()
    {
        // 動いているか
        if (vOldPos.x == transform.position.x || vOldPos.z == transform.position.z)
        {
            // 待機モーション
            animator.SetInteger("Parameter", (int)eAnimetion.eWait);
        }
        else
        {
            // 移動モーション
            animator.SetInteger("Parameter", (int)eAnimetion.eMove);
        }

        // 次の場所を計算
        Vector3 nextPoint = myAgent.steeringTarget;
        Vector3 targetDir = nextPoint - transform.position;

        // 回転
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

        // 一定の距離離れた場合、見失う
        Vector3 vDiffPos = this.transform.position - player.transform.position;
        if (vDiffPos.x > fMissDis || vDiffPos.z > fMissDis)
            bFind = false;

        // 範囲内にプレイヤーがいたら追いかける
        if (bFind)
        {
            myAgent.SetDestination(player.transform.position);

            // 敵との距離が一定以下なら攻撃処理
            if ((vDiffPos.x <= fAttackDis && vDiffPos.x >= -fAttackDis) && (vDiffPos.z <= fAttackDis && vDiffPos.z >= -fAttackDis))
            {
                // 攻撃開始
                EnemyAttack();
            }
            else if (myAgent.speed == 0.0f)
            {
                // スピードの再設定
                myAgent.speed = status.Speed;
            }
        }
        //　設定フレーム毎に、目的地変更
        else if (!bFind)
        {
            nMoveTime -= Time.deltaTime;
            if (nMoveTime < 0)
            {
                // ランダム移動
                myAgent.SetDestination(new Vector3(Random.Range(-fRandMove, fRandMove), 0, Random.Range(-fRandMove, fRandMove)));
                nMoveTime = 2.0f;　// TODO:敵の進路選択カウント（仮）
            }
        }

        vOldPos = this.gameObject.transform.position;
    }

    //-------------------
    // 攻撃
    //-------------------
    private bool StartAttack()
    {
        fAttackCount -= Time.deltaTime;

        // 攻撃開始
        if (fAttackCount < 0.0f)
        {
            fAttackCount = fAttackTime;
            return true;
        }
        return false;
    }

    //------------------------------
    // 範囲にプレイヤーが入った時
    //-----------------------------
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーが範囲に入ったら追う
        if (other.CompareTag("Player"))
        {
            bFind = true;
        }
    }

    //------------------------------
    // プレイヤーに当たった時
    //-----------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // プレイヤーの正面に押す
            Vector3 vPush = player.transform.forward;
            transform.position += vPush;

            // ダメージ処理
            status.HP -= 10;     // TODO:ここにプレイヤーの攻撃力が入る,下記の通りになるはず（多分）
            //status.HP -= player.GetComponent<StatusComponent>().Attack;
        }
    }

    //------------------------------
    // バーストされたとき
    //-----------------------------
    private void Burst()
    {
        // 物理演算ONの時（バースト時、プレイヤーのスクリプトにて物理演算がONにされる）
        if (!rb.isKinematic)
        {
            // 2秒後、物理演算をOFFにする
            rbTime -= Time.deltaTime;
            if (rbTime < 0.0f)
            {
                rbTime = 2.0f;
                rb.isKinematic = true;
            }
        }


    }
}
