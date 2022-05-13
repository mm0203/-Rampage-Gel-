//======================================================================
// BossBase.cs
//======================================================================
// 開発履歴
//
// 2022/04/15 author：松野将之 ボスの基底クラス実装
// 2022/05/02 author：松野将之 死亡関数(Death)からHPを取得可能に
// 2022/05/02 author：小椋駿   ターゲットマーカー生成追加
// 2022/05/05 author：竹尾　プレイヤーの速度に対してダメージ出せるように
// 2022/05/06 　　　　　　　メモ、攻撃関数を追加しアニメーションを起こしたい
// 2022/05/09               アニメーションと攻撃を仮追加
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBase : MonoBehaviour
{
    //0509 追加 **********************************************
    // アニメーションの種類
    enum eAnimetion
    {
        eDefult,
        eMove,
        eAttack,
    }

    // 攻撃中か
    public bool bAttack { get; set; }

    // 攻撃頻度
    private float fAttackCount;

    [Header("攻撃を開始する距離")]
    [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 3.0f;

    [Header("攻撃頻度")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;

    // 攻撃範囲に入ってから、一度目の攻撃か
    private bool bFirstAttack = false;

    private Rigidbody rb;
    //********************************************************

    [SerializeField] public EnemyData enemyData;
    public EnemyData GetEnemyData { get { return enemyData; } }
    public GameObject player { get; set; }
    private NavMeshAgent myAgent;
    private Animator animator;
    

    // HP
    public int nHp;

    // 速度に対するダメージ補正
    float fSpeedtoDamage = 0.03f;

    //*応急
    [SerializeField] GameObject Portals;
    bool bPortal = false;

    // 前フレームの座標
    private Vector3 vOldPos;

    // ダメージUI
    [SerializeField] private GameObject DamageObj;

    [Header("死亡時効果音")]
    [SerializeField] private AudioClip DeathSE;

    [Header("エフェクトシステム")]
    [SerializeField] EnemyEffect effect;

    [Header("敵マーカー")]
    [SerializeField] Canvas Marker;

    public EnemyEffect GetEffect { get { return effect; } }



    void Start()
    {
        player = GameObject.FindWithTag("Player");

        nHp = enemyData.BossHp + (enemyData.nLevel * enemyData.nUpHP);

        // ナビメッシュ初期化（ステータスからスピードを取得）
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = enemyData.fSpeed;

        // アニメーター初期化
        animator = GetComponent<Animator>();

        // 敵ターゲットマーカー生成
        Marker = Instantiate(Marker, Vector3.zero, Quaternion.identity);

        // ボス情報をセット
        Marker.GetComponentInChildren<TargetMarker>().target = gameObject.transform;

        //0509 追加 **********************************************
        rb = this.gameObject.GetComponent<Rigidbody>();
        //********************************************************

    }



    void Update()
    {
        Move();
        Death();

        // 0509 追加 **********************************************
        Burst();
        //********************************************************

    }



    // 死亡
    public int Death()
    {
        // HP0以下で消滅
        if (nHp <= 0)
        {
            //*応急*
            if (bPortal == false)
            {
                // 効果音再生
                AudioSource.PlayClipAtPoint(DeathSE, transform.position);

                Instantiate(Portals, this.gameObject.transform.position, Quaternion.identity);
                bPortal = true;
            }

            // ターゲットマーカー消滅
            Destroy(Marker);

            // 全て消滅
            Destroy(this.gameObject);

            return 0;
        }
        return nHp;
    }



    // 移動
    public void Move()
    {
        // 次の場所を計算
        Vector3 nextPoint = myAgent.steeringTarget;
        Vector3 targetDir = nextPoint - transform.position;

        // 回転
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

        // 現在のプレイヤーの位置を目指す
        myAgent.SetDestination(player.transform.position);

        // 過去座標を更新
        vOldPos = gameObject.transform.position;

        // プレイヤーとの距離計算
        Vector3 vDiffPos = this.transform.position - player.transform.position;


        // 0509 追加 **********************************************
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
        //********************************************************
    }

    //プレイヤーとの接触時(IsTrigger)
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーとの衝突時ダメージ
        if (other.CompareTag("Player"))
        {
            // ダメージ処理
            Damege();
        }
    }


    // 0509 追加 **********************************************
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

    // ダメージの処理
    public void Damege()
    {
        // ダメージ処理
        int n = (int)((player.GetComponent<PlayerStatus>().Attack * (int)player.GetComponent<Rigidbody>().velocity.magnitude) * fSpeedtoDamage);
        nHp -= n;

        // ダメージ表記
        ViewDamage(n);
    }

    // ダメージ表記
    private void ViewDamage(int damage)
    {
        // テキストの生成
        GameObject text = Instantiate(DamageObj);
        text.GetComponent<TextMesh>().text = damage.ToString();

        // 少しずらした位置に生成(z + 1.0f)
        text.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.0f);
    }
    //********************************************************

    //----------------------------
    // バーストをくらったとき
    //----------------------------
    private void Burst()
    {
        // 物理演算がONの時（バースト時に物理演算がONになる）
        if (!rb.isKinematic)
        {

            rb.isKinematic = true;




        }
        
    }
}
