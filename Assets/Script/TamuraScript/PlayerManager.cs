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
    private int nUpCount;

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
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            GotoHardState();
        }

        // バースト移行
        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            nUpCount++;
            StartCoroutine(IsPossibleBurst());
        }
    }

    IEnumerator IsPossibleBurst()
    {
        // 0.3秒待機
        yield return new WaitForSeconds(0.3f);

        if(nUpCount >= 2)
        {
            GotoBurstState();
        }
        else
        {
            GotoNormalState();
        }
        nUpCount = 0;

        yield break;
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
