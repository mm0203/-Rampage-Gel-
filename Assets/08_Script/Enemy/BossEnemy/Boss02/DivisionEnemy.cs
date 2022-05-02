//======================================================================
// DivisionEnemy.cs
//======================================================================
// 開発履歴
//
// 2022/04/26 author：松野将之 分裂後の実装　追従(ガバガバなので要修正)
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DivisionEnemy : MonoBehaviour
{
    private GameObject player;

    private NavMeshAgent myAgent;

    private bool bStop = false;

    private float fTime = 4.0f;

    void Start()
    {
        player = GameObject.Find("Player");
        // NavMeshAgentを保持しておく
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Move();
        fTime -= Time.deltaTime;

        if (fTime <= 2.0f)
        {
            //myAgent.velocity = Vector3.zero;
            myAgent.Stop();
            //Destroy(this.gameObject);
        }
        if(fTime <= 0.0f)
        {
            Destroy(this.gameObject);
            //fTime = 0.0f;
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
}
