//======================================================================
// BossBase.cs
//======================================================================
// 開発履歴
//
// 2022/04/15 author：松野将之 ボスの基底クラス実装
// 2022/05/02 author：小椋駿   ターゲットマーカー生成追加
// 2022/05/05 author：竹尾　プレイヤーの速度に対してダメージ出せるように
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
    public int nHp;

    // 速度に対するダメージ補正
    float fSpeedtoDamage = 0.03f;

    //*応急*
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
        player = GameObject.Find("Player");

        nHp = enemyData.nBossHp + (enemyData.nLevel * enemyData.nUpHP);

        // ナビメッシュ初期化（ステータスからスピードを取得）
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = enemyData.fSpeed;

        // アニメーター初期化
        animator = GetComponent<Animator>();

        // 敵ターゲットマーカー生成
        Marker = Instantiate(Marker, Vector3.zero, Quaternion.identity);

        // ボス情報をセット
        Marker.GetComponentInChildren<TargetMarker>().target = gameObject.transform;

    }

    void Update()
    {
        Move();
        Death();

    }

    // 死亡
    public bool Death()
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

            return true;
        }
        return false;
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
    }

    // プレイヤーとの接触時
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーとの衝突時ダメージ
        if (other.CompareTag("Player"))
        {
            // ダメージ処理
            Damege();
        }
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
}
