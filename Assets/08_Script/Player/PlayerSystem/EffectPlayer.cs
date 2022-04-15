//======================================================================
// EffectPlayer.cs
//======================================================================
// 開発履歴
//
// 2022/04/09 author：竹尾 PostProcessを任意起動するスクリプト作成
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;//必要
using UnityEngine.Rendering.Universal;//必要、「Volume」を認識するため

public class EffectPlayer : MonoBehaviour
{
    [Header("<デバックは１～０キーで確認できます>")]

    [Header("PostProcessエフェクト")]
    [SerializeField] Volume DamageVolume = null;
    public int nDamageInterbal = 30;

    [SerializeField] Volume GuardVolume = null;
    public int nGuardInterbal = 30;
    bool bGuardEffect = false;

    [SerializeField] Volume BurstShotVolume = null;
    public int nBurstShotInterbal = 30;


    private void Start()
    {
        // 警告文
        if (DamageVolume == null) Debug.LogError("[DamageVolume] is null!");

        if (GuardVolume == null) Debug.LogError("[GuardVolume] is null!");

        if (BurstShotVolume == null) Debug.LogError("[BurstShotVolume] is null!");
    }

    // 確認用
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(DamageEffect());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GuradEffect();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(BurstShotEffect(nBurstShotInterbal));
        }
    }

    
    
    // ダメージ演出　**********************************************************
    public IEnumerator DamageEffect()
    {
        DamageVolume.weight = 1;
        for (int n = nDamageInterbal; n >= 0; n--)
        {           
            yield return null;
            DamageVolume.weight = (float)n / nDamageInterbal;            
        }
        DamageVolume.weight = 0;
    }
    // ************************************************************************

    // ガード演出 *************************************************************
    public void GuradEffect()
    {
        bGuardEffect = !bGuardEffect;
        if (bGuardEffect == true)
        {
            StartCoroutine(StartGurad());
        }
        else
        {
            StartCoroutine(EndGurad());
        }
    }

    IEnumerator StartGurad()
    {
        GuardVolume.weight = 0;
        for (int n = 0; n <= nGuardInterbal; n++)
        {
            yield return null;
            GuardVolume.weight = (float)n / nGuardInterbal;
        }
        GuardVolume.weight = 1;
    }

    IEnumerator EndGurad()
    {
        GuardVolume.weight = 1;
        for (int n = nGuardInterbal; n >= 0; n--)
        {
            yield return null;
            GuardVolume.weight = (float)n / nGuardInterbal;
        }
        GuardVolume.weight = 0;
    }
    // ************************************************************************

    // バーストショット演出　**********************************************************
    public IEnumerator BurstShotEffect(int armorflame) // 無敵時間中に発動
    {
        BurstShotVolume.weight = 1;
        for (int n = armorflame; n >= 0; n--)
        {
            yield return null;
            if(n <= armorflame / 3)
            {
                BurstShotVolume.weight = (float)n / nBurstShotInterbal;
            }           
        }
        BurstShotVolume.weight = 0;
    }
    // ************************************************************************
}
