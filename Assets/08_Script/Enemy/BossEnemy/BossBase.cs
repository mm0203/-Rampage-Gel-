//======================================================================
// BossBase.cs
//======================================================================
// 開発履歴
//
// 2022/04/15 author：松野将之 ボスの基底クラス実装
// 2022/05/02 author：松野将之 死亡関数(Death)からHPを取得可能に
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBase : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    public GameObject player { get; set; }
    private NavMeshAgent myAgent;
    private Animator animator;

    // HP
    private int nHp;

    //*応急*
    [SerializeField] GameObject Portals;
    bool bPortal = false;

    // 前フレームの座標
    private Vector3 vOldPos;

    // ダメージUI
    [SerializeField] private GameObject DamageObj;

    // 効果音
    [Header("死亡時効果音")]
    [SerializeField] private AudioClip DeathSE;

    // エフェクト
    [Header("エフェクトシステム")]
    [SerializeField] EnemyEffect effect;

    public EnemyEffect GetEffect { get { return effect; } }

    // ボスの前面に当たり判定用意
    GameObject FrontCube;

    // 突進中か
    bool bRush = false;
    float fRushTime = 1.0f;
    float fRushCount = 0.0f;

    bool bVisible = false;

    void Start()
    {
        player = GameObject.Find("Player");

        nHp = enemyData.BossHp + (enemyData.nLevel * enemyData.nUpHP);

        // ナビメッシュ初期化（ステータスからスピードを取得）
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = enemyData.fSpeed;

        // その他初期化
        animator = GetComponent<Animator>();

        // ダメージ処理
        BossRush rush = gameObject.GetComponentInChildren<BossRush>();
        rush.SetPlayer(player);
        rush.SetEnemy(gameObject);

    }

    void Update()
    {
        Move();
        Death();

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
        if (fRushCount < 0.0f)
        {
            bRush = false;
        }

        // 過去座標を更新
        vOldPos = gameObject.transform.position;
    }

    // プレイヤーとの接触時
    //private void OnTriggerEnter(Collider other)
    //{
    //    // プレイヤーとの衝突時ダメージ
    //    if (other.CompareTag("Player"))
    //    {
    //        // ダメージ処理
    //        Damege();
    //    }
    //}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // ダメージ処理
            Damege();
        }
    }

    // ダメージの処理
    public void Damege()
    {
        // ダメージ処理
        nHp -= player.GetComponent<PlayerStatus>().Attack;

        // ダメージ表記
        ViewDamage(player.GetComponent<PlayerStatus>().Attack);
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
}
