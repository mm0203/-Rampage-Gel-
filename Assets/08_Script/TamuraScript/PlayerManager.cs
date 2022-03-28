//======================================================================
// PlayerManager.cs
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

public abstract class PlayerManager : MonoBehaviour
{
    // モード状態
    protected enum StateEnum
    {
        eNormal = 0,
        eHard,
        eBurst,
    }
    static protected StateEnum eState = StateEnum.eNormal;

    // リジッドボディ
    protected Rigidbody rb;

    // バースト
    //同時離し判定受付時間
    [SerializeField] private int interbalTime = 3;

    // 同時離し判定
    private bool b_release = false;

    // 受付用
    private bool b_receiptA = false, b_receiptB = false;
    private int n_interbalA = 0, n_interbalB = 0;

    // 現在モード取得
    public bool IsNormal => eState == StateEnum.eNormal;
    public bool IsHard => eState == StateEnum.eHard;
    public bool IsBurst => eState == StateEnum.eBurst;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // キーボード移動 
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1) ||
            Input.GetAxis("LTrigger") >= 0.3f && Input.GetAxis("RTrigger") >= 0.3f)
        {
            GotoHardState();
        }
        else
        {
            GotoNormalState();
        }

        // バースト移行
        if (Check(Input.GetMouseButtonUp(0), Input.GetMouseButtonUp(1)))
        {
            GotoBurstState();
        }
    }

    // 同時離しチェック関数(竹尾作成)
    bool Check(bool KeyA, bool KeyB)
    {
        // KeyA 受付
        if (KeyA)
        {
            b_receiptA = true;
        }

        if (b_receiptA)
        {
            Debug.Log("A受付中" + n_interbalA);
            n_interbalA++;
        }

        if (n_interbalA > interbalTime)
        {
            Debug.Log("A受付停止");
            b_receiptA = false;
            n_interbalA = 0;
        }


        // KeyB 受付
        if (KeyB)
        {
            b_receiptB = true;
        }

        if (b_receiptB)
        {
            Debug.Log("B受付中" + n_interbalB);
            n_interbalB++;
        }

        if (n_interbalB > interbalTime)
        {
            Debug.Log("B受付停止");
            b_receiptB = false;
            n_interbalB = 0;
        }

        // 判定
        if (b_receiptA && b_receiptB)
        {
            b_release = true;
            return b_release;
        }

        b_release = false;

        return b_release;
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
