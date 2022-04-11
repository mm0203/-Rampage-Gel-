//======================================================================
// BossBase.cs
//======================================================================
// 開発履歴
//
// 2022/03/27 author：小椋駿 製作開始　ボスベース処理
// 2022/03/28 author：竹尾　応急　ポータル出現、リスト消去機能コメントアウト
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
    private NavMeshAgent myAgent;
    private Animator animator;

    //*応急*
    [SerializeField] GameObject Portals;
    bool bPortal = false;

    // 前フレームの座標
    private Vector3 vOldPos;

    // ダメージUI
    [SerializeField] private GameObject DamageObj;

    // 効果音
    [Header("死亡時効果音")] [SerializeField] private AudioClip DeathSE;

    // 攻撃関連
    [Header("攻撃を開始する距離")] [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 5.0f;
    [Header("攻撃頻度")] [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;
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

    // 突進中か
    bool bRush = false;
    float fRushTime = 1.0f;
    float fRushCount = 0.0f;

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


        BossRush rush = gameObject.GetComponentInChildren<BossRush>();
        rush.SetPlayer(player);
        rush.SetEnemy(gameObject);

    }

    //-------------------------
    // 更新
    //-------------------------
    void Update()
    {
        Move();
        //Attack();
        Death();

    }

    //----------------------------
    // 死亡
    //----------------------------
    private void Death()
    {
        // HP0以下で消滅
        if (status.HP <= 0)
        {
            

            //*応急*
            if(bPortal == false)
            {
                // 効果音再生
                AudioSource.PlayClipAtPoint(DeathSE, transform.position);

                Instantiate(Portals, this.gameObject.transform.position, Quaternion.identity);
                bPortal = true;
            }

            // 全て消滅
            Destroy(tail);
            Destroy(body);
            Destroy(this.gameObject);

        }
    }

    //----------------------------
    // 移動
    //----------------------------
    void Move()
    {
        // 次の場所を計算
        Vector3 nextPoint = myAgent.steeringTarget;
        Vector3 targetDir = nextPoint - transform.position;

        // 回転
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);


        // プレイヤーを追いかける（一定秒数ごとに敵めがけて移動）
        if (!bRush)
        {
            // 現在のプレイヤーの位置を目指す
            myAgent.SetDestination(player.transform.position);

            // 初期化
            bRush = true;
            fRushCount = fRushTime;
        }
        
        // 移動のカウント処理
        fRushCount -= Time.deltaTime;
        if(fRushCount < 0.0f)
        {
            bRush = false;
        }
        

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
            Damege();
        }
    }

    //------------------------------------------------------
    // ダメージの処理(ボスの体、尾でも使えるようにpublic)
    //------------------------------------------------------
    public void Damege()
    {
        // ダメージ処理
        status.HP -= player.GetComponent<PlayerStatus>().Attack;

        // ダメージ表記
        ViewDamage(player.GetComponent<PlayerStatus>().Attack);
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
