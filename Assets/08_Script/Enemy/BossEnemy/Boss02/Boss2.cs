// Boss2.cs
//======================================================================
// 開発履歴
//
// 2022/04/15 author：松野将之 ボス2(バイバイン)実装開始
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2 : MonoBehaviour
{
    // 敵のデータ
    public EnemyData EnemyData;

    // AI
    private NavMeshAgent myAgent;

    // プレイヤー情報
    [SerializeField] private GameObject Player;

    // 前フレームの座標
    private Vector3 vOldPos;

    //public void SetPlayer(GameObject obj) { Player = obj; }
    // 突進中か
    bool bRush = false;
    float fRushTime = 1.0f;
    float fRushCount = 0.0f;

    bool bVisible = false;

    void Start()
    {
        // ナビメッシュ初期化（ステータスからスピードを取得）
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = EnemyData.fSpeed;
    }

    void Update()
    {
        //Move(myAgent, Player);
        //CreateDivision();
    }

    //protected override void Move(NavMeshAgent nav, GameObject obj)
    //{

    //    // プレイヤーを追いかける（一定秒数ごとに敵めがけて移動）
    //    if (!bRush)
    //    {
    //        // 現在のプレイヤーの位置を目指す
    //        myAgent.SetDestination(Player.transform.position);

    //        // 初期化
    //        bRush = true;
    //        fRushCount = fRushTime;
    //    }

    //    // 移動のカウント処理
    //    fRushCount -= Time.deltaTime;
    //    if (fRushCount < 0.0f)
    //    {
    //        bRush = false;
    //    }


    //    vOldPos = gameObject.transform.position;
    //}


    //public void Move()
    //{
    //    // 次の場所を計算
    //    Vector3 nextPoint = myAgent.steeringTarget;
    //    Vector3 targetDir = nextPoint - transform.position;

    //    // 回転
    //    Quaternion targetRotation = Quaternion.LookRotation(targetDir);
    //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);
    //    myAgent.SetDestination(Player.transform.position);

    //   // vOldPos = gameObject.transform.position;
    //}

    // ボスの分裂発生
    void CreateDivision()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーとの衝突時ダメージ
        if (other.CompareTag("Player"))
        {
            // ダメージ処理
            //Damege();
            CreateDivision();
            Debug.Log("バイバイン");
        }
    }

}
