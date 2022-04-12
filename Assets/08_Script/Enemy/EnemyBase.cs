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
    private Animator animator;
    private Rigidbody rb;

    // 前フレームの座標
    private Vector3 vOldPos;

   // 吹っ飛ばされてから動き出す秒数
    private float fBurstTime = 2.0f;

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

    // エフェクト
    [Header("エフェクトシステム")] [SerializeField] EnemyEffect effect;

    // 消滅距離
    float fDistance = 20.0f;

    //------------------------
    // ゲッター、セッター
    //------------------------
    public void SetManager(EnemyManager obj) { manager = obj; }
    public void SetPlayer(GameObject obj) { player = obj; }
    public GameObject GetPlayer { get { return player; } }

    public void SetAttack(bool flag) { bAttack = flag; }
    public EnemyEffect GetEffect { get { return effect; } }

    //----------------------------
    // 初期化
    //----------------------------
    void Start()
    {
        // ステータス初期化
        status = GetComponent<StatusComponent>();
        status.HP = status.HP + (status.Level * status.UpHP);
        status.Attack = status.Attack + (status.Level * status.UpAttack);
        status.Speed = status.Speed;

        // ナビメッシュ設定
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = status.Speed;

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
        if (status.HP <= 0)
        {
            // 経験値処理
            player.GetComponent<PlayerExp>().AddExp(10);

            // 効果音再生
            AudioSource.PlayClipAtPoint(DeathSE,transform.position);

            // リストから削除(中ボスはEnemyManagerのリストに入ってないため処理しない)
            if(manager != null) manager.NowEnemyList.Remove(gameObject);
            Destroy(this.gameObject);
        }
    }

    //----------------------------
    //  一定距離離れたら消滅
    //----------------------------
    private void DistanceDeth()
    {
        // 中ボスは離れても消滅しないため処理しない
        if (manager == null) return;

        // プレイヤーとの差を計算
        Vector2 vdistance = new Vector2(transform.position.x - player.transform.position.x, transform.position.z - player.transform.position.z);

        // 消滅処理
        if(vdistance.x > fDistance || vdistance.x < -fDistance ||
           vdistance.y > fDistance || vdistance.y < -fDistance)
        {
            Debug.Log("消滅");

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
            // 待機モーション
            animator.SetInteger("Parameter", (int)eAnimetion.eWait);
        }
        else
        {
            // 移動モーション
            animator.SetInteger("Parameter", (int)eAnimetion.eMove);
            bFirstAttack = false;
        }

        // 攻撃中でないとき
        if(!bAttack)
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
            myAgent.speed = status.Speed;
        }

        vOldPos = this.gameObject.transform.position;
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
            status.HP -= player.GetComponent<PlayerStatus>().Attack;     // TODO:ここにプレイヤーの攻撃力が入る

            // ダメージ表記
            ViewDamage(player.GetComponent<PlayerStatus>().Attack);      // TODO:ここにプレイヤーの攻撃力が入る
        }
    }

    //----------------------------
    // バーストをくらったとき
    //----------------------------
    private void Burst()
    {
        // 物理演算がONの時（バースト時に物理演算がONになる）
        if(!rb.isKinematic)
        {
            // 2秒後に、物理演算OFFにする(仮)
            fBurstTime -= Time.deltaTime;
            if(fBurstTime < 0.0)
            {
                fBurstTime = 2.0f;
                rb.isKinematic = true;
            }
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
        text.transform.position = new Vector3(transform.position.x,transform.position.y, transform.position.z + 1.0f) ;
    }
}
