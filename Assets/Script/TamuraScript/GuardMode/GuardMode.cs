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
[RequireComponent(typeof(UIGauge))]
[RequireComponent(typeof(GuardBurst))]

public class GuardMode : PlayerManager
{
    // 停止
    private Stop stop;

    // バースト
    private UIGauge UIgauge;
    private GuardBurst burst;

    // ガードゲージ
    [SerializeField] private float fMaxGuardGauge;
    private float fGuardGauge;
    private bool bGuardGauge;
    [SerializeField] private float fRecovery;
    [SerializeField] private float cost;

    // ゲージ取得
    public float GetMaxGuardGauge => fMaxGuardGauge;
    public float GetGuardGauge => fGuardGauge;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        stop = GetComponent<Stop>();
        burst = GetComponent<GuardBurst>();
        UIgauge = GetComponent<UIGauge>();

        // 体力満タン
        fGuardGauge = fMaxGuardGauge;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // UI
        UIgauge.Refresh(fMaxGuardGauge, fGuardGauge);
        // ハードモードじゃないならゲージ回復
        if (!IsHard)
        {
            RecoveryGauge();
        }
        // ハードモードならゲージ消費
        if(IsHard)
        {
            SubtractGauge();
        }
        // バーストモードなら
        if(IsBurst)
        {
            // 爆発
            burst.Explode();
            GotoNormalState();
        }

        // ハードモードかつゲージ残量があるなら停止
        if(IsHard && fGuardGauge > 0)
        {
            stop.DoStop(rb);
        }
        else
        {
            GotoNormalState();
        }
    }

    // ゲージ回復
    private void RecoveryGauge()
    {
        // ゲージ量回復
        fGuardGauge += fRecovery;
        if(fGuardGauge >= fMaxGuardGauge)
        {
            fGuardGauge = fMaxGuardGauge;
        }
    }

    // ゲージ消費
    private void SubtractGauge()
    {
        fGuardGauge -= cost;
        if (fGuardGauge < 0)
        {
            fGuardGauge = 0;
        }
    }
}
