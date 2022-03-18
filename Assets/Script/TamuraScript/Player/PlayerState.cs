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

    // 現在モード取得
    public bool IsNormal => eState == StateEnum.eNormal;
    public bool IsHard => eState == StateEnum.eHard;
    public bool IsBurst => eState == StateEnum.eBurst;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // キーボード移動 
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            GotoHardState();
        }
        else
        {
            GotoNormalState();
        }

        // バースト移行
        if (IsDoubleTrigger(Input.GetMouseButtonUp(0), Input.GetMouseButtonUp(1)))
        {
            GotoBurstState();
        }
    }

    private bool IsDoubleTrigger(bool LB, bool RB)
    {
        if (LB) bLflg = true;
        if (RB) bRflg = true;

        if(bLflg || bRflg)
        {
            time += Time.deltaTime;

            if(time <= fInterbalTime)
            {
                if(LB && bRflg)
                {
                    bLflg = false;
                    bRflg = false;
                    time = 0.0f;
                    return true;
                }
                if(RB && bLflg)
                {
                    bLflg = false;
                    bRflg = false;
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
        return false;
    }

    // ノーマルモードに移行
    public void GotoNormalState()
    {
        eState = StateEnum.eNormal;
    }

    // ハードモードに移行
    public void GotoHardState()
    {
        eState = StateEnum.eHard;
    }

    // バーストモードに移行
    public void GotoBurstState()
    {
        eState = StateEnum.eBurst;
    }
}
