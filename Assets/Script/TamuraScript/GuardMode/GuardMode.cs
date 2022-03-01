using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMode : PlayerManager
{
    // 停止
    Stop stop;

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

        // 体力満タン
        fGuardGauge = fMaxGuardGauge;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // ハードモードじゃないなら回復
        if (!IsHard)
        {
            RecoveryGauge();
        }
        if(IsHard)
        {
            SubtractGauge();
        }

        // ハードモードかつゲージ残量があるなら停止
        if(IsHard && fGuardGauge > 0)
        {
            stop.DoStop(rb);
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
