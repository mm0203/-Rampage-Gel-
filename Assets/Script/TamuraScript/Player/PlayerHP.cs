//======================================================================
// PlayerHP.cs
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

public class PlayerHP : MonoBehaviour
{
    PlayerStatus status;
    PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
        state = GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // ダメージコールバック関数
    public void OnDamage(int damage)
    {
        // 0以下なら死んでるためリターン
        if (status.HP <= 0) return;
        // ハードモードなら無効
        if (state.IsHard)
        {
            // damageをストックする
            this.GetComponent<GuardMode>().AddStockExplode(damage);
            return;
        }

        // ダメージを与える
        status.HP -= damage;
    }
}
