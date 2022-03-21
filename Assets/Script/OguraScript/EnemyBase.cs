//======================================================================
// EnemyBase.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵のベース処理追加
// 2022/03/11 author：小椋駿 バースト処理追加
// 2022/03/15 author：小椋駿 ステータス部分変更
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

    // プレイヤーを見つけているか
    //private bool bFind = false;

    // ランダムに動く時間
    private float nMoveTime = 2.0f; // 仮
    private Vector3 vOldPos;

   // 吹っ飛ばされてから動き出す秒数
    private float fBurstTime = 2.0f;
    private Rigidbody rb;

    [SerializeField] private GameObject DamageObj;
    [Header("ターゲットを見つける距離")] [SerializeField, Range(1.0f, 50.0f)] private float fRadius = 5.0f;
    [Header("ターゲットを見失う距離")] [SerializeField, Range(1.0f, 50.0f)] private float fMissDis = 8.0f;
    [Header("ランダムに動く距離")] [SerializeField, Range(1.0f, 100.0f)] private float fRandMove = 10.0f;
    [Header("攻撃を開始する距離")] [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 3.0f;
    [Header("攻撃頻度")] [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;
    private float fAttackCount;

    public void SetManager(EnemyManager obj) { manager = obj; }
    public void SetPlayer(GameObject obj) { player = obj; }
    public GameObject GetPlayer { get { return player; } }

    //----------------------------
    // 初期化
    //----------------------------
    void Start()
    {
        // ステータス初期化
        status = GetComponent<StatusComponent>();
        status.Level = 0;   // TODO:後々Managerで設定する??
        status.HP = status.HP + (status.Level * status.UpHP);
        status.Attack = status.Attack + (status.Level * status.UpAttack);
        status.Speed = status.Speed;

        // ナビメッシュ設定
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = status.Speed;        

        // SpherCollider追加（プレイヤー探索用）
        //SpherCol = gameObject.AddComponent<SphereCollider>();
        //SpherCol.isTrigger = true;
        //SpherCol.radius = fRadius;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        //bFind = false;
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
    }

    //----------------------------
    // 死亡
    //----------------------------
    private void Death()
    {
        // HP0以下で消滅
        if (status.HP <= 0)
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

        // 攻撃開始か判定(仮)
        if (IsAttack())
        {
            // 攻撃モーション
            animator.SetInteger("Parameter", (int)eAnimetion.eAttack);

            Debug.Log("攻撃");
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
        }


        // 次の場所を計算
        Vector3 nextPoint = myAgent.steeringTarget;
        Vector3 targetDir = nextPoint - transform.position;

        // 回転
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

        
        Vector3 vDiffPos = this.transform.position - player.transform.position;
        // 一定の距離離れた場合、見失う
        //if (vDiffPos.x > fMissDis || vDiffPos.z > fMissDis)
        //    bFind = false;

        // プレイヤーを追いかける
        myAgent.SetDestination(player.transform.position);

        // 敵との距離が一定以下なら攻撃処理
        if ((vDiffPos.x <= fAttackDis && vDiffPos.x >= -fAttackDis) && (vDiffPos.z <= fAttackDis && vDiffPos.z >= -fAttackDis))
        {
            EnemyAttack();
        }
        // 攻撃終了時動き出す
        else if (myAgent.speed == 0.0f)
        {
            // スピードの再設定
            myAgent.speed = status.Speed;
        }

        //if (bFind)
        //{
        //    myAgent.SetDestination(player.transform.position);

        //    // 敵との距離が一定以下なら攻撃処理
        //    if ((vDiffPos.x <= fAttackDis && vDiffPos.x >= -fAttackDis) && (vDiffPos.z <= fAttackDis && vDiffPos.z >= -fAttackDis))
        //    {
        //        EnemyAttack();
        //    }
        //    else if (myAgent.speed == 0.0f)
        //    {
        //        // スピードの再設定
        //        myAgent.speed = status.Speed;    
        //    }
        //}
        ////　設定フレーム毎に、目的地変更
        //else if (!bFind)
        //{
        //    nMoveTime -= Time.deltaTime;
        //    if (nMoveTime < 0)
        //    {
        //        // ランダム移動
        //        myAgent.SetDestination(new Vector3(Random.Range(-fRandMove, fRandMove), 0, Random.Range(-fRandMove, fRandMove)));
        //        nMoveTime = 2.0f;　// 仮
        //    }
        //}

        vOldPos = this.gameObject.transform.position;
    }

    //----------------------------
    // プレイヤー追跡
    //----------------------------
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーが範囲に入ったら追う
        // プレイヤーとの衝突時ダメージ
        if (other.CompareTag("Player"))
        {
            //bFind = true;

            // ダメージ処理
            status.HP -= 10;     // TODO:ここにプレイヤーの攻撃力が入る

            // ダメージ表記
            ViewDamage(10);      // TODO:ここにプレイヤーの攻撃力が入る
        }
    }

    //----------------------------
    // プレイヤーとの衝突時
    //----------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // プレイヤーの正面に押す
            Vector3 vPush = player.transform.forward;
            transform.position += vPush;

            // ダメージ処理
            status.HP -= 10;     // TODO:ここにプレイヤーの攻撃力が入る

            // ダメージ表記
            ViewDamage(10);      // TODO:ここにプレイヤーの攻撃力が入る
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
