//======================================================================
// DamageUI.cs
//======================================================================
// 開発履歴
//
// 2022/03/15 author：小椋駿 製作開始　ダメージ表記処理
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    // 消える秒数
    [SerializeField] private float fDeleteTime = 1.0f;

    // 動く距離
    [SerializeField] private float fMoveRange = 1.0f;

    // カウンター
    private float TimeCnt;

    private TextMesh NowText;

    void Start()
    {
        TimeCnt = 0.0f;
        Destroy(this.gameObject, fDeleteTime);
        NowText = this.gameObject.GetComponent<TextMesh>();
    }


    void Update()
    {
        // カメラの向きに向く
        //this.transform.LookAt(Camera.main.transform);

        TimeCnt += Time.deltaTime;

        // テキストの動き
        this.gameObject.transform.localPosition += new Vector3(0, fMoveRange / fDeleteTime * Time.deltaTime, 0);

        // テキスト透明度
        float alpha = 1.0f - (TimeCnt / fDeleteTime);
        if (alpha <= 0.0f) alpha = 0.0f;

        // テキストカラー
        NowText.color = new Color(NowText.color.r, NowText.color.g, NowText.color.b, alpha);
    }
}