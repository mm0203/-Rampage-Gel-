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
// 2022/05/05                    バーストの発生をダメージから回数へ変更
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
    [SerializeField] private LineRenderer Direction = null;  // 発射方向

    // 停止
    private Stop stop;

    // バースト
    private GuardBurst burst;

    // リジッドボディ
    private Rigidbody rb;

    // ステート
    private PlayerState state;

    // ガードゲージ
    private PlayerStatus status;

    // 爆発威力を収納
    private int fStockBurst = 0;

    // 硬化中にローテするための変数
    bool bGuardStart = false;

    //*応急* エフェクトスクリプト
    [SerializeField] AID_PlayerEffect effect;

    // サウンドエフェクト
    [SerializeField] SoundManager soundManager;
    float fVibeInterbal  = 1.0f;
    float fCountTime = 0;

    // ガードモデルとデフォルトモデル
    [SerializeField] private GameObject DefaultModel;
    [SerializeField] private GameObject GuardModel;

    // ガード中に向き変えるための変数
    private Vector3 vStartPos = Vector3.zero;
    private Vector3 vCurrentForce = Vector3.zero;

    // ガードペナルティ
    public bool bGuardPenalty = false;
    float fGuardPenaltyTime = 1.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        stop = GetComponent<Stop>();
        burst = GetComponent<GuardBurst>();
        state = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();

        fVibeInterbal = 1.0f;
        fCountTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!state.IsHard)
        {
            // ゲージ回復
            RecoveryGauge();
            DefaultModel.SetActive(true);
            GuardModel.SetActive(false);
            // ガードペナルティ時間
            if(bGuardPenalty)
            {
                fGuardPenaltyTime += Time.deltaTime;
                if(fGuardPenaltyTime >= status.fGuardPenalty)
                {
                    fGuardPenaltyTime = 0.0f;
                    bGuardPenalty = false;
                }
            }
        }
        // ハードモードなら
        if (state.IsHard)
        {
            // キーボード
            if(!bGuardStart)
            {
                vStartPos = GetMousePosition();
                bGuardStart = true;
            }
            // パッド
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (Mathf.Abs(x) >= 0.5f || Mathf.Abs(y) >= 0.5f)
            {
                // 入力方向を逆にして受け取る
                vCurrentForce = new Vector3(-x * 1000, 0, -y * 1000);

                // 動く方向を見る
                transform.rotation = Quaternion.LookRotation(vCurrentForce);
            }


            // ゲージ消費
            SubtractGauge();
            DefaultModel.SetActive(false);
            GuardModel.SetActive(true);

            // 動かしたマウス座標の位置を取得
            var position = GetMousePosition();
            // マウスの初期座標と動かした座標の差分を取得
            vCurrentForce = vStartPos - position;

            // ガード中にダメージが当たったら
            if (fStockBurst >= 0.01f)
            {
                // 矢印の引っ張り処理
                Direction.enabled = true;
                // 動く方向と逆に矢印が出るように
                Direction.SetPosition(0, rb.position);
                Direction.SetPosition(1, rb.position - transform.forward * 2);
            }

            // 動く方向を見る
            if (vCurrentForce != new Vector3(0, 0, 0))
            {
                transform.rotation = Quaternion.LookRotation(vCurrentForce);
            }
        }
        // バーストモードなら
        if (state.IsBurst)
        {
            //*応急*
            effect.StartEffect(1, this.gameObject, 1.0f);
            soundManager.Play_PlayerBurst(this.gameObject);

            // 爆発
            burst.Explode(fStockBurst);
            // 瞬間的に力を加えてはじく
            rb.AddForce(transform.forward * fStockBurst, ForceMode.Impulse);
            Direction.enabled = false;
            status.bArmor = true;
            status.fBreakTime = 0.0f;
            state.GotoNormalState();
            fStockBurst = 0;
            bGuardStart = false;
        }

        // ハードモードかつゲージ残量があるなら停止
        if (state.IsHard && status.Stamina > 0)
        {
            if(fCountTime < 0)
            {
                StartCoroutine(StartVibation());
                fCountTime = fVibeInterbal;
            }
            fCountTime -= Time.deltaTime;


            stop.DoStop(rb);
        }
        else
        {
            state.GotoNormalState();
            state.bGuard = false;
            bGuardStart = false;
        }
    }

    // ゲージ回復
    private void RecoveryGauge()
    {
        // ゲージ量回復
        status.Stamina += status.StaminaRecovery;
        if (status.Stamina >= status.MaxStamina)
        {
            status.Stamina = status.MaxStamina;
        }
    }

    // ゲージ消費
    private void SubtractGauge()
    {
        status.Stamina -= status.StaminaCost;
        if (status.Stamina < 0)
        {
            status.Stamina = 0;
            // ガードペナルティ発生
            soundManager.Play_PlayerGuardBreak(this.gameObject);
            bGuardPenalty = true;
        }
    }

    public void AddStockExplode(float damage)
    {
        //回数制に変更（竹尾）
        fStockBurst += 1;
        soundManager.Play_PlayerDamageatGuardA(this.gameObject);
        if(fStockBurst > 6)
        {
            fStockBurst = 6;
        }
    }

    private Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
    }

    // 振動コルーチン(ガード振動)
    IEnumerator StartVibation()
    {

        XInputDotNetPure.GamePad.SetVibration(0, 1, 1);
        yield return new WaitForSecondsRealtime(0.1f);
        XInputDotNetPure.GamePad.SetVibration(0, 0, 0);
        yield return new WaitForSecondsRealtime(0.1f);
        XInputDotNetPure.GamePad.SetVibration(0, 1, 1);
        yield return new WaitForSecondsRealtime(0.1f);
        XInputDotNetPure.GamePad.SetVibration(0, 0, 0);
    }
}
