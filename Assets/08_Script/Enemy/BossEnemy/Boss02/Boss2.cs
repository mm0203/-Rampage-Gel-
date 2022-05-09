//======================================================================
// Boss2.cs
//======================================================================
// 開発履歴
//
// 2022/04/15 author：松野将之 ボス2(バイバイン)実装開始
// 2022/04/26 author：松野将之 アニメーション追加(移動・攻撃)
// 2022/05/02 author：松野将之 雑魚敵をボスの周りから円状に生成されるように
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2 : MonoBehaviour
{
    // プレイヤー情報
    [SerializeField] private GameObject player;

    [SerializeField] private EnemyData enemyData;

    // 分裂する雑魚的e
    public GameObject DivisionEnemy;

    private Animator animation;

    // ボスの基底クラス
    private BossBase BossBase;

    private int nMaxHp;

    private bool bHit = false;

    // 半径
    public float distance = 5.0f;

    private float fTime = 2.0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;

        animation = GetComponent<Animator>();

        BossBase = GetComponent<BossBase>();

        nMaxHp = BossBase.enemyData.BossHp;
    }

    void Update()
    {
        //Move(myAgent, Player);
        //CreateDivision();

        if(Input.GetKey(KeyCode.O))
        {
            animation.SetTrigger("attack");
        }

        if(bHit)
        {
            fTime -= Time.deltaTime;

            if(fTime <= 0.0f)
            {
                fTime = 2.0f;
                bHit = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !bHit)
        {
            bHit = true;

            // ボスのHPを取得
            int nDivisionCnt = OnDamegeHp();

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
        int CurrentHP = BossBase.Death();
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
}
