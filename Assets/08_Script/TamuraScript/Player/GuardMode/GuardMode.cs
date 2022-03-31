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
// 2022/03/27 author：田村敏基 爆発の威力を蓄える機能実装
// 2022/03/28 author：竹尾　応急 エフェクト発生組み込み
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 判定コンポーネントアタッチ
[RequireComponent(typeof(Stop))]
[RequireComponent(typeof(GuardBurst))]

public class GuardMode : PlayerManager
{
    // 停止
    private Stop stop;

    // バースト
    private GuardBurst burst;

    // ガードゲージ
    private PlayerStatus status;
    [SerializeField] private int nRecovery = 2;
    [SerializeField] private int nCost = 1;

    // 爆発威力を収納
    private float fStockBurst = 0.0f;

    //*応急* エフェクトスクリプト
    [SerializeField] AID_PlayerEffect effect;

    // ガードモデルとデフォルトモデル
    [SerializeField] private GameObject DefaultModel;
    [SerializeField] private GameObject GuardModel;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        stop = GetComponent<Stop>();
        burst = GetComponent<GuardBurst>();
<<<<<<< HEAD:Assets/08_Script/TamuraScript/GuardMode/GuardMode.cs
        UIgauge = GetComponent<UIGauge>();

        // 体力満タン
        fGuardGauge = fMaxGuardGauge;
=======
        state = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/TamuraScript/Player/GuardMode/GuardMode.cs
    }

    // Update is called once per frame
    protected override void Update()
    {
<<<<<<< HEAD
<<<<<<< HEAD:Assets/08_Script/TamuraScript/GuardMode/GuardMode.cs
        base.Update();

        // UI
        UIgauge.Refresh(fMaxGuardGauge, fGuardGauge);
=======
        Debug.Log(fStockBurst);
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/TamuraScript/Player/GuardMode/GuardMode.cs
        // ハードモードじゃないならゲージ回復
        if (!IsHard)
=======
        // ハードモードじゃない
        if (!state.IsHard)
>>>>>>> 68d270aaeb83160bd5b19f520e97cbc3b59f9431
        {
            // ゲージ回復
            RecoveryGauge();
            DefaultModel.SetActive(true);
            GuardModel.SetActive(false);
        }
<<<<<<< HEAD
        // ハードモードならゲージ消費
        if(IsHard)
=======
        // ハードモードなら
        if(state.IsHard)
>>>>>>> 68d270aaeb83160bd5b19f520e97cbc3b59f9431
        {
            // ゲージ消費
            SubtractGauge();
            DefaultModel.SetActive(false);
            GuardModel.SetActive(true);
        }
        // バーストモードなら
        if(IsBurst)
        {
            //*応急*
            effect.StartEffect(1, this.gameObject, 1.0f);

            // 爆発
<<<<<<< HEAD:Assets/08_Script/TamuraScript/GuardMode/GuardMode.cs
            burst.Explode();
            GotoNormalState();
        }

        // ハードモードかつゲージ残量があるなら停止
        if(IsHard && fGuardGauge > 0)
=======
            burst.Explode(fStockBurst);
            // 瞬間的に力を加えてはじく
            rb.AddForce(transform.forward * fStockBurst, ForceMode.Impulse);
            state.GotoNormalState();
            fStockBurst = 0.0f;
        }

        // ハードモードかつゲージ残量があるなら停止
        if(state.IsHard && status.Stamina > 0)
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/TamuraScript/Player/GuardMode/GuardMode.cs
        {
            stop.DoStop(rb);

            //*応急*
            effect.StartEffect(2, this.gameObject, 1.0f);
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

    public void AddStockExplode(float damage)
    {
        fStockBurst += damage;
    }
}
