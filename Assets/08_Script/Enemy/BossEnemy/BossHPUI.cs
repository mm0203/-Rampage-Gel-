
//======================================================================
// BossHPUI.cs
//======================================================================
// 開発履歴
//
// 2022/05/02 author：小椋駿  ボスHPUI処理作成。
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    // [BossTimer.cs]にてBossを取得
    [HideInInspector] public GameObject Boss;

    // HP
    float fMaxHP, fNowHp;

    Slider slider;

    //----------------------
    // 初期化
    //----------------------
    void Start()
    {
        // 体力取得
        fMaxHP = fNowHp = Boss.GetComponent<BossBase>().nHp;

        // 初期化
        slider = GetComponent<Slider>();
    }


    //----------------------
    // 更新
    //----------------------
    void Update()
    {
        // ボス死亡時破棄
        if (Boss == null) Destroy(gameObject);

        // 現在のHP取得
        fNowHp = Boss.GetComponent<BossBase>().nHp;

        // ゲージ減少
        slider.value = fNowHp / fMaxHP;
    }
}
