//======================================================================
// BossTimer.cs
//======================================================================
// 開発履歴
//
// 2022/03/24 author：小椋駿 製作開始　ボス出現タイマー処理追加。
//                           テキストの反映、ゲージ減少。
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossTimer : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI textMesh;

    private int second;
    private int minute;

    // 残り時間
    float fTimer;

    // ボス出現時間
    [Header("ボス出現時間")][SerializeField]float fCount = 60.0f;

    // 出現するボス
    [Header("出現するボス")][SerializeField]GameObject Boss;

    GameObject Player;

    void Start()
    {
        // 初期化
        slider = GetComponent<Slider>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        fTimer = fCount;
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        fTimer -= Time.deltaTime;

        // 0秒になったら
        if(fTimer < 0.0f)
        {
            fTimer = 0.0f;

            // ボスの出現処理(座標は適当)
            Instantiate(Boss, new Vector3(0.0f, 0.0f, 0.0f), Boss.transform.rotation);

            // 消滅（仮）
            Destroy(gameObject);
        }

        // ゲージ減少
        slider.value =  fTimer/ fCount;

        // 分、秒の計算
        minute = (int)fTimer / 60;
        second = (int)fTimer % 60;

        // テキストに反映
        textMesh.text = minute.ToString("d2") + ":" + second.ToString("d2");

    }
}
