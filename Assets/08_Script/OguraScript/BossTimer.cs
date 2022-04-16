//======================================================================
// BossTimer.cs
//======================================================================
// 開発履歴
//
// 2022/03/24 author：小椋駿 製作開始　ボス出現タイマー処理追加。
//                           テキストの反映、ゲージ減少。
// 2022/04/04 author：小椋駿 中ボス用に少し改良
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
            // プレイヤーの上方向に出現
            Vector3 pos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z + 20.0f);   
            Boss = Instantiate(Boss, pos, Boss.transform.rotation);

            // 中ボスにプレイヤー情報セット
            if(Boss.GetComponent<EnemyBase>())
                Boss.GetComponent<EnemyBase>().SetPlayer(Player);

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
