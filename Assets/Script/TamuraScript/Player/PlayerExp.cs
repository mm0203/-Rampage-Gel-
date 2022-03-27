//======================================================================
// PlayerExp.cs
//======================================================================
// 開発履歴
//
// 2022/03/25 author：田村敏基 作成開始
// 2022/03/27 author：田村敏基 hardモードなら無効化する機能追加
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    PlayerStatus status;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 経験値上昇
    public void AddExp(int exp)
    {
        status.Exp += exp;

        if(status.MaxExp <= status.Exp)
        {
            LevelUp();
        }
    }

    // レベルアップ
    public void LevelUp()
    {
        status.Level++;

        // 現在Expを0にする
        status.Exp = 0;
        // 次のレベルアップまでの経験値量を増やす
        status.MaxExp += status.UpExp;
        status.Attack += status.UpAttack;
    }
}
