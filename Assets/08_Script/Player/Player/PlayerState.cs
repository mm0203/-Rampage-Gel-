//======================================================================
// PlayerState.cs
//======================================================================
// 開発履歴
//
// 2022/03/01 author：田村敏基 状態遷移を行うスクリプト作成
// 2022/03/03 author：田村敏基 親クラスにするためabstractにした
// 2022/03/11 author：田村敏基 バースト状態追加(バグ多数)
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // モード状態
    private enum StateEnum
    {
        eNormal = 0,
        eHard,
        eBurst,
        eDie,
    }
    private StateEnum eState = StateEnum.eNormal;

    // リジッドボディ
    private Rigidbody rb;

    // バースト
    //同時離し判定受付時間
    private float time;
    [SerializeField] private float fInterbalTime = 3;

    // 同時離し判定
    bool bLflg = false;
    bool bRflg = false;

    private bool m_isLAxisInUse = false;
    private bool m_isRAxisInUse = false;

    GuardMode guardMode;

    // 硬化フラグ
    public bool bGuard = false;
    public bool bPadGuard = false;

    // 現在モード取得
    public bool IsNormal => eState == StateEnum.eNormal;
    public bool IsHard => eState == StateEnum.eHard;
    public bool IsBurst => eState == StateEnum.eBurst;
    public bool IsDie => eState == StateEnum.eDie;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        guardMode = GetComponent<GuardMode>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDie) return;

        //// XBox操作 ***********************************************
        //if (IsDoubleTrigger_XBOXTriggerDown(Input.GetAxis("LTrigger"), Input.GetAxis("RTrigger")))
        //{
        //    if (!guardMode.bGuardPenalty)
        //    {
        //        GotoHardState();
        //    }
        //}

        //// バースト移行
        //if (IsDoubleTrigger_XBOXTriggerUP(Input.GetAxis("LTrigger"), Input.GetAxis("RTrigger")))
        //{
        //    if (bGuard)
        //    {
        //        GotoBurstState();
        //    }
        //}
        //***********************************************************

        // XBox操作 ************************************************* 
        //if (IsDoubleTrigger(Input.GetKeyDown("joystick button 4"), Input.GetKeyDown("joystick button 5")))
        //{
        //    if (!guardMode.bGuardPenalty)
        //    {              
        //        GotoHardState();
        //    }

        //}
        if (Input.GetAxis("LTrigger") >= 0.1f && Input.GetAxis("RTrigger") >= 0.1f)
        {
            GotoHardState();
            bPadGuard = true;
        }

        if (Input.GetAxis("LTrigger") <= 0.1f && Input.GetAxis("RTrigger") <= 0.1f)
        {
            if (bPadGuard)
            {
                Debug.Log("aaa");
                // 一定量振動させる
                StartCoroutine("StartVibation");
                GotoBurstState();
                bPadGuard = false;
            }
        }

        // バースト移行
        //if (IsDoubleTrigger(Input.GetKeyUp("joystick button 4"), Input.GetKeyUp("joystick button 5")))
        //{
        //    if (bGuard)
        //    {
        //        // 一定量振動させる
        //        StartCoroutine("StartVibation");
        //        GotoBurstState();
        //    }
        //}
        //***********************************************************

        // キーボード操作********************************************
        if (IsDoubleTrigger(Input.GetMouseButtonDown(0), Input.GetMouseButtonDown(1)))
        {
            if (!guardMode.bGuardPenalty)
            {
                GotoHardState();
            }
        }

        // バースト移行
        if (IsDoubleTrigger(Input.GetMouseButtonUp(0), Input.GetMouseButtonUp(1)))
        {
            if (bGuard)
            {
                
                StartCoroutine(StartVibation());
                GotoBurstState();
            }
        }
        //***********************************************************
    }

    public bool IsDoubleTrigger(bool LB, bool RB)
    {
        if (LB) bLflg = true;
        if (RB) bRflg = true;

        if (bLflg || bRflg)
        {
            time += Time.deltaTime;

            if (time <= fInterbalTime)
            {
                if (LB && bRflg)
                {
                    bLflg = false;
                    bRflg = false;
                    bGuard = true;
                    time = 0.0f;
                    return true;
                }
                if (RB && bLflg)
                {
                    bLflg = false;
                    bRflg = false;
                    bGuard = true;
                    time = 0.0f;
                    return true;
                }
            }
            else
            {
                bLflg = false;
                bRflg = false;
                time = 0.0f;
            }
        }
        bGuard = false;
        return false;
    }

    // XBOXコントローラー処理 **********************************
    public bool IsDoubleTrigger_XBOXTriggerDown(float LB, float RB)
    {
        if (LB != 0)
        {
            if (m_isLAxisInUse == false)
            {
                Debug.Log("押した");
                bLflg = true;
                m_isLAxisInUse = true;
            }
        }
        if (LB == 0)
        {

            m_isLAxisInUse = false;
        }

        if (RB != 0)
        {
            if (m_isRAxisInUse == false)
            {
                bRflg = true;
                m_isRAxisInUse = true;
            }
        }
        if (RB == 0)
        {
            m_isLAxisInUse = false;
        }



        if (bLflg || bRflg)
        {
            time += Time.deltaTime;

            if (time <= fInterbalTime)
            {
                if (LB > 0.1 && bRflg)
                {
                    bLflg = false;
                    bRflg = false;
                    bGuard = true;
                    time = 0.0f;
                    return true;
                }
                if (RB > 0.1 && bLflg)
                {
                    bLflg = false;
                    bRflg = false;
                    bGuard = true;
                    time = 0.0f;
                    return true;
                }
            }
            else
            {
                bLflg = false;
                bRflg = false;
                time = 0.0f;
            }
        }
        bGuard = false;
        return false;
    }

    public bool IsDoubleTrigger_XBOXTriggerUP(float LB, float RB)
    {
        
        if (LB != 0)
        {
            if (m_isLAxisInUse == false)
            {
                Debug.Log("押した");
                bLflg = true;
                m_isLAxisInUse = true;
            }
        }
        if (LB == 0)
        {

            m_isLAxisInUse = false;
        }

        if (RB != 0)
        {
            if (m_isRAxisInUse == false)
            {
                bRflg = true;
                m_isRAxisInUse = true;
            }
        }
        if (RB == 0)
        {
            m_isLAxisInUse = false;
        }


        if (bLflg || bRflg)
        {
            time += Time.deltaTime;

            if (time <= fInterbalTime)
            {
                if (LB > 0.1 && bRflg)
                {
                    bLflg = false;
                    bRflg = false;
                    bGuard = true;
                    time = 0.0f;
                    return true;
                }
                if (RB > 0.1 && bLflg)
                {
                    bLflg = false;
                    bRflg = false;
                    bGuard = true;
                    time = 0.0f;
                    return true;
                }
            }
            else
            {
                bLflg = false;
                bRflg = false;
                time = 0.0f;
            }
        }
        bGuard = false;
        return false;
    }
    //**********************************************************

    // ノーマルモードに移行
    public void GotoNormalState()
    {
        if (!IsDie)
            eState = StateEnum.eNormal;
    }

    // ハードモードに移行
    public void GotoHardState()
    {
        if (!IsDie)
            eState = StateEnum.eHard;
    }

    // バーストモードに移行
    public void GotoBurstState()
    {
        if (!IsHard) return;
        
        eState = StateEnum.eBurst;
    }

    public void GotoDieState()
    {
        eState = StateEnum.eDie;
    }

    // 振動コルーチン
    IEnumerator StartVibation()
    {
        
        XInputDotNetPure.GamePad.SetVibration(0, 5, 5);
        yield return new WaitForSecondsRealtime(0.5f);
        XInputDotNetPure.GamePad.SetVibration(0, 0, 0);
    }

    
}
