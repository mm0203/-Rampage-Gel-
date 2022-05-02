using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHPUI : MonoBehaviour
{
    public GameObject Boss;

    float fMaxHP, fNowHp;

    Slider slider;
    TextMeshProUGUI textMesh;

    void Start()
    {
        // 体力取得
        fMaxHP = fNowHp = Boss.GetComponent<BossBase>().nHp;

        // 初期化
        slider = GetComponent<Slider>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

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
