//======================================================================
// BossManager.cs
//======================================================================
// 開発履歴
//
// 2022/04/15 author：松野将之 ボス全体の管理実装
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    // プレイヤー情報
    public GameObject player;

    private Boss2 boss;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //boss.Move(player);
    }
}
