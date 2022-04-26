//======================================================================
// Boss2.cs
//======================================================================
// 開発履歴
//
// 2022/04/15 author：松野将之 ボス2(バイバイン)実装開始
// 2022/04/26 author：松野将之 アニメーション追加(移動・攻撃)
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

    // 分裂する雑魚的e
    [SerializeField] private GameObject DivisionEnemy;

    private Animator animation;

    void Start()
    {
        player = GameObject.Find("Player");

        animation = GetComponent<Animator>();
    }

    void Update()
    {
        //Move(myAgent, Player);
        //CreateDivision();

        if(Input.GetKey(KeyCode.O))
        {
            animation.SetTrigger("attack");
        }
    }

    // ボスの分裂発生
    void CreateDivision(int DivisionCnt)
    {
        for (int i = 0; i < DivisionCnt; i++)
        {
            Instantiate(DivisionEnemy, new Vector3(0, 0, 20), this.transform.rotation);
        }
    }

    // プレイヤーとの接触時
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーとの衝突時ダメージ
        if (other.CompareTag("Player"))
        {
            CreateDivision(2);
        }
    }

}
