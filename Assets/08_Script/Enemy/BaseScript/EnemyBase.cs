//======================================================================
// EnemyBase.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵のベース処理追加
// 2022/03/11 author：小椋駿 バースト処理追加
// 2022/03/15 author：小椋駿 ステータス部分変更
// 2022/03/28 auther：竹尾　応急　経験値機能追加
// 2022/03/24 author：小椋駿 効果音処理の追加
// 2022/03/31 author：小椋駿 一定距離離れると敵が消滅するように
// 2022/04/04 author：小椋駿 中ボス用に少し改良
// 2022/04/15 author：松野将之 マスターデータからステータスを取得
// 2022/04/21 author：小椋駿 GetEnemyDataを作成(EnamyManager.csにて使用)
// 2022/04/28 author：竹尾　攻撃してこない不具合をeAnimetionに要素追加で対応
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
        eDefult,
        eMove,
        eAttack,
    }

    // 敵のマスターデータ
    [SerializeField] private EnemyData enemyData;
    public EnemyData GetEnemyData { get { return enemyData; } }

    public GameObject player{ get; set; }
    public EnemyManager manager { get; set; }
    private NavMeshAgent myAgent;
    private Animator animator;
    private Rigidbody rb;

    // HPと攻撃力
    public float nHp { get; set; }
    private float nAttack;

    // 前フレームの座標
    private Vector3 vOldPos;

    // 吹っ飛ばされてから動き出す秒数
    private float fBurstTime = 0.2f;
    private float fSetOldBurstTime;

    // 攻撃中か
    public bool bAttack { get; set; }

    // 攻撃範囲に入ってから、一度目の攻撃か
    private bool bFirstAttack = false;

    [Header("死亡時効果音")]
    [SerializeField] private AudioClip DeathSE;

    [Header("攻撃を開始する距離")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackDis = 3.0f;

    [Header("攻撃頻度")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;
    private float fAttackCount;

    // 消滅距離
    float fDistance = 20.0f;

    //----------------------------
    // 初期化
    //----------------------------
    void Start()
    {
        // ステータス初期化
        nHp = enemyData.nHp + (enemyData.nLevel * enemyData.nUpHP);
        nAttack = enemyData.nAttack + (enemyData.nLevel * enemyData.nUpAttack);

        // ナビメッシュ設定
        myAgent = GetComponent<NavMeshAgent>();
   
        // スピード設定
        myAgent.speed = enemyData.fSpeed;
    
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        fAttackCount = fAttackTime;
    }

    //----------------------------
    // 更新
    //----------------------------
    void Update()
    {
        Burst();
        Move();
        Death();
        DistanceDeth();   
    }

    //----------------------------
    // 死亡
    //----------------------------
    private void Death()
    {
        // HP0以下で消滅
        if (nHp <= 0)
        {
            // 経験値処理
            player.GetComponent<PlayerExp>().AddExp(10);

            // 効果音再生
            AudioSource.PlayClipAtPoint(DeathSE, transform.position);

            Destroy(this.gameObject);
        }
    }

    //----------------------------
    //  一定距離離れたら消滅
    //----------------------------
    private void DistanceDeth()
    {
        // プレイヤーとの差を計算
        Vector2 vdistance = new Vector2(transform.position.x - player.transform.position.x, transform.position.z - player.transform.position.z);

        // 消滅処理
        if (vdistance.x > fDistance || vdistance.x < -fDistance ||vdistance.y > fDistance || vdistance.y < -fDistance)
        {
            // リストから削除
            manager.NowEnemyList.Remove(gameObject);
            Destroy(this.gameObject);
        }
    }


    //----------------------------
    // 攻撃
    //----------------------------
    private void EnemyAttack()
    {
        // 動きを止める
        myAgent.speed = 0.0f;
        myAgent.velocity = Vector3.zero;

        // 攻撃開始か判定 or 攻撃範囲に入ってから、最初の攻撃の時
        if (IsAttack() || !bFirstAttack)
        {
            // 攻撃モーション
            animator.SetInteger("Parameter", (int)eAnimetion.eAttack);

            bFirstAttack = true;
        }
    }

    //----------------------------
    // 攻撃開始か
    //----------------------------
    private bool IsAttack()
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

    //----------------------------
    // 移動
    //----------------------------
    private void Move()
    {
        // 動いているか
        if ((vOldPos.x == transform.position.x || vOldPos.z == transform.position.z))
        {
            //Debug.Log("notmove");
        }
        else
        {
            // 移動モーション
            animator.SetInteger("Parameter", (int)eAnimetion.eMove);
            bFirstAttack = false;
        }

        // 攻撃中でないとき
        if (!bAttack)
        {
           // 次の場所を計算
           Vector3 nextPoint = myAgent.steeringTarget;
            Vector3 targetDir = nextPoint - transform.position;

           // 回転
           Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

            // プレイヤーを追いかける
            myAgent.SetDestination(player.transform.position);
        }

        // プレイヤーとの距離計算
        Vector3 vDiffPos = this.transform.position - player.transform.position;

        // 敵との距離が一定以下なら攻撃処理
        if ((vDiffPos.x <= fAttackDis && vDiffPos.x >= -fAttackDis) && (vDiffPos.z <= fAttackDis && vDiffPos.z >= -fAttackDis))
        {
            EnemyAttack();
        }
        // 攻撃終了時動き出す
        else if (myAgent.speed == 0.0f && !bAttack)
        {
            // スピードの再設定
            myAgent.speed = enemyData.fSpeed;
        }

        vOldPos = this.gameObject.transform.position;
    }

    //----------------------------
    // バーストをくらったとき
    //----------------------------
    private void Burst()
    {
        // 物理演算がONの時（バースト時に物理演算がONになる）
        if (!rb.isKinematic)
        {
            
            // 2秒後に、物理演算OFFにする(仮)
            fBurstTime -= Time.deltaTime;
            if (fBurstTime < 0.0)
            {
                
                fBurstTime = fSetOldBurstTime;
                
                rb.isKinematic = true;
                
                
            }
        }
        else
        {
            fSetOldBurstTime = fBurstTime;
        }
    }
}
