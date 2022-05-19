//======================================================================
// Boss2.cs
//======================================================================
// 開発履歴
//
// 2022/04/15 author：松野将之 ボス2(バイバイン)実装開始
// 2022/04/26 author：松野将之 アニメーション追加(移動・攻撃)
// 2022/05/02 author：松野将之 雑魚敵をボスの周りから円状に生成されるように
// 2022/05/18 author：松野将之 攻撃範囲・ダメージ処理追加                           
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2 : MonoBehaviour
{
    // ボスの情報
    [Header("ボスの情報")]
    public EnemyData enemyData;
    // 分裂する雑魚敵
    [Header("雑魚的オブジェクト")]
    public GameObject DivisionEnemy;
    // 攻撃範囲の表示用オブジェクト
    [Header("攻撃範囲表示用オブジェクト")]
    public GameObject AttackField;
    // ボスの攻撃範囲
    [Header("攻撃範囲")]
    public float Radius = 5.0f;

    // プレイヤー情報
    private GameObject player;
    // アニメーション
    private Animator animation;
    // ボスの基底クラス
    private EnemyBase BossBase;
    // ボスの最大HP
    private int nMaxHp;
    // 雑魚的生成の半径
    private float distance = 5.0f;
    // 雑魚敵が生成から消えるまでの時間
    private float fTime = 2.0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;

        animation = GetComponent<Animator>();
        BossBase = GetComponent<EnemyBase>();
        // ボスの最大HP設定
        nMaxHp = enemyData.BossHp;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) // 竹尾：CollisionからTriggerへ仮変更、プレイヤーを何とかしたら戻す
    {
        if (other.gameObject.tag == "Player")
        {
            //bHit = true;

            // ボスのHPを取得
            int nDivisionCnt = OnDamegeHp();

            // 雑魚敵の生成
            CreateDivision(DivisionEnemy,    // 生成するオブジェクト
                           nDivisionCnt * 2, // 生成する数
                           this.gameObject,  // 中心のオブジェクト
                           distance,         // 各オブジェクトの距離
                           true);            // 中央に向けるかどうか

        }
    }

    // HPによる分裂数
    private int OnDamegeHp()
    {
        // ボスの現在のHPを取得
        int CurrentHP = (int)BossBase.nHp;
        Debug.Log(CurrentHP);

        int nDivisionCnt = 0;

        // HPが半分以上なら
        if (CurrentHP > nMaxHp / 2)
        {
            nDivisionCnt = 1;
        }
        // HPが半分以下なら
        else if (CurrentHP <= nMaxHp / 2 && CurrentHP > nMaxHp / 4)
        {
            nDivisionCnt = 2;
        }
        // HPが1/4以下なら
        else if (CurrentHP <= nMaxHp / 4)
        {
            nDivisionCnt = 4;
        }

        return nDivisionCnt;
    }

    // 分裂する敵の生成
    public void CreateDivision(GameObject prefab, int count, GameObject center, float distance, bool isLookAtCenter)
    {
        for (int i = 0; i < count; i++)
        {
            // 円状にオブジェクトを生成
            var position = center.transform.position + (Quaternion.Euler(0f, 360f / count * i, 0f) * center.transform.forward * distance);
            var obj = Instantiate(prefab, position, Quaternion.identity);

            // 中央に向けるかどうか
            if (isLookAtCenter)
            {
                obj.transform.LookAt(center.transform);
            }
        }
    }

    // 攻撃範囲表示
    void CreateAttackField()
    {
        GameObject field = AttackField;

        // 攻撃範囲生成
        field = Instantiate(field, new Vector3(this.transform.position.x, 0.1f, this.transform.position.z), field.transform.rotation);
        // 攻撃範囲の色を赤色に
        field.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);

        // 範囲の削除
        StartCoroutine(AcidAttackField(field));
    }

    // 攻撃範囲削除
    IEnumerator AcidAttackField(GameObject field)
    {
        // 攻撃範囲が表示されて1.8秒経過したら
        yield return new WaitForSeconds(1.8f);
        // 攻撃範囲を削除
        Destroy(field);
    }

    // ダメージ処理
    void Boss2Attack()
    {
        // ボスの位置
        Vector3 enemypos = this.transform.position;
        // プレイヤーの位置
        Vector3 playerpos = player.transform.position;

        // ボスとプレイヤーの位置が交差してるならダメージ処理
        if (InSphere(enemypos, Radius, playerpos))
        {
            player.GetComponent<PlayerHP>().OnDamage(enemyData.BossAttack);
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
