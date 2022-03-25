//======================================================================
// GuardMode.cs
//======================================================================
// 開発履歴
//
// 2022/03/01 author：田村敏基 ガード状態を管理するスクリプト
//                             止まる機能実装
// 2022/03/03 author：田村敏基 ガードゲージなどのガードに必要な機能実装
// 2022/03/11 author：田村敏基 UI機能実装(時間がなかっため、作り直したい)
//                             爆発実装(敵に効果なし...)
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 判定コンポーネントアタッチ
[RequireComponent(typeof(Stop))]
[RequireComponent(typeof(GuardBurst))]

public class GuardMode : MonoBehaviour
{
    // 停止
    private Stop stop;

    // バースト
    private UIGauge UIgauge;
    private GuardBurst burst;

    // リジッドボディ
    private Rigidbody rb;

    // ステート
    private PlayerState state;

    // ガードゲージ
    private PlayerStatus status;
    [SerializeField] private int nRecovery = 2;
    [SerializeField] private int nCost = 1;

    // Start is called before the first frame update
    void Start()
    {
        stop = GetComponent<Stop>();
        burst = GetComponent<GuardBurst>();
        UIgauge = GetComponent<UIGauge>();
        state = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        // ハードモードじゃないならゲージ回復
        if (!state.IsHard)
        {
            RecoveryGauge();
        }
        // ハードモードならゲージ消費
        if(state.IsHard)
        {
            SubtractGauge();
        }
        // バーストモードなら
        if(state.IsBurst)
        {
            // 爆発
            burst.Explode();
            state.GotoNormalState();
        }

        // ハードモードかつゲージ残量があるなら停止
        if(state.IsHard && status.Stamina > 0)
        {
            stop.DoStop(rb);
        }
        else
        {
            state.GotoNormalState();
        }
    }

    // ゲージ回復
    private void RecoveryGauge()
    {
        // ゲージ量回復
        status.Stamina += nRecovery;
        if(status.Stamina >= status.MaxStamina)
        {
            status.Stamina = status.MaxStamina;
        }
    }

    // ゲージ消費
    private void SubtractGauge()
    {
        status.Stamina -= nCost;
        if (status.Stamina < 0)
        {
            status.Stamina = 0;
        }
    }
}
