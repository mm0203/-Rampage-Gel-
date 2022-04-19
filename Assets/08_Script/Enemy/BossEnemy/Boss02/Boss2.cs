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
    // プレイヤー情報
    [SerializeField] private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        //Move(myAgent, Player);
        //CreateDivision();
    }

    // ボスの分裂発生
    void CreateDivision()
    {

    }

}
