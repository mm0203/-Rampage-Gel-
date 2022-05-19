//======================================================================
// DivisionEnemy.cs
//======================================================================
// 開発履歴
//
// 2022/04/26 author：松野将之 分裂後の実装　追従(ガバガバなので要修正)
// 2022/05/19 author：松野将之 雑魚敵のダメージ処理
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DivisionEnemy : MonoBehaviour
{
    // 発生から消滅までの時間
    [Header("発生から消滅までの時間")]
    public float ExtinctTime = 4.0f;
    [Header("追尾する時間")]
    public float stopTime = 2.0f;
    [Header("AoE(攻撃範囲の表示)")]
    public GameObject AttackCircle;
    // 雑魚的の攻撃範囲
    [Header("攻撃範囲")]
    public float Radius = 2.0f;

    // プレイヤー
    private GameObject player;
    // 雑魚敵のAI
    private NavMeshAgent myAgent;
    // Updateで1回だけ関数呼ぶための判定
    private bool isCalledOnce = false;

    void Start()
    {
        player = GameObject.Find("Player");
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Move();
        ExtinctTime -= Time.deltaTime;

        // 2秒経過したら
        if (ExtinctTime <= stopTime)
        {
            // AoE生成
            if (!isCalledOnce)
            {
                // 1度だけAoeを生成
                isCalledOnce = true;
                AttackCircle = Instantiate(AttackCircle, new Vector3(this.transform.position.x, 0.1f, this.transform.position.z), AttackCircle.transform.rotation);
                AttackCircle.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);

                // 追尾後停止
                myAgent.velocity = Vector3.zero;
                myAgent.Stop();
            }
        }

        // 爆発
        if(ExtinctTime <= 0.0f)
        {
            // ダメージ処理
            DivisionEnemyAttack();

            // AoEと雑魚敵を削除
            Destroy(this.AttackCircle);
            Destroy(this.gameObject);
        }
    }

    private void Move()
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
    // ダメージ処理
    void DivisionEnemyAttack()
    {
        // ボスの位置
        Vector3 enemypos = this.transform.position;
        // プレイヤーの位置
        Vector3 playerpos = player.transform.position;

        // ボスとプレイヤーの位置が交差してるならダメージ処理
        if (InSphere(enemypos, Radius, playerpos))
        {
            player.GetComponent<PlayerHP>().OnDamage(10);
        }
    }

    // Aoeが消える時の判定取得用
    public static bool InSphere(Vector3 pos, float rad, Vector3 center)
    {
        var sum = 0f;
        for (var i = 0; i < 3; i++)
        {
            sum += Mathf.Pow(pos[i] - center[i], 2);
        }
        return sum <= Mathf.Pow(rad, 2f);
    }
}
